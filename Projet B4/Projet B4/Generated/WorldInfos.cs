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
		public HashTable Npcs = new HashTable();
		public HashTable Items = new HashTable();
		public HashTable Spells = new HashTable();
		public HashTable Quests = new HashTable();
		
		public WorldInfos()
		{
			//fill hashtables here...
		}
		
		public Item getItemByName(String itemName) //generates an item using its pattern. that item has no id until it is specified.
		{
			Item newItem = new Item(Items[itemName]);
			newItem.uses = newItem.infos.charges;
			return newItem;
		}
		
		public EntityInfos getEntityInfosByName(String name)
		{
			return Npcs[name];
		}
	}
}