using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public enum triggerAction
    { 
        doNothing, triggerEvent, grantReward, makeNpcSayMessage, spawnNpcs, killNpc, moveToPoint,
    }

    public class Triggers
    {
        public Entity parentEntity;
       
        public Triggers[] triggersToEnable = new Triggers[0];
        public Triggers[] requiredTriggersOn = new Triggers[0];
        public Triggers[] requiredTriggersOff = new Triggers[0];

        public string[] requiredKeys = new string[0];

        public bool activated = false;
        public bool locked = false;
        public int autoTrigger = 0; //if >0 call activate every X runs
        public int autoTriggerCounter = 0; //if >0 call activate every X runs

        public triggerAction onActivation = triggerAction.doNothing;

        public Item[] rewards = new Item[0];
        public bool[] rewardsChecker = new bool[0];

        public Triggers(Entity _parentEntity)
        {
            parentEntity = _parentEntity;
        }

        public void activate()
        {
            if (requiredTriggersOn.Length > 0)
            {
                foreach (Triggers t in requiredTriggersOn)
                {
                    if (!t.activated)
                        return;
                }
            }

            if (requiredTriggersOff.Length > 0)
            {
                foreach (Triggers t in requiredTriggersOff)
                {
                    if (t.activated)
                        return;
                }
            }

            if (activated)
            {
                activated = false;
            }
            else
            {
                activated = true;
            }
            sendTriggerStatus();
        }

        public void activate(Hero author)
        {
            if (rewards.Length > 0)
            {
                //send rewards...
                return;
            }


            if (requiredTriggersOn.Length > 0)
            {
                foreach (Triggers t in requiredTriggersOn)
                {
                    if (!t.activated)
                        return;
                }
            }

            if (requiredTriggersOff.Length > 0)
            {
                foreach (Triggers t in requiredTriggersOff)
                {
                    if (t.activated)
                        return;
                }
            }

           
            if (locked)
                return;

            if (requiredKeys.Length > 0)
            {
                foreach (string s in requiredKeys)
                {
                    //if i have the key proceed, otherwise return.
                    try
                    {
                        float tmpStr = author.itemsByName[s];
                    }
                    catch (Exception e)
                    {
                        return;
                    }
                }
            }
           
            foreach (Triggers t in triggersToEnable)
            {
                //parentEntity.myGame.PlayerIO.ErrorLog.WriteError("Activating child trigger: "+t.parentEntity.name);
                t.activate();
            }
            

            if (activated)
            {
                activated = false;
            }
            else
            {
                activated = true;
            }
            sendTriggerStatus();
        }

        public void sendTriggerStatus()
        {
            Object[] data = new Object[2];
            data[0] = parentEntity.id; //id
            data[1] = activated; //activated

            parentEntity.myGame.sendDataToAll("trigger", data);
        }

        public void sendRewards()
        { 
        
        }

        public void pickReward(int _index, Hero author)
        {
            if (rewardsChecker[_index])
                author.addItem(rewards[_index].infos.name);
        }
    }
}
