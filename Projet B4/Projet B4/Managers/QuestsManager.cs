using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class QuestsManager
    {
        public GameCode mainInstance;
        public QuestsManager(GameCode _mainInstance)
        {
            mainInstance = _mainInstance;
        }

        /// <summary>
        /// Starts the quest.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="quest">The quest.</param>
        /// <param name="entity">The entity.</param>
        public void startQuest(Player player, string quest, Entity entity)
        {
            try
            {
                QuestPattern questInfos = mainInstance.questInfos.getQuestByName(quest);

                if (questInfos.requiredLevel <= player.myCharacter.level)
                {
                    Quest newQuest = new Quest();
                    newQuest.quest = quest;
                    newQuest.status = QuestStatus.started;
                    newQuest.tasks = questInfos.cloneTasks();
                }
                else
                {
                    player.Send("err", "You dont have the required level!");
                }
            }
            catch
            {
                player.Send("err", "Quest not found!");
            }
        }

        /// <summary>
        /// Updates the quest status.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="specificInfos">The specific infos.</param>
        /// <param name="quest">The quest.</param>
        /// <param name="taskType">Type of the task.</param>
        public void updateQuestStatus(Player player, Object specificInfos, string quest, QuestTaskType taskType)
        {
            try
            {
                Quest myQuest = player.myCharacter.quests[quest];

                QuestPattern questInfos = mainInstance.questInfos.getQuestByName(myQuest.quest);

                List<QuestTask> myTasks;

                try
                {
                    myTasks = questInfos.tasksByType[taskType];
                }
                catch
                {
                    //this quest does not have such QuestTaskType
                    return;
                }

                foreach (QuestTask task in myTasks)
                {
                    foreach (string s in task.requiredElements.Keys)
                    {
                        bool allowElement = false;
                        //if this is the right element:
                        if (taskType != QuestTaskType.reachLevel)
                        {
                            string specificString = specificInfos + "";

                            if (specificString.Equals(s))
                                allowElement = true;
                        }

                        if (taskType == QuestTaskType.reachLevel)
                        {
                            int tmpLevel = (int)specificInfos;
                            if (player.myCharacter.level >= int.Parse(s))
                            {
                                allowElement = true;
                            }
                        }

                        //if i have not completed the requirements for this task element.
                        if (myQuest.tasks[task.id].requiredElements[s] < task.requiredElements[s] && allowElement)
                        {
                            myQuest.tasks[task.id].requiredElements[s]++;

                            //send the quest status update... example on the client side: blobs killed: 3/5
                            Object[] data = new Object[4];
                            data[0] = quest;
                            data[1] = s; //required element (ex: golem)
                            data[2] = myQuest.tasks[task.id].requiredElements[s] + "/" + task.requiredElements[s]; //amount of elements info ex: 2/5
                            data[3] = taskType.ToString(); //task type (ex: kill)

                            player.Send("qUp", data);
                        }
                    }
                }

                if (isQuestCompleted(player, quest))
                {
                    myQuest.status = QuestStatus.completed;
                    myQuest.tasks.Clear();
                    player.myCharacter.startedQuests.Remove(quest);

                    giveQuestRewards(player, quest);
                }
            }
            catch 
            {
                player.Send("err", "quest fatal error!");
            }
        }

        /// <summary>
        /// Determines whether [is quest completed].
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="quest">The quest.</param>
        /// <returns>
        /// 	<c>true</c> if [is quest completed]; otherwise, <c>false</c>.
        /// </returns>
        public bool isQuestCompleted(Player player, string quest)
        {
            try
            {
                Quest myQuest = player.myCharacter.quests[quest];

                if (myQuest.status == QuestStatus.completed)
                    return true;

                QuestPattern questInfos = mainInstance.questInfos.getQuestByName(myQuest.quest);

                foreach (byte b in questInfos.tasks.Keys)
                {
                    QuestTask task = questInfos.tasks[b];

                    foreach (string s in task.requiredElements.Keys)
                    {
                        //i have not completed the required element the quest is not completed...
                        if (myQuest.tasks[b].requiredElements[s] < task.requiredElements[s]) 
                        {
                            return false;
                        }
                    }
                }

            }
            catch
            {
                player.Send("err", "Quest not found!");
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Determines whether [has quest started] for [the specified player].
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="quest">The quest.</param>
        /// <returns>
        /// 	<c>true</c> if [has quest started] for [the specified player]; otherwise, <c>false</c>.
        /// </returns>
        public bool hasQuestStarted(Player player, string quest)
        {
            try
            {
                Quest myQuest = player.myCharacter.quests[quest];
                return true;
            }
            catch
            {
                return false;
            }
        }

        void giveQuestRewards(Player player, string quest)
        {
            QuestPattern questInfos = mainInstance.questInfos.getQuestByName(quest);
            foreach (byte b in questInfos.rewards.Keys)
            {
                QuestReward myReward = questInfos.rewards[b];

                if (myReward.type == QuestRewardType.xp)
                    player.myCharacter.addXp(myReward.rewardQuantity);

                if (myReward.type == QuestRewardType.gold)
                {
                    player.money += (int)(myReward.rewardQuantity);
                    player.myCharacter.sendMoney("");
                }

                if (myReward.type == QuestRewardType.specifiedItem)
                {
                    for (int i = 0; i < myReward.rewardQuantity; i++ )
                        player.myCharacter.addItem(myReward.rewardName);
                }
                
                if (myReward.type == QuestRewardType.spell)
                    player.myCharacter.addSpell(myReward.rewardName);

                if (myReward.type == QuestRewardType.generatedItem)
                {
                    for (int i = 0; i < myReward.rewardQuantity; i++)
                    {
                        Item newItem = mainInstance.itemGenerator.generateItem("gen", myReward.rewardRarity, (int)Math.Ceiling(myReward.rewardLevel));
                        player.myCharacter.addItem(newItem);
                    }
                }

                if (myReward.type == QuestRewardType.randomItem)
                { 
                    //will come with the future loot system that has predefined items...
                }

                if (myReward.type == QuestRewardType.randomUsable)
                {
                    //will come with the future loot system that has predefined items...
                }

                if (myReward.type == QuestRewardType.randomIngredient)
                {
                    //will come with the future loot system that has predefined items...
                }
            }
        }
    }
}
