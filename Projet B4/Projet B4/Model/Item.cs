using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProjetB4
{
    public class Item
    {
        public String id;

        public float cooldown = 0; //triggered after use
        public float uses = 0; //if the item is usable only a specific amount of times.

        public bool equipped = false;

        public bool generated = false;

        public ItemPattern infos;
        public Vector3 position; //position on the Bag;

        public Item(ItemPattern _infos)
        {
            infos = _infos;
        }
		
		public Hashtable toHashtable()
		{
			Hashtable tmpInfos = new Hashtable();
			
			tmpInfos.Add("id", id);
			tmpInfos.Add("cooldown", cooldown);
			tmpInfos.Add("uses", uses);
			tmpInfos.Add("equipped", equipped);

            tmpInfos.Add("infos", infos.toHashtable());

            return tmpInfos;
		}

        public String toString()
        {
            String des = "{id: " + id + ", type: " + infos.type.ToString() + ",";

            for (int i = 0; infos.effects[i]!=null; i++)
            {
                des += " [Effect" + i + ": " + infos.getEffectDescription(infos.effects[i].effect, infos.effects[i].value)+"]";
            }
            
            return des+"}";
        }
    }
}
