/*
	Generated with B4 Built-in Editor
		999* tiles
		999* pathTiles
		999* events
		999* entities
*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
	public class MapsData
	{
		public Hashtable maps = new Hashtable();
		public MapsData()
		{
			//example...
			Map map_0 = new Map("fields");
			map_0.name = "fields";
			
			Vector3 tile_0 = new Vector3(0,0,0);
			map_0.pathTiles.Add(tile_0.toPosRefId(), tile_0);
			
			maps.Add("fields", map_0);
		}
	}
}