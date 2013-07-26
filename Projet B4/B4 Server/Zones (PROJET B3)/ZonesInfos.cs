using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB3
{
    public class ZonesInfos
    {
        public Dictionary<String, ZonePattern> zones = new Dictionary<string, ZonePattern>();

        public ZonesInfos()
        {
            zones.Add("Map0", new Zones.Zone1());
            zones.Add("Map1", new Zones.Zone2());
            zones.Add("Map2", new Zones.Zone3());
            zones.Add("Map3", new Zones.Zone4());
        }
    }
}
