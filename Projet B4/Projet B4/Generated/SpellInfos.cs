using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class SpellInfos
    {
        public Hashtable allSpells = new Hashtable();

        public Hashtable fireBall = new Hashtable();
        public Hashtable fireRain = new Hashtable();
        public Hashtable iceRain = new Hashtable();
        public Hashtable cataclysm = new Hashtable();
        public Hashtable teleport = new Hashtable();
        public Hashtable spiritualShout = new Hashtable();
        public Hashtable criticalStrike = new Hashtable();
        public Hashtable Spikes = new Hashtable();
        public Hashtable avatar = new Hashtable();
        public Hashtable boneDance = new Hashtable();
        public Hashtable Reanimation = new Hashtable();
        public Hashtable SoulShield = new Hashtable();
        public Hashtable MoonlightArt = new Hashtable();
        public Hashtable WavesofEden = new Hashtable();
        public Hashtable HealthRegen = new Hashtable();
        public Hashtable iceBolt = new Hashtable();
        public Hashtable iceBall = new Hashtable();
        public Hashtable quickStrike = new Hashtable();
        public Hashtable EnergyBall = new Hashtable();
        public Hashtable thunderClap = new Hashtable();
        public Hashtable HighKick = new Hashtable();

        public Hashtable MirrorImage = new Hashtable();
        public Hashtable Shuriken = new Hashtable();

        public SpellInfos()
        {
            //HealthRegen

            HealthRegen.Add("rank", 1);
            HealthRegen.Add("sp_period", 1);
            HealthRegen.Add("maxlvl", 5);
            HealthRegen.Add("cd", 0);
            HealthRegen.Add("mana", 15);

            HealthRegen.Add("manaPerRank", 15);

            HealthRegen.Add("icon", "HealthRegen");
            HealthRegen.Add("name", "HealthRegen");
            HealthRegen.Add("range", 60);
            HealthRegen.Add("zone", 0);
            HealthRegen.Add("baseEffect", 80);
            HealthRegen.Add("coolDown", 5); //in sec
            HealthRegen.Add("incant", 2000); //in ms
            HealthRegen.Add("canalisation", 0); //in ms

            HealthRegen.Add("targets", "ally"); //ally, foe, any
            HealthRegen.Add("targetType", "all"); //all, unit, hero, mechanical
            HealthRegen.Add("usage", "target");

            HealthRegen.Add("id", "HealthRegen");
            allSpells.Add("HealthRegen", HealthRegen);

            //fireball

            fireBall.Add("rank", 1);
            fireBall.Add("sp_period", 1);
            fireBall.Add("maxlvl", 5);
            fireBall.Add("cd", 0);
            fireBall.Add("mana", 15);

            fireBall.Add("manaPerRank", 15);

            fireBall.Add("range", 60);
            fireBall.Add("zone", 0);
            fireBall.Add("baseEffect", 80);
            fireBall.Add("coolDown", 10); //in sec
            fireBall.Add("incant", 500); //in ms
            fireBall.Add("canalisation", 0); //in ms

            fireBall.Add("icon", "fireBall");
            fireBall.Add("name", "fireBall");
            fireBall.Add("description", "Throws a fireball at the target, dealing ??? magic damage and slowing the target for 2 seconds.");
            fireBall.Add("targets", "foe");
            fireBall.Add("targetType", "all");
            fireBall.Add("usage", "target");

            fireBall.Add("id", "fireBall");
            allSpells.Add("fireBall", fireBall);

            //thunderClap

            thunderClap.Add("rank", 1);
            thunderClap.Add("sp_period", 1);
            thunderClap.Add("maxlvl", 5);
            thunderClap.Add("cd", 0);
            thunderClap.Add("mana", 15);

            thunderClap.Add("manaPerRank", 15);

            thunderClap.Add("range", 60);
            thunderClap.Add("zone", 0);
            thunderClap.Add("baseEffect", 80);
            thunderClap.Add("coolDown", 100); //in sec
            thunderClap.Add("incant", 500); //in ms
            thunderClap.Add("canalisation", 0); //in ms

            thunderClap.Add("icon", "thunderClap");
            thunderClap.Add("name", "thunderClap");
            thunderClap.Add("description", "thunderClap");
            thunderClap.Add("targets", "foe");
            thunderClap.Add("targetType", "all");
            thunderClap.Add("usage", "self");

            thunderClap.Add("id", "thunderClap");
            allSpells.Add("thunderClap", thunderClap);

            //HighKick

            HighKick.Add("rank", 1);
            HighKick.Add("sp_period", 1);
            HighKick.Add("maxlvl", 5);
            HighKick.Add("cd", 0);
            HighKick.Add("mana", 0);

            HighKick.Add("manaPerRank", 15);

            HighKick.Add("range", 30);
            HighKick.Add("zone", 0);
            HighKick.Add("baseEffect", 80);
            HighKick.Add("coolDown", 40); //in sec
            HighKick.Add("incant", 0); //in ms
            HighKick.Add("canalisation", 0); //in ms

            HighKick.Add("icon", "HighKick");
            HighKick.Add("name", "HighKick");
            HighKick.Add("description", "HighKick");
            HighKick.Add("targets", "foe");
            HighKick.Add("targetType", "all");
            HighKick.Add("usage", "target");

            HighKick.Add("id", "HighKick");
            allSpells.Add("HighKick", HighKick);

            //Shuriken

            Shuriken.Add("rank", 1);
            Shuriken.Add("sp_period", 1);
            Shuriken.Add("maxlvl", 5);
            Shuriken.Add("cd", 0);
            Shuriken.Add("mana", 0);

            Shuriken.Add("manaPerRank", 0);

            Shuriken.Add("range", 30);
            Shuriken.Add("zone", 0);
            Shuriken.Add("baseEffect", 80);
            Shuriken.Add("coolDown", 5); //in sec
            Shuriken.Add("incant", 500); //in ms
            Shuriken.Add("canalisation", 0); //in ms

            Shuriken.Add("icon", "Shuriken");
            Shuriken.Add("name", "Shuriken");
            Shuriken.Add("description", "Shuriken");
            Shuriken.Add("targets", "foe");
            Shuriken.Add("targetType", "all");
            Shuriken.Add("usage", "target");

            Shuriken.Add("id", "Shuriken");
            allSpells.Add("Shuriken", Shuriken);

            //MirrorImage
            //there should be an energy bar...
            
            MirrorImage.Add("rank", 1);
            MirrorImage.Add("sp_period", 1);
            MirrorImage.Add("maxlvl", 5);
            MirrorImage.Add("cd", 0);
            MirrorImage.Add("mana", 0);

            MirrorImage.Add("manaPerRank", 0);

            MirrorImage.Add("range", 30);
            MirrorImage.Add("zone", 0);
            MirrorImage.Add("baseEffect", 80);
            MirrorImage.Add("coolDown", 10 * 180); //in sec
            MirrorImage.Add("incant", 5000); //in ms
            MirrorImage.Add("canalisation", 0); //in ms

            MirrorImage.Add("icon", "MirrorImage");
            MirrorImage.Add("name", "MirrorImage");
            MirrorImage.Add("description", "MirrorImage");
            MirrorImage.Add("targets", "foe");
            MirrorImage.Add("targetType", "all");
            MirrorImage.Add("usage", "self");

            MirrorImage.Add("id", "MirrorImage");
            allSpells.Add("MirrorImage", MirrorImage);

            //EnergyBall

            EnergyBall.Add("rank", 1);
            EnergyBall.Add("sp_period", 1);
            EnergyBall.Add("maxlvl", 5);
            EnergyBall.Add("cd", 0);
            EnergyBall.Add("mana", 15);

            EnergyBall.Add("manaPerRank", 15);

            EnergyBall.Add("range", 60);
            EnergyBall.Add("zone", 0);
            EnergyBall.Add("baseEffect", 80);
            EnergyBall.Add("coolDown", 10); //in sec
            EnergyBall.Add("incant", 500); //in ms
            EnergyBall.Add("canalisation", 0); //in ms

            EnergyBall.Add("icon", "EnergyBall");
            EnergyBall.Add("name", "EnergyBall");
            EnergyBall.Add("description", "EnergyBall");
            EnergyBall.Add("targets", "foe");
            EnergyBall.Add("targetType", "all");
            EnergyBall.Add("usage", "target");

            EnergyBall.Add("id", "EnergyBall");
            allSpells.Add("EnergyBall", EnergyBall);

            //quickStrike
            quickStrike.Add("rank", 1);
            quickStrike.Add("sp_period", 1);
            quickStrike.Add("maxlvl", 5);
            quickStrike.Add("cd", 0);
            quickStrike.Add("mana", 0);

            quickStrike.Add("manaPerRank", 0);

            quickStrike.Add("range", 60);
            quickStrike.Add("zone", 0);
            quickStrike.Add("baseEffect", 80);
            quickStrike.Add("coolDown", 40); //in sec
            quickStrike.Add("incant", 500); //in ms
            quickStrike.Add("canalisation", 0); //in ms

            quickStrike.Add("icon", "quickStrike");
            quickStrike.Add("name", "quickStrike");
            quickStrike.Add("description", "");
            quickStrike.Add("targets", "foe");
            quickStrike.Add("targetType", "all");
            quickStrike.Add("usage", "target");

            quickStrike.Add("id", "quickStrike");
            allSpells.Add("quickStrike", quickStrike);

            //iceBall

            iceBall.Add("rank", 1);
            iceBall.Add("sp_period", 1);
            iceBall.Add("maxlvl", 5);
            iceBall.Add("cd", 0);
            iceBall.Add("mana", 15);

            iceBall.Add("manaPerRank", 15);

            iceBall.Add("range", 60);
            iceBall.Add("zone", 0);
            iceBall.Add("baseEffect", 80);
            iceBall.Add("coolDown", 5); //in sec
            iceBall.Add("incant", 1000); //in ms
            iceBall.Add("canalisation", 0); //in ms

            iceBall.Add("icon", "iceBall");
            iceBall.Add("name", "iceBall");
            iceBall.Add("description", "");
            iceBall.Add("targets", "foe");
            iceBall.Add("targetType", "all");
            iceBall.Add("usage", "target");

            iceBall.Add("id", "iceBall");
            allSpells.Add("iceBall", iceBall);

            //iceBolt

            iceBolt.Add("rank", 1);
            iceBolt.Add("sp_period", 1);
            iceBolt.Add("maxlvl", 5);
            iceBolt.Add("cd", 0);
            iceBolt.Add("mana", 80);

            iceBolt.Add("manaPerRank", 30);

            iceBolt.Add("range", 60);
            iceBolt.Add("zone", 0);
            iceBolt.Add("baseEffect", 80);
            iceBolt.Add("coolDown", 300); // = 30s
            iceBolt.Add("incant", 4000); //in ms
            iceBolt.Add("canalisation", 0); //in ms

            iceBolt.Add("icon", "iceBolt");
            iceBolt.Add("name", "iceBolt");
            iceBolt.Add("description", "");
            iceBolt.Add("targets", "foe");
            iceBolt.Add("targetType", "all");
            iceBolt.Add("usage", "target");

            iceBolt.Add("id", "iceBolt");
            allSpells.Add("iceBolt", iceBolt);

            //cataclysm

            cataclysm.Add("rank", 1);
            cataclysm.Add("sp_period", 6);
            cataclysm.Add("maxlvl", 3);
            cataclysm.Add("cd", 0);
            cataclysm.Add("mana", 225);
            cataclysm.Add("manaPerRank", 150);

            cataclysm.Add("range", 15);
            cataclysm.Add("zone", 5);
            cataclysm.Add("baseEffect", 0);
            cataclysm.Add("coolDown", 80); //in sec
            cataclysm.Add("incant", 1000); //in ms
            cataclysm.Add("canalisation", 0); //in ms

            cataclysm.Add("icon", "cataclysm");
            cataclysm.Add("name", "cataclysm");
            cataclysm.Add("description", "A huge ball of energy falls from the sky, dealing ??? magic damage to all the ennemies in a zone. \n For each enemy killed by the spell, a fire spirit is raised from the corpse to serve you.");
            cataclysm.Add("targets", "foe");
            cataclysm.Add("targetType", "all");
            cataclysm.Add("usage", "target");

            cataclysm.Add("id", "cataclysm");
            allSpells.Add("cataclysm", cataclysm);

            //fire rain
            fireRain.Add("rank", 1);
            fireRain.Add("sp_period", 1);
            fireRain.Add("maxlvl", 5);
            fireRain.Add("cd", 0);
            fireRain.Add("mana", 110);
            fireRain.Add("manaPerRank", 55);

            fireRain.Add("range", 30);
            fireRain.Add("zone", 20);
            fireRain.Add("baseEffect", 0);
            fireRain.Add("coolDown", 25); //in sec
            fireRain.Add("incant", 25); //in ms
            fireRain.Add("canalisation", 0); //in ms

            fireRain.Add("icon", "fireRain");
            fireRain.Add("name", "fireRain");
            fireRain.Add("description", "Waves of fire fall from the sky, dealing ??? damages per wave to the enemies in a zone.");
            fireRain.Add("targets", "foe");
            fireRain.Add("targetType", "all");
            fireRain.Add("usage", "target");

            fireRain.Add("id", "fireRain");
            allSpells.Add("fireRain", fireRain);

            //iceRain
            iceRain.Add("rank", 1);
            iceRain.Add("sp_period", 1);
            iceRain.Add("maxlvl", 5);
            iceRain.Add("cd", 0);
            iceRain.Add("mana", 110);
            iceRain.Add("manaPerRank", 55);

            iceRain.Add("range", 30);
            iceRain.Add("zone", 20);
            iceRain.Add("baseEffect", 0);
            iceRain.Add("coolDown", 25); //in sec
            iceRain.Add("incant", 25); //in ms
            iceRain.Add("canalisation", 0); //in ms

            iceRain.Add("icon", "iceRain");
            iceRain.Add("name", "iceRain");
            iceRain.Add("description", "");
            iceRain.Add("targets", "foe");
            iceRain.Add("targetType", "all");
            iceRain.Add("usage", "target");

            iceRain.Add("id", "iceRain");
            allSpells.Add("iceRain", iceRain);

            //Spiritual Shout

            spiritualShout.Add("rank", 1);
            spiritualShout.Add("sp_period", 1);
            spiritualShout.Add("maxlvl", 5);
            spiritualShout.Add("cd", 0);
            spiritualShout.Add("mana", 50);
            spiritualShout.Add("manaPerRank", 25);

            spiritualShout.Add("range", 7);
            spiritualShout.Add("zone", 3);
            spiritualShout.Add("baseEffect", 0);
            spiritualShout.Add("coolDown", 30); //in sec
            spiritualShout.Add("incant", 500); //in ms
            spiritualShout.Add("canalisation", 0); //in ms

            spiritualShout.Add("icon", "spiritualShout");
            spiritualShout.Add("name", "Spiritual Shout");
            spiritualShout.Add("description", "Knocks back every ennemy in front of you.");
            spiritualShout.Add("targets", "foe");
            spiritualShout.Add("targetType", "all");
            spiritualShout.Add("usage", "target");
            spiritualShout.Add("isCone", true);


            spiritualShout.Add("id", "spiritualShout");
            allSpells.Add("spiritualShout", spiritualShout);

            //Critical Strike

            criticalStrike.Add("rank", 1);
            criticalStrike.Add("sp_period", 1);
            criticalStrike.Add("maxlvl", 5);
            criticalStrike.Add("cd", 0);
            criticalStrike.Add("mana", 0);
            criticalStrike.Add("manaPerRank", 0);

            criticalStrike.Add("range", 0);
            criticalStrike.Add("zone", 0);
            criticalStrike.Add("baseEffect", 0);
            criticalStrike.Add("coolDown", 0); //in sec
            criticalStrike.Add("incant", 0); //in ms
            criticalStrike.Add("canalisation", 0); //in ms

            criticalStrike.Add("icon", "CriticalStrike");
            criticalStrike.Add("name", "Critical Strike");
            // criticalStrike.Add("description", "Knocks back every ennemy in front of you.");
            criticalStrike.Add("targets", "foe");
            criticalStrike.Add("targetType", "all");
            criticalStrike.Add("usage", "passive");

            criticalStrike.Add("id", "CriticalStrike");
            allSpells.Add("CriticalStrike", criticalStrike);

            //Spikes

            Spikes.Add("rank", 0);
            Spikes.Add("sp_period", 1);
            Spikes.Add("maxlvl", 5);
            Spikes.Add("cd", 0);
            Spikes.Add("mana", 0);
            Spikes.Add("manaPerRank", 0);

            Spikes.Add("range", 0);
            Spikes.Add("zone", 0);
            Spikes.Add("baseEffect", 0);
            Spikes.Add("coolDown", 0); //in sec
            Spikes.Add("incant", 0); //in ms
            Spikes.Add("canalisation", 0); //in ms

            Spikes.Add("icon", "Spikes");
            Spikes.Add("name", "Spikes");
            // criticalStrike.Add("description", "Knocks back every ennemy in front of you.");
            Spikes.Add("targets", "foe");
            Spikes.Add("targetType", "all");
            Spikes.Add("usage", "passive");

            Spikes.Add("id", "Spikes");
            allSpells.Add("Spikes", Spikes);

            //teleport

            teleport.Add("rank", 0);
            teleport.Add("sp_period", 6);
            teleport.Add("maxlvl", 3);
            teleport.Add("cd", 0);
            teleport.Add("mana", 300);
            teleport.Add("manaPerRank", 150);

            teleport.Add("range", 250);
            teleport.Add("zone", 3);
            teleport.Add("baseEffect", 0);
            teleport.Add("coolDown", 80); //in sec
            teleport.Add("incant", 1000); //in ms
            teleport.Add("canalisation", 0); //in ms

            teleport.Add("icon", "teleport");
            teleport.Add("name", "Teleport");
            teleport.Add("description", "Teleports you to the specified Zone.");
            teleport.Add("targets", "foe");
            teleport.Add("targetType", "all");
            teleport.Add("usage", "target");

            teleport.Add("id", "teleport");
            allSpells.Add("teleport", teleport);

            //Avatar

            avatar.Add("rank", 0);
            avatar.Add("sp_period", 6);
            avatar.Add("maxlvl", 3);
            avatar.Add("cd", 0);
            avatar.Add("mana", 150);
            avatar.Add("manaPerRank", 50);

            avatar.Add("range", 0);
            avatar.Add("zone", 0);
            avatar.Add("baseEffect", 0);
            avatar.Add("coolDown", 60); //in sec
            avatar.Add("incant", 500); //in ms
            avatar.Add("canalisation", 0); //in ms

            avatar.Add("icon", "avatar");
            avatar.Add("name", "Avatar");
            avatar.Add("description", "Turns you into a giant for 25 seconds. You gain health, resistance, armor, and damage during the effect.");
            avatar.Add("targets", "foe");
            avatar.Add("targetType", "all");
            avatar.Add("usage", "self");

            avatar.Add("id", "avatar");
            allSpells.Add("avatar", avatar);

            //Bone Dance

            boneDance.Add("rank", 0);
            boneDance.Add("sp_period", 6);
            boneDance.Add("maxlvl", 3);
            boneDance.Add("cd", 0);
            boneDance.Add("mana", 220);
            boneDance.Add("manaPerRank", 110);

            boneDance.Add("range", 8);
            boneDance.Add("zone", 0);
            boneDance.Add("baseEffect", 0);
            boneDance.Add("coolDown", 60); //in sec
            boneDance.Add("incant", 500); //in ms
            boneDance.Add("canalisation", 0); //in ms

            boneDance.Add("icon", "boneDance");
            boneDance.Add("name", "Bone Dance");
            boneDance.Add("description", "??");
            boneDance.Add("targets", "foe");
            boneDance.Add("targetType", "all");
            boneDance.Add("usage", "self");

            boneDance.Add("id", "boneDance");
            allSpells.Add("boneDance", boneDance);

            //Reanimation

            Reanimation.Add("rank", 0);
            Reanimation.Add("sp_period", 1);
            Reanimation.Add("maxlvl", 5);
            Reanimation.Add("cd", 0);
            Reanimation.Add("mana", 90);
            Reanimation.Add("manaPerRank", 20);

            Reanimation.Add("range", 10);
            Reanimation.Add("zone", 0);
            Reanimation.Add("baseEffect", 0);
            Reanimation.Add("coolDown", 30); //in sec
            Reanimation.Add("incant", 500); //in ms
            Reanimation.Add("canalisation", 0); //in ms

            Reanimation.Add("icon", "reanimation");
            Reanimation.Add("name", "Reanimation");
            Reanimation.Add("description", "??");
            Reanimation.Add("targets", "foe");
            Reanimation.Add("targetType", "all");
            Reanimation.Add("usage", "self");

            Reanimation.Add("id", "Reanimation");
            allSpells.Add("Reanimation", Reanimation);

            //Soul Shield

            SoulShield.Add("rank", 0);
            SoulShield.Add("sp_period", 1);
            SoulShield.Add("maxlvl", 5);
            SoulShield.Add("cd", 0);
            SoulShield.Add("mana", 60);
            SoulShield.Add("manaPerRank", 5);

            SoulShield.Add("range", 0);
            SoulShield.Add("zone", 0);
            SoulShield.Add("baseEffect", 0);
            SoulShield.Add("coolDown", 40); //in sec
            SoulShield.Add("incant", 500); //in ms
            SoulShield.Add("canalisation", 0); //in ms

            SoulShield.Add("icon", "SoulShield");
            SoulShield.Add("name", "Soul Shield");
            SoulShield.Add("description", "??");
            SoulShield.Add("targets", "foe");
            SoulShield.Add("targetType", "all");
            SoulShield.Add("usage", "self");

            SoulShield.Add("id", "SoulShield");
            allSpells.Add("SoulShield", SoulShield);


            //MoonlightArt

            MoonlightArt.Add("rank", 0);
            MoonlightArt.Add("sp_period", 6);
            MoonlightArt.Add("maxlvl", 3);
            MoonlightArt.Add("cd", 0);
            MoonlightArt.Add("mana", 60);
            MoonlightArt.Add("manaPerRank", 5);

            MoonlightArt.Add("range", 0);
            MoonlightArt.Add("zone", 0);
            MoonlightArt.Add("baseEffect", 0);
            MoonlightArt.Add("coolDown", 120); //in sec
            MoonlightArt.Add("incant", 500); //in ms
            MoonlightArt.Add("canalisation", 0); //in ms

            MoonlightArt.Add("icon", "MoonlightArt");
            MoonlightArt.Add("name", "Moonlight Art");
            MoonlightArt.Add("description", "??");
            MoonlightArt.Add("targets", "foe");
            MoonlightArt.Add("targetType", "all");
            MoonlightArt.Add("usage", "self");

            MoonlightArt.Add("id", "MoonlightArt");
            allSpells.Add("MoonlightArt", MoonlightArt);

            //Waves of Eden

            WavesofEden.Add("rank", 0);
            WavesofEden.Add("sp_period", 6);
            WavesofEden.Add("maxlvl", 3);
            WavesofEden.Add("cd", 0);
            WavesofEden.Add("mana", 220);
            WavesofEden.Add("manaPerRank", 30);

            WavesofEden.Add("range", 12);
            WavesofEden.Add("zone", 6);
            WavesofEden.Add("baseEffect", 0);
            WavesofEden.Add("coolDown", 120); //in sec
            WavesofEden.Add("incant", 500); //in ms
            WavesofEden.Add("canalisation", 0); //in ms

            WavesofEden.Add("icon", "WavesofEden");
            WavesofEden.Add("name", "Waves of Eden");
            WavesofEden.Add("description", "??");
            WavesofEden.Add("targets", "foe");
            WavesofEden.Add("targetType", "all");
            WavesofEden.Add("usage", "target");
            WavesofEden.Add("isCone", true);

            WavesofEden.Add("id", "WavesofEden");
            allSpells.Add("WavesofEden", WavesofEden);

            
        }
    }
}
