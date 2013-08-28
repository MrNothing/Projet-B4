using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class QuestInfos
    {
        public Dictionary<string, QuestPattern> quests = new Dictionary<string,QuestPattern>();
        public QuestPattern getQuestByName(string name)
        {
            return quests[name];
        }

        public QuestInfos()
        { 
            //initialize all quests here...
        }
    }
}
