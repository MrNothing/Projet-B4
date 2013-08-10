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
            infos.range = 2;
            return infos;
        }
    }

    public class IceSpirit //FireSpirits 3-5
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 30;
            infos.baseStats.sta = 55;
            infos.baseStats.str = 55;
            infos.baseStats.intel = 200;
            infos.baseStats.sou = 333;
            infos.range = 20;
            string[] spells = { "iceBall", "iceBall", "iceRain"};
            infos.spells = spells;

            infos.model = "IceSpirit";
            infos.level = 30;
            return infos;
        }
    }

    public class Horse1 //Horse1
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 10;
            infos.baseStats.sta = 10;
            infos.baseStats.str = 10;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 10;

            infos.model = "Horse1";
            infos.level = 10;
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
            infos.range = 2;
            infos.level = 7;
            return infos;
        }
    }

    public class WoodenGolem //WoodenGolems 9-11
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 25;
            infos.baseStats.sta = 100;
            infos.baseStats.str = 150;
            infos.baseStats.intel = 9;
            infos.baseStats.sou = 55;
            infos.model = "Ent";
            infos.level = 40;
            infos.range = 5;
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

    public class Witch //Wiches 13-15
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
            infos.baseStats.agi = 40;
            infos.baseStats.sta = 300;
            infos.baseStats.str = 344;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 122;
            infos.model = "LavaGolem";
            infos.level = 55;
            infos.range = 5;
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

    //crawler
    public class Crawler
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 30;
            infos.baseStats.sta = 50;
            infos.baseStats.str = 40;
            infos.baseStats.intel = 30;
            infos.baseStats.sou = 30;
            infos.model = "Crawler";
            infos.level = 20;
            return infos;
        }
    }

    //Balrog
    public class Balrog
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 20;
            infos.baseStats.sta = 45;
            infos.baseStats.str = 30;
            infos.baseStats.intel = 20;
            infos.baseStats.sou = 20;
            infos.model = "Balrog";
            infos.level = 15;
            infos.range = 3;
            return infos;
        }
    }

    //BlackDrake
    public class BlackDrake
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 70;
            infos.baseStats.sta = 250;
            infos.baseStats.str = 250;
            infos.baseStats.intel = 55;
            infos.baseStats.sou = 99;
            infos.model = "BlackDragon";
            infos.level = 60;
            infos.range = 8;
            infos.ridable = true;
            return infos;
        }
    }
}
