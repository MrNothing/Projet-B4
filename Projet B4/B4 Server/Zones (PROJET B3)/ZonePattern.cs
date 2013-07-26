using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProjetB3
{
    public class ZonePattern //DEPRECATED! use Map instead
    {
        public String zoneName;
        public Dictionary<String, Event> Events = new Dictionary<string, Event>(); //The events for this zone.
        public Dictionary<String, SpawnZone> spawnZones = new Dictionary<string, SpawnZone>(); //The events for this zone.
        public Dictionary<String, Entity> Entities = new Dictionary<string, Entity>(); //entities, units or triggers
        public Dictionary<String, Item> Items = new Dictionary<string, Item>(); //items on the floor;
		public HashTable pathTiles = new HashTable(); //walkable tiles
		
        public ZonePattern()
        {
            
        }
    }
}
