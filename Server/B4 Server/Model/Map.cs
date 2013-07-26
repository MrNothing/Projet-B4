using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
	public class Map
	{
		public string id;
		public string name="N/A";
		public int mapLevel=0;
		public bool persistent=false;
		
		public HashTable tiles = new HashTable();	//map elements (useless on the serverside)
		public HashTable pathTiles = new HashTable(); //walkable tiles
		
		public Dictionary<String, Event> events = new Dictionary<string, Event>(); //The events for this zone.
		public Dictionary<String, Entity> entities = new Dictionary<string, Entity>(); //entities, units or triggers
		//public Dictionary<String, Item> Items = new Dictionary<string, Item>(); //items on the floor;
		
		public Dictionary<String, SpawnZone> spawnZones = new Dictionary<string, SpawnZone>(); //The events for this zone.
		
		public Map(string _id)
		{
			id = _id;
		}
	}
}