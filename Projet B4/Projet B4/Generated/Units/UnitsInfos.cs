using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class UnitsInfos
    {
        public Dictionary<String, EntityInfos> items = new Dictionary<string, EntityInfos>();
        public EntityInfos getEntityInfosByName(String itemName)
        {
            return items[itemName];
        }


        public UnitsInfos()
        {
            items.Add("Lever", new Static_Units.Lever().value());
            items.Add("BobDeLaLune", new Static_Units.BobDeLaLune().value());
            items.Add("Lich", new Static_Units.TikTik().value());
            items.Add("Ent", new Static_Units.WoodenGolem().value());
            items.Add("LavaGolem", new Static_Units.LavaGolem().value());
            items.Add("Button", new Static_Units.Button().value());
            items.Add("MillHelice", new Static_Units.MillHelice().value());
            items.Add("Blob", new Static_Units.Blob().value());
            items.Add("IceSpirit", new Static_Units.IceSpirit().value());
            items.Add("MechaGolem", new Static_Units.MechaGolem().value());
            items.Add("Crawler", new Static_Units.Crawler().value());
            items.Add("Balrog", new Static_Units.Balrog().value());
            items.Add("Skeleton", new Static_Units.Skeleton().value());
            items.Add("BlackDrake", new Static_Units.BlackDrake().value());
            items.Add("Horse1", new Static_Units.Horse1().value());
            items.Add("Guard", new Static_Units.Guard().value());
            items.Add("Wolf", new Static_Units.Wolf().value());
            items.Add("Wolf2", new Static_Units.WhiteWolf().value());
            items.Add("Wolf3", new Static_Units.SpiritWolf().value());
        }
    }
}
