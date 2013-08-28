using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public enum QuestType
    {
        standard, repeat, daily, monthy, everySpecificHour, everySpecificDate
    }

    public enum QuestTaskType
    {
        killEntities, activateTrigger, completeEvent, talkTo, getItem, completeQuest, killPlayers, exploreZone, reachLevel, chatWithNpcAndSayTheCode
    }

    public enum QuestRewardType
    {
        xp, gold, specifiedItem, randomItem, randomIngredient, randomUsable, generatedItem, spell
    }

    /// <summary>
    ///  public QuestTaskType task;
    ///  public Dictionary<string, int> requiredElements; //what element and what amount?
    /// </summary>
    public class QuestTask
    {
        public byte id;
        public QuestTaskType task;
        public Dictionary<string, int> requiredElements = new Dictionary<string,int>(); //what element and what amount?

        public QuestTask(byte _id)
        {
            id = _id;
        }

        public QuestTask(QuestTask origin)
        {
            id = origin.id;
            task = origin.task;
            requiredElements = new Dictionary<string,int>(origin.requiredElements);
        }

        public QuestTask(byte _id, Hashtable infos)
        {
            id = _id;
            task = (QuestTaskType)Enum.Parse(typeof(QuestTaskType), infos["task"].ToString(), true);

            Hashtable tmpElements = (Hashtable)infos["elements"];
            foreach (string s in tmpElements.Keys)
            {
                requiredElements.Add(s, (int)tmpElements[s]);
            }
        }

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            tmp.Add("task", task.ToString());

            Hashtable elements = new Hashtable();
            
            foreach (string s in requiredElements.Keys)
                elements.Add(s, requiredElements[s]);

            tmp.Add("elements", elements);

            return tmp;
        }
    }

    /// <summary>
    /// Defines the reward for teh player when the quest is completed.
    /// </summary>
    public class QuestReward
    {
        public QuestRewardType type;
        public float rewardLevel = 0; //applies for randomized items...
        public float rewardRarity = 0; //applies for randomized items...
        public float rewardQuantity = 1; //how much xp or gold or how many items...
        public string rewardName = ""; //the name of the reward (for specific rewards)

        public QuestReward(QuestRewardType _type)
        {
            type = _type;
        }

        public QuestReward(QuestRewardType _type, string _rewardName, float quantity, float level, float rarity)
        {
            type = _type;
            rewardName = _rewardName;
            rewardQuantity = quantity;
            rewardLevel = level;
            rewardRarity = rarity;
        }
    }

    public class QuestPattern
    {
        public QuestPattern(string _name, int _requiredLevel, Dictionary<byte, QuestTask> _tasks, Dictionary<byte, QuestReward> _rewards, Entity _questGiver, Entity _questEnder)
        {
            requiredLevel = _requiredLevel;
            name = _name;
            tasks = _tasks;
            rewards = _rewards;

            questGiver = _questGiver;
            questEnder = _questEnder;

            foreach (byte b in tasks.Keys)
            {
                QuestTask myTask = tasks[b];
                myTask.id = b;

                QuestTaskType myType = myTask.task;

                List<QuestTask> tmpList;
                try
                {
                    tmpList = tasksByType[myType];
                }
                catch
                {
                    tmpList = new List<QuestTask>();
                }

                tmpList.Add(myTask);

                try
                {
                    tasksByType.Add(myType, tmpList);
                }
                catch
                {
                    tasksByType[myType] = tmpList;
                }
            }
        }

        public string name;
        public QuestType type = QuestType.standard;
        public int requiredLevel = 0;

        public Dictionary<byte, QuestTask> tasks; //all the required tasks to complete the quest
        public Dictionary<QuestTaskType, List<QuestTask>> tasksByType = new Dictionary<QuestTaskType, List<QuestTask>>();

        public string startMessage="";
        public string onGoingMessage="";
        public string onSuccesMessage="";
        public string completedMessage = "";
        public string description = "";

        public Entity questGiver;
        public Entity questEnder;

        public Dictionary<byte, QuestReward> rewards;

        /// <summary>
        /// /// starts the event on quest start.
        /// </summary>
        public string triggerEventOnStart = "";
        /// <summary>
        /// starts the event on quest completion.
        /// </summary>
        public string triggerEventOnComplete = "";
        /// <summary>
        /// the fail even is not implemented yet
        /// </summary>
        public string triggerEventOnFail = "";

        public string previousQuest = "";
        public string nextQuest = "";

        /// <summary>
        /// Generates the quest description.
        /// Only compatible with the non id based QuestTaskTypes:
        ///     - killEntities
        ///     - exploreZone
        ///     - getItem
        ///     - killPlayers
        ///     - reachLevel
        ///     - completeQuest
        ///     - completeEvent
        ///     - talkTo
        /// </summary>
        /// <param name="questGiver">The quest giver.</param>
        void generateQuestDescription(Entity questGiver)
        {
            if (questGiver.type==EntityType.trigger)
                description = "";
            else
                description = questGiver.name + " wants you to ";

            foreach(byte i in tasks.Keys)
            {
                if (tasks[i].task == QuestTaskType.completeEvent)
                {
                    //Well fuck this is too long I'll write the descriptions myself...
                }
            }
        }

        public Dictionary<byte, QuestTask> cloneTasks()
        {
            Dictionary<byte, QuestTask> newTasks = new Dictionary<byte, QuestTask>();
            foreach (byte b in tasks.Keys)
            { 
                newTasks.Add(b, new QuestTask(tasks[b]));
            }
            return newTasks;
        }
    }
}
