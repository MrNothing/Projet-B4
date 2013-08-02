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
            items.Add("TikTik", new Static_Units.TikTik().value());
            items.Add("Wooden Golem", new Static_Units.WoodenGolem().value());
            
            items.Add("Button", new Static_Units.Button().value());
            items.Add("MillHelice", new Static_Units.MillHelice().value());
            items.Add("Blob", new Static_Units.Blob().value());
            items.Add("Fire Spirit", new Static_Units.FireSpirit().value());
            items.Add("Mecha Golem", new Static_Units.MechaGolem().value());
        }
    }
}
