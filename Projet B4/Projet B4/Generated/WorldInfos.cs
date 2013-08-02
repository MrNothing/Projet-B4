/*
	Generated with B4 Built-in Editor
		999* Npcs
		999* Items
		999* Spells
		999* Quests
*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
	public class WorldInfos
	{
		public Hashtable Npcs = new Hashtable();
		public Hashtable Items = new Hashtable();
		public Hashtable Spells = new Hashtable();
		public Hashtable Quests = new Hashtable();
		
		public WorldInfos()
		{
			//fill hashtables here...
		}
		
		public Item getItemByName(String itemName) //generates an item using its pattern. that item has no id until it is specified.
		{
			Item newItem = new Item((ItemPattern)Items[itemName]);
			newItem.uses = newItem.infos.charges;
			return newItem;
		}
		
		public EntityInfos getEntityInfosByName(String name)
		{
            UnitsInfos infos = new UnitsInfos();
            return infos.items[name];
		}
	}
}