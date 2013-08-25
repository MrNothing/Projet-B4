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
            string[] spells = { "iceBall", "iceRain", "iceBall" };
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

    public class Wolf //Woves 3-7
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 3;
            infos.baseStats.sta = 3;
            infos.baseStats.str = 3;
            infos.baseStats.intel = 3;
            infos.baseStats.sou = 3;
            infos.model = "Wolf1";
            infos.level = 3;
            return infos;
        }
    }

    public class WhiteWolf //Woves 6
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 6;
            infos.baseStats.sta = 6;
            infos.baseStats.str = 6;
            infos.baseStats.intel = 6;
            infos.baseStats.sou = 6;
            infos.model = "Wolf2";
            infos.level = 6;
            return infos;
        }
    }

    public class SpiritWolf //Woves 6
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 75;
            infos.baseStats.sta = 33;
            infos.baseStats.str = 25;
            infos.baseStats.intel = 5;
            infos.baseStats.sou = 5;
            infos.model = "Wolf3";
            infos.level = 20;
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
            infos.baseStats.intel = 2;
            infos.baseStats.sou = 20;
            infos.model = "Ent";
            infos.level = 40;
            infos.range = 5;
            string[] spells = { "thunderClap"};
            infos.spells = spells;
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

    /*public class WarGolem //WarGolems 15-17
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
    }*/

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
            infos.model = "Ancient Lich";
            infos.level = 20;
            infos.range = 20;
            string[] spells = { "iceBolt", "iceBall", "iceRain", "iceBall" };
            infos.spells = spells;

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
            infos.range = 30;
            string[] spells = { "fireBall", "fireBall" };
            infos.spells = spells;
            infos.ridable = true;
            return infos;
        }
    }

    //Guard
    public class Guard
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 100;
            infos.baseStats.sta = 290;
            infos.baseStats.str = 110;
            infos.baseStats.intel = 55;
            infos.baseStats.sou = 110;
            infos.model = "Guard";
            infos.level = 55;
            infos.range = 3;
            return infos;
        }
    }

    //Ancien Guard
    public class AncienGuard
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 15*3;
            infos.baseStats.sta = 15*9;
            infos.baseStats.str = 15*9;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 0;
            infos.model = "Ancien Guard";
            infos.level = 15;
            infos.range = 3;
            return infos;
        }
    }

    //Ancient Warrior
    public class AncienWarrior
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 14 * 3;
            infos.baseStats.sta = 14 * 8;
            infos.baseStats.str = 14 * 8;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 0;
            infos.model = "Ancien Warrior";
            infos.level = 14;
            infos.range = 3;
            return infos;
        }
    }

    //MudGolem
    public class MudGolem
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 17 * 3;
            infos.baseStats.sta = 17 * 3;
            infos.baseStats.str = 17 * 3;
            infos.baseStats.intel = 1;
            infos.baseStats.sou = 0;
            infos.model = "MudGolem";
            infos.level = 17;
            infos.range = 4;
            return infos;
        }
    }

    //Rakar
    public class Rakar
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 100;
            infos.baseStats.sta = 16 * 15;
            infos.baseStats.str = 16 * 15;
            infos.baseStats.intel = 2;
            infos.baseStats.sou = 20;
            infos.model = "Rakar";
            string[] spells = { "thunderClap" };
            infos.level = 16;
            infos.range = 5;
            infos.isBoss = true;
            //TODO: infos.forceDrop = {...}
            return infos;
        }
    }

    //Rukus
    public class Rukus
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 100;
            infos.baseStats.sta = 16 * 10;
            infos.baseStats.str = 16 * 8;
            infos.baseStats.intel = 16*30;
            infos.baseStats.sou = 0;
            infos.model = "Rukus";
            string[] spells = { "fireBall", "fireBall", "fireRain" };
            infos.spells = spells;
            infos.level = 16;
            infos.range = 30;
            
            return infos;
        }
    }

    //War Golem
    public class WarGolem
    {
        public EntityInfos value()
        {
            EntityInfos infos = new EntityInfos();
            infos.baseStats.agi = 18*10;
            infos.baseStats.sta = 18 * 10;
            infos.baseStats.str = 18 * 15;
            infos.baseStats.intel = 16 * 30;
            infos.baseStats.sou = 0;
            infos.model = "War Golem";
            infos.level = 18;
            infos.range = 4;

            return infos;
        }
    }
}
