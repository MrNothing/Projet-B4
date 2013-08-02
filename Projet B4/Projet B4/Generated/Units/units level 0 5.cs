using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4.Static_Units
{
    public class Blob //blobs 1-3
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 1;
            infos.baseStats.sta = 1;
            infos.baseStats.str = 1;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 1;
            infos.model = "Blob";
            return infos;
        }
    }

    public class FireSpirit //FireSpirits 3-5
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 3;
            infos.baseStats.sta = 3;
            infos.baseStats.str = 3;
            infos.baseStats.intel = 3;
            infos.baseStats.sou = 3;
            infos.model = "Fire Spirit";
            return infos;
        }
    }

    public class Wolf //Woves 5-7
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 5;
            infos.baseStats.sta = 5;
            infos.baseStats.str = 5;
            infos.baseStats.intel = 5;
            infos.baseStats.sou = 5;
            infos.model = "Wolf";
            infos.level = 6;
            return infos;
        }
    }

    public class Skeleton //skeletons 7-9
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 7;
            infos.baseStats.sta = 7;
            infos.baseStats.str = 7;
            infos.baseStats.intel = 7;
            infos.baseStats.sou = 7;
            infos.model = "Skeleton";
            infos.level = 7;
            return infos;
        }
    }

    public class WoodenGolem //WoodenGolems 9-11
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 9;
            infos.baseStats.sta = 9;
            infos.baseStats.str = 9;
            infos.baseStats.intel = 9;
            infos.baseStats.sou = 9;
            infos.model = "WoodenGolem";
            infos.level = 9;
            return infos;
        }
    }

    public class MechaGolem //MechaGolems 11-13
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 11;
            infos.baseStats.sta = 11;
            infos.baseStats.str = 11;
            infos.baseStats.intel = 11;
            infos.baseStats.sou = 11;
            infos.model = "Golem";
            infos.level = 11;
            return infos;
        }
    }

    public class Wich //Wiches 13-15
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 13;
            infos.baseStats.sta = 13;
            infos.baseStats.str = 13;
            infos.baseStats.intel = 13;
            infos.baseStats.sou = 13;
            infos.model = "Wich";
            infos.level = 13;
            return infos;
        }
    }

    public class WarGolem //WarGolems 15-17
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 15;
            infos.baseStats.sta = 15;
            infos.baseStats.str = 15;
            infos.baseStats.intel = 15;
            infos.baseStats.sou = 15;
            infos.model = "WarGolem";
            infos.level = 15;
            return infos;
        }
    }

    public class LavaGolem //LavaGolems 17-19
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 17;
            infos.baseStats.sta = 17;
            infos.baseStats.str = 17;
            infos.baseStats.intel = 17;
            infos.baseStats.sou = 17;
            infos.model = "LavaGolem";
            infos.level = 17;
            return infos;
        }
    }

    public class SeaGod //SeaGods 20
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 20;
            infos.baseStats.sta = 20*5;
            infos.baseStats.str = 20;
            infos.baseStats.intel = 20;
            infos.baseStats.sou = 20;
            infos.model = "SeaGod";
            infos.level = 20;
            return infos;
        }
    }

    public class Lever
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 15;
            infos.baseStats.sta = 15;
            infos.baseStats.str = 15;
            infos.baseStats.intel = 15;
            infos.baseStats.sou = 15;
            infos.model = "Lever";
            return infos;
        }
    }

    public class Button
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 15;
            infos.baseStats.sta = 15;
            infos.baseStats.str = 15;
            infos.baseStats.intel = 15;
            infos.baseStats.sou = 15;
            infos.model = "Button";
            return infos;
        }
    }

    public class MillHelice
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 15;
            infos.baseStats.sta = 15;
            infos.baseStats.str = 15;
            infos.baseStats.intel = 15;
            infos.baseStats.sou = 15;
            infos.model = "MillHelice";
            return infos;
        }
    }

    public class BobDeLaLune
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 300;
            infos.baseStats.sta = 300;
            infos.baseStats.str = 300;
            infos.baseStats.intel = 300;
            infos.baseStats.sou = 300;
            infos.model = "BobDeLaLune";
            return infos;
        }
    }

    public class TikTik
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 50;
            infos.baseStats.sta = 100;
            infos.baseStats.str = 50;
            infos.baseStats.intel = 300;
            infos.baseStats.sou = 200;
            infos.model = "TikTik";
            infos.level = 20;
            return infos;
        }
    }

    

    
}
