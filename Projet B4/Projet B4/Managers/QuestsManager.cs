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
                        //if i have not completed the requirements for this task element.
                        if (myQuest.tasks[task.id].requiredElements[s] < task.requiredElements[s])
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
            }
            catch 
            {
                player.Send("err", "Quest not found!");
            }
        }

        public void completeQuest()
        { 
        
        }
    }
}
