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
		public void loadMap(GameCode core, String map)
        {
            if (map.Equals("Map1"))
            {
                Map map_0 = new Map("Fields");
                map_0.name = "Map1";

                UnitsInfos tmpInfos = new UnitsInfos();

                string[] mobsGrp1 = new string[1];
                mobsGrp1[0] = "Blob";

                SpawnZone pack1 = new SpawnZone(core, new Vector3(-0.1796811f, 7.400879f, -4.319699f), mobsGrp1, 20);
                pack1.maxAmountSimulaneously = 10;

                core.spawnZones.Add("blobs1", pack1);

                string[] mobsskel = new string[1];
                mobsskel[0] = "Skeleton";

                SpawnZone pack2 = new SpawnZone(core, new Vector3(-40, 7.400879f, -4.319699f), mobsskel, 20);
                pack2.maxAmountSimulaneously = 10;

                core.spawnZones.Add("skel", pack2);

                core.addUnit(new Entity(core, "", "Crawler", tmpInfos.getEntityInfosByName("Crawler"), new Vector3(0, 7.400879f, -30)));
                //core.addUnit(new Entity(core, "", "Black Dragon", tmpInfos.getEntityInfosByName("Black Dragon"), new Vector3(0, 7.400879f, -60)));
            }
        }
	}
}