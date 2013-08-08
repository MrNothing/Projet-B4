using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class SpellsManager
    {
        public GameCode mainInstance;
        public SpellsManager(GameCode _mainInstance)
        {
            mainInstance = _mainInstance;
        }

        public void applyPassiveEffects(Entity caster, String spellId)
        {
            Hashtable mySpell = (Hashtable)caster.spells[spellId];

            if (mySpell["name"].Equals("Critical Strike"))
            {
                if((int)mySpell["rank"]>1)
                {
                    caster.infos.vitalInfosBon.crit -= (5 + ((int)mySpell["rank"]-2) * 2.5f);
                    caster.infos.vitalInfosBon.critBon -= (10 + ((int)mySpell["rank"]-2) * 5f);
                }

                caster.infos.vitalInfosBon.crit += 5 + ((int)mySpell["rank"] - 1) * 2.5f;
                caster.infos.vitalInfosBon.critBon += 10 + ((int)mySpell["rank"] - 1) * 5f;

                //caster.getMyOwner().Send("_err", caster.infos.vitalInfosBon.crit);
            }

            if (mySpell["name"].Equals("Spikes"))
            {
                if ((int)mySpell["rank"] > 1)
                {
                    caster.infos.vitalInfosBon.armor -= (5 + ((int)mySpell["rank"] - 2) * 2.5f);
                    caster.infos.specialEffects.spikes -= (10 + ((int)mySpell["rank"] - 2) * 7.5f);
                }

                caster.infos.vitalInfosBon.armor += 5 + ((int)mySpell["rank"] - 1) * 2.5f;
                caster.infos.specialEffects.spikes += 10 + ((int)mySpell["rank"] - 1) * 7.5f;

                //caster.getMyOwner().Send("_err", caster.infos.vitalInfosBon.crit);
            }

            caster.sendDynamicInfosToAll();
            caster.sendInfosToMe();
        }

        public void UseItem(Entity caster, String target, String spellId, float zoneX, float zoneY, float zoneZ)
	    {
            if (caster.hp <= 0)
            {
                caster.getMyOwner().Send("err", "e2"); //You are dead!
                return;
            }

            if (caster.incantation != null)
            {
                caster.getMyOwner().Send("err", "s9"); //You are already incanting!
                return;
            }

            if (caster.canalisedSpell != null)
            {
                caster.getMyOwner().Send("err", "s10"); //You are already channeling!
                return;
            }

		    Item myItem;

            try
            {
                myItem = (Item)caster.items[spellId];
            }
            catch (Exception e)
            { 
                caster.getMyOwner().Send("err", "i6"); //Item not found!
                return;
            }

            if (myItem.infos.advancedType == ItemAdvancedTypes.none || myItem.infos.advancedType == ItemAdvancedTypes.book || myItem.infos.advancedType == ItemAdvancedTypes.spellBook)
		    {
                caster.getMyOwner().Send("err", "i1"); //This item cannot be used that way!
			    //send message: need more mana!
			    return;
		    }
		
		    //System.out.println("Using Item: "+spellId);

            Hashtable mySpell;
            try
            {
                mySpell = (Hashtable)(new SpellInfos()).allSpells[myItem.infos.spell];
                mySpell.Remove("rank");
                mySpell.Add("rank", myItem.infos.spellRank);
            }
            catch(Exception e)
            {
                caster.getMyOwner().Send("err", "The spell associated with this item was not found!"); //"The item is not ready!"
                return;
            }

            if (caster.itemsUsageCd > 0)
		    {
                if (myItem.cooldown > 0)
			    {
                    caster.getMyOwner().Send("err", "i2"); //"The item is not ready!"
				    return;
			    }
		    }

            SpellsCaster spellsCaster = null;

		    if((mySpell["usage"].Equals("target")))
		    {
			
			    if((int)mySpell["zone"]>0)
			    {
				    //proceed...
                    spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], 1, zoneX, zoneY, zoneZ);
			    }
			    else
			    {
				    if(caster.myGame.units[target]==null)
				    {
                        caster.getMyOwner().Send("err", "s1"); //"this target does not exist!"
					    return;
				    }

                    Entity targetUnit = (Entity)caster.myGame.units[target];
				
				    if(mySpell["targets"].Equals("foe") && caster.team.Equals(targetUnit.team))
				    {
                        caster.getMyOwner().Send("err", "s2"); //"wrong target!"
                        return;
				    }
				
				    if(mySpell["targets"].Equals("ally") && !caster.team.Equals(targetUnit.team))
				    {
                        caster.getMyOwner().Send("err", "s2");
                        return;
				    }
				
				    if(mySpell["targetType"].Equals("Hero") && !targetUnit.type.Equals("Hero"))
				    {
                        caster.getMyOwner().Send("err", "s2");
					    //send message: wrong target! -> the target is not a Hero
					    return;
				    }
				
				    if(mySpell["targetType"].Equals("Unit") && !targetUnit.type.Equals("Unit"))
				    {
                        caster.getMyOwner().Send("err", "s2");
                        //send message: wrong target! -> the target is not a Unit
					    return;
				    }
				
				    if(mySpell["targetType"].Equals("living") && targetUnit.type.Equals("building"))
				    {
                        caster.getMyOwner().Send("err", "s2");
                        //send message: wrong target! -> the target is a building
					    return;
				    }
				
				    if(mySpell["targetType"].Equals("building") && !targetUnit.type.Equals("building"))
				    {
                        caster.getMyOwner().Send("err", "s2");
                        //send message: wrong target! -> the target is not a building
					    return;
				    }

                    spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], 1, targetUnit, 0);
			    }
			
		    }
		
		    if((mySpell["usage"].Equals("self")))
		    {
                spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], 1);
		    }
		
		    if(myItem.infos.charges>0)
		    {
                myItem.uses--;
                if (myItem.uses <= 0)
                    ((Hero)caster).destroyItem(spellId);
                else
                { 
                    //send item lost 1 charge   
                    Object[] data = new Object[1];
                    data[0] = spellId; //i

                    caster.getMyOwner().Send("charge", data);
                }
		    }

            caster.itemsUsageCd = 30;
            //caster.sendCast(myItem);
		    //System.out.println("Casting spell: "+(string)mySpell["id"]);
		
	    }

            //target can be random if its not a targeted spell
            public void UseSpell(Entity caster, String target, String spellId, float zoneX, float zoneY, float zoneZ)
            {
                if (caster.hp <= 0)
                {
                    caster.getMyOwner().Send("err", "e2"); //You are dead!
                    return;
                }

                if (caster.incantation!=null)
                {
                    caster.getMyOwner().Send("err", "s9"); //You are already incanting!
                    return;
                }

                if (caster.canalisedSpell != null)
                {
                    caster.getMyOwner().Send("err", "s10"); //You are already channeling!
                    return;
                }

                Hashtable mySpell;

                try 
                {
                    mySpell = (Hashtable)caster.spells[spellId];
                }
                catch (Exception e) 
                {
                    caster.getMyOwner().Send("err", "s5"); //You dont know this spell!
                    return;
                }

                if(((string)mySpell["id"]).Equals("Reanimation"))
                {
                    if (/*caster.visibleCorpses.Count <= 0*/ true)
                    {
                        caster.getMyOwner().Send("err", "e1"); //You cannot do that! there are no corpses around!
                        return;
                    }
                }

                if (caster.mp < ((int)mySpell["mana"] + (int)mySpell["manaPerRank"] * (-1 + (int)mySpell["rank"])))
                {
                    caster.getMyOwner().Send("err", "s3"); //need more mana!
                    //send message: need more mana!
                    return;
                }

                if ((int) mySpell["cd"] > 0)
                {
                    caster.getMyOwner().Send("err", "s4"); //The spell is not ready!
                    //send message: The spell is not ready!
                    return;
                }
               
                if ((int)mySpell["rank"] <= 0)
                {
                    caster.getMyOwner().Send("err", "s5"); //You dont know this spell!
                    //send message: The spell is not rank 1!
                    return;
                }

                SpellsCaster spellsCaster = null;

                if ((mySpell["usage"].Equals("target")))
                {

                    if ((int)mySpell["zone"] > 0)
                    {
                        //proceed...
                        bool isCone = false;

                        try {
                            isCone = (bool)mySpell["isCone"];
                        }
                        catch (Exception e) { }

                        if (caster.getDistance(new Vector3(zoneX, zoneY, zoneZ)) > (int)mySpell["range"] && !isCone)
                        {
                            caster.getMyOwner().Send("err", "s6"); //Out of range!
                            //send message: this target does not exist!
                            return;
                        }
                        spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"], zoneX, zoneY, zoneZ);
                    }
                    else
                    {
                        if (caster.myGame.units[target] == null)
                        {
                            caster.getMyOwner().Send("err", "s1");
                            //send message: this target does not exist!
                            return;
                        }

                        Entity targetUnit = (Entity)caster.myGame.units[target];


                        if (targetUnit.getDistance(caster) > (int)mySpell["range"])
                        {
                                caster.getMyOwner().Send("err", "s6");
                                //send message: out of range!
                                return;
                        }

                        if (mySpell["targets"].Equals("foe") && caster.team.Equals(targetUnit.team))
                        {
                            caster.getMyOwner().Send("err", "s2f");
                            //send message: wrong target! -> the target is in my team
                            return;
                        }

                        if (mySpell["targets"].Equals("ally") && !caster.team.Equals(targetUnit.team))
                        {
                            caster.getMyOwner().Send("err", "s2e");
                            //send message: wrong target! -> the target is not in my team
                            return;
                        }

                        if (mySpell["targetType"].Equals("Hero") && targetUnit.type != EntityType.player)
                        {
                            caster.getMyOwner().Send("err", "s2");
                            //send message: wrong target! -> the target is not a Hero
                            return;
                        }

                        if (mySpell["targetType"].Equals("Unit") && targetUnit.type == EntityType.player)
                        {
                            caster.getMyOwner().Send("err", "s2");
                            //send message: wrong target! -> the target is not a Unit
                            return;
                        }

                        if (mySpell["targetType"].Equals("living") && targetUnit.type.Equals("building"))
                        {
                            caster.getMyOwner().Send("err", "s2");
                            //send message: wrong target! -> the target is a building
                            return;
                        }

                        if (mySpell["targetType"].Equals("building") && !targetUnit.type.Equals("building"))
                        {
                            caster.getMyOwner().Send("err", "s2");
                            //send message: wrong target! -> the target is not a building
                            return;
                        }

                        spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"], targetUnit, 0);
                    }

                }

                if ((mySpell["usage"].Equals("self")))
                {
                    spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"]);
                }

                //System.out.println("Casting spell: "+(string)mySpell["id"]);

                //set spell used...
                mySpell.Remove("cd");
                mySpell.Add("cd", (int)mySpell["coolDown"]);
                caster.mp -= ((int)mySpell["mana"] + (int)mySpell["manaPerRank"] * (-1 + (int)mySpell["rank"]));
                caster.sendDynamicInfosToAll("");

                //send CD infos
                Object[] cdinfos = new Object[1];
                cdinfos[0] = spellId; //i
                caster.getMyOwner().Send("cd", cdinfos);

               /* if ((int)mySpell["zone"] > 0)
                    caster.sendCast(zoneX, zoneY, zoneZ);
                else*/
                //caster.sendCast((string)mySpell["id"], target);
                caster.incantation = mainInstance.ScheduleCallback(spellsCaster.run, (int)mySpell["incant"]);
            }

            //target can be random if its not a targeted spell
            public bool IAUseSpell(Entity caster, String target, String spellId, float zoneX, float zoneY, float zoneZ)
            {
                Hashtable mySpell = (Hashtable)caster.spells[spellId];

                if (caster.mp < ((int)mySpell["mana"] + (int)mySpell["manaPerRank"] * (-1 + (int)mySpell["rank"])))
                {
                    //send message: need more mana!
                    return false;
                }

                if ((int)mySpell["cd"] > 0)
                {
                    //send message: The spell is not ready!
                    return false;
                }

                //set spell used...
                caster.mp -= ((int)mySpell["mana"] + (int)mySpell["manaPerRank"] *(-1 + (int)mySpell["rank"]));
                caster.sendDynamicInfosToAll("");

                SpellsCaster spellsCaster=null;

                if ((mySpell["usage"].Equals("target")))
                {

                    if ((int)mySpell["zone"] > 0)
                    {
                        //proceed...
                        spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"], zoneX, zoneY, zoneZ);

                        return true;
                    }
                    else
                    {
                        if (caster.myGame.units[target] == null)
                        {
                            //send message: this target does not exist!
                            return false;
                        }

                        Entity targetUnit = caster.myGame.units[target];

                        if (mySpell["targets"].Equals("foe") && caster.team.Equals(targetUnit.team))
                        {
                            //send message: wrong target! -> the target is in my team
                            return false;
                        }

                        if (mySpell["targets"].Equals("ally") && !caster.team.Equals(targetUnit.team))
                        {
                            //send message: wrong target! -> the target is not in my team
                            return false;
                        }

                        if (mySpell["targetType"].Equals("Hero") && targetUnit.type!=EntityType.player)
                        {
                            //send message: wrong target! -> the target is not a Hero
                            return false;
                        }

                        if (mySpell["targetType"].Equals("Unit") && targetUnit.type == EntityType.player)
                        {
                            //send message: wrong target! -> the target is not a Unit
                            return false;
                        }

                        if (mySpell["targetType"].Equals("living") && targetUnit.type.Equals("building"))
                        {
                            //send message: wrong target! -> the target is a building
                            return false;
                        }

                        if (mySpell["targetType"].Equals("building") && !targetUnit.type.Equals("building"))
                        {
                            //send message: wrong target! -> the target is not a building
                            return false;
                        }

                        spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"], targetUnit, 0);
                    }

                }

                if ((mySpell["usage"].Equals("self")))
                {
                    spellsCaster = new SpellsCaster(mainInstance, caster, (string)mySpell["id"], (int)mySpell["rank"]);
                }

                caster.incantation = mainInstance.ScheduleCallback(spellsCaster.run, (int)mySpell["incant"]);

                return true;
                //System.out.println("Casting spell: "+(string)mySpell["id"]);

            }
        }

    public enum SpellsTypes
    {
        passive, self, target, zone, cone
    }

    public class SpellsCaster
    {
        public bool noIncant = false;

        SpellsTypes type;
        GameCode mainInstance;
        Entity author;
        String spell;
        int rank;
        float ix;
        float iy;
        float iz;

        string targetId = "";

        public SpellsCaster(GameCode _mainInstance, Entity _author, String _spell, int _rank, float _ix, float _iy, float _iz)
        {
            mainInstance = _mainInstance;
            author = _author;
            spell = _spell;
            rank = _rank;
            ix = _ix;
            iy = _iy;
            iz = _iz;
            type = SpellsTypes.zone;

            initialize();
        }

        Entity target;
        float fixedDmg;
        public SpellsCaster(GameCode _mainInstance, Entity _author, String _spell, int _rank, Entity _target, float _fixedDmg)
        {
            mainInstance = _mainInstance;
            author = _author;
            spell = _spell;
            rank = _rank;
            target = _target;
            fixedDmg = _fixedDmg;
            type = SpellsTypes.target;

            targetId = _target.id;

            initialize();
        }

        public SpellsCaster(GameCode _mainInstance, Entity _author, String _spell, int _rank)
        {
            mainInstance = _mainInstance;
            author = _author;
            spell = _spell;
            rank = _rank;
            type = SpellsTypes.self;

            targetId = _author.id;

            initialize();
        }

        public void initialize()
        {
            if (noIncant)
                return;

            SpellInfos tmpInfos = new SpellInfos();
            Hashtable newSpell = (Hashtable)tmpInfos.allSpells[spell];

            author.sendIncant(spell, (int)newSpell["incant"]);
        }

        public void run()
        {
            author.sendCast(spell, targetId);

            if (type == SpellsTypes.zone)
                castZoneSpell();

            if (type == SpellsTypes.target)
                castTargetSpell();

            if (type == SpellsTypes.self)
                castSelfSpell();

            author.incantation = null;
        }

        void castZoneSpell()
        {

            if (spell.Equals("cataclysm"))
            {
                float dmg = 255f + 110f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.fireBon) * 1.1f;

                delayedZoneMagicDmg myDelayedSpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "fire", ix, iy, iz, 5f, 1);
                myDelayedSpell.period = 1000;
                myDelayedSpell.invokeOnKill = "Fire Spirit";

                mainInstance.ScheduleCallback(myDelayedSpell.run, 1500);

                Object[] infos = new Object[6];
                infos[0] = "cataclysm"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = ix; //x
                infos[3] = iy; //y
                infos[4] = iz; //z

                infos[5] = 0; //waves

                author.myGame.sendDataToAll("z_spell", infos, author);
            }

            if (spell.Equals("teleport"))
            {
                delayedTeleport myTeleport = new delayedTeleport(author);
                myTeleport.x = ix;
                myTeleport.y = iy;
                myTeleport.z = iz;

                mainInstance.ScheduleCallback(myTeleport.run, 2500 - 500 * (rank - 1));

                Object[] infos = new Object[3];
                infos[0] = "Aura1"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = 300 - 60 * (rank - 1); //x

                author.myGame.sendDataToAll("aura", infos, author);

                author.sendCast(300 - 60 * (rank - 1));
            }

            if (spell.Equals("spiritualShout"))
            {
                float dmg = 20 + 15 * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.natureBon) * 0.2f;

                delayedZoneMagicDmg myDelayedSpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "nature", author.position.x, author.position.y, author.position.z, 5f, 1);

                myDelayedSpell.propelValue = 5 + 0.3f * (rank - 1);
                myDelayedSpell.angleLimit = 15;

                myDelayedSpell.run();


                Object[] infos = new Object[4];
                infos[0] = "SpiritualShout"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = ix; //x
                infos[3] = iz; //y

                author.myGame.sendDataToAll("blow", infos, author);
            }

            if (spell.Equals("WavesofEden"))
            {
                float dmg = 250 + 60 * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.natureBon) * 0.2f;

                delayedZoneMagicDmg myDelayedSpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "nature", author.position.x, author.position.y, author.position.z, 5f, 10);

                myDelayedSpell.propelValue = 2f;

                myDelayedSpell.decreaseWithDistance = 1;
                myDelayedSpell.angleLimit = 20;
                myDelayedSpell.period = 500;

                myDelayedSpell.run();


                Object[] infos = new Object[4];
                infos[0] = "WaveOfEden"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = ix; //x
                infos[3] = iz; //y

                author.myGame.sendDataToAll("blow", infos, author);
            }



            if (spell.Equals("fireRain"))
            {
                float dmg = 9f + 13f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.fireBon) * 0.2f;

                delayedZoneMagicDmg mySpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "fire", ix, iy, iz, 5f, 3 + ((int)Math.Floor(rank / 2f)));
                mySpell.period = 1300;
                author.canalisedSpell = mainInstance.ScheduleCallback(mySpell.run, 1000);

                Object[] infos = new Object[7];
                infos[0] = "RainOfFire"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = ix; //x
                infos[3] = iy; //y
                infos[4] = iz; //z

                infos[5] = 3 + ((int)Math.Floor(rank / 2f)); //waves

                infos[6] = mySpell.period;

                author.myGame.sendDataToAll("z_spell", infos, author);
            }

            if (spell.Equals("iceRain"))
            {
                float dmg = 49f + 13f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.iceBon) * 0.2f;

                delayedZoneMagicDmg mySpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "ice", ix, iy, iz, 5f, 3 + ((int)Math.Floor(rank / 2f)));
                mySpell.period = 1300;
                author.canalisedSpell = mainInstance.ScheduleCallback(mySpell.run, 1000);

                Object[] infos = new Object[7];
                infos[0] = "iceRain"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = ix; //x
                infos[3] = iy; //y
                infos[4] = iz; //z

                infos[5] = 3 + ((int)Math.Floor(rank / 2f)); //waves

                infos[6] = mySpell.period;

                author.myGame.sendDataToAll("z_spell", infos, author);
            }
        }

        void castTargetSpell()
        {
            if (spell.Equals("fireBall"))
            {
                float dmg = 12f + 22f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.fireBon) * 0.45f;

                float tx = target.position.x;
                float ty = target.position.y;
                float tz = target.position.z;

                float bruteDistance = ((tx - author.position.x) * (tx - author.position.x) + (ty - author.position.y) * (ty - author.position.y) + (tz - author.position.z) * (tz - author.position.z));
                float distance = (float)Math.Sqrt(bruteDistance);

                float projectileSpeed = 0.2f;

                float timeToHit = (float)(distance / (projectileSpeed * 30f)) * 250; //in ms

                if (timeToHit < 25)
                    timeToHit = 25;

                delayedMagicDmg mySpell = new delayedMagicDmg(author, mainInstance, targetId, dmg, "fire", spell);
                mySpell.period = 0;
                mainInstance.ScheduleCallback(mySpell.run, (int)timeToHit);

                Object[] infos = new Object[3];
                infos[0] = "proj1"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = target.id; //

                author.myGame.sendDataToAll("t_spell", infos, author);
            }

            if (spell.Equals("iceBall"))
            {
                float dmg = 33f + 22f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.iceBon) * 0.45f;

                float tx = target.position.x;
                float ty = target.position.y;
                float tz = target.position.z;

                float bruteDistance = ((tx - author.position.x) * (tx - author.position.x) + (ty - author.position.y) * (ty - author.position.y) + (tz - author.position.z) * (tz - author.position.z));
                float distance = (float)Math.Sqrt(bruteDistance);

                float projectileSpeed = 0.2f;

                float timeToHit = (float)(distance / (projectileSpeed * 30f)) * 250; //in ms

                if (timeToHit < 25)
                    timeToHit = 25;

                delayedMagicDmg mySpell = new delayedMagicDmg(author, mainInstance, targetId, dmg, "ice", spell);
                mySpell.period = 0;
                mainInstance.ScheduleCallback(mySpell.run, (int)timeToHit);

                Object[] infos = new Object[3];
                infos[0] = "iceBall"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = target.id; //

                author.myGame.sendDataToAll("t_spell", infos, author);
            }

            if (spell.Equals("iceBolt"))
            {
                float dmg = 55f + 85f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.iceBon) * 0.45f;

                float tx = target.position.x;
                float ty = target.position.y;
                float tz = target.position.z;

                float bruteDistance = ((tx - author.position.x) * (tx - author.position.x) + (ty - author.position.y) * (ty - author.position.y) + (tz - author.position.z) * (tz - author.position.z));
                float distance = (float)Math.Sqrt(bruteDistance);

                float projectileSpeed = 0.1f;

                float timeToHit = (float)(distance / (projectileSpeed * 30f)) * 250; //in ms

                if (timeToHit < 25)
                    timeToHit = 25;

                float zone = 10f;

                delayedZoneMagicDmg mySpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "ice", tx, ty, tz, zone, 1);
                mySpell.period = 0;
                mainInstance.ScheduleCallback(mySpell.run, (int)timeToHit);

                Object[] infos = new Object[5];
                infos[0] = "iceBolt"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = tx; //x
                infos[3] = ty; //y
                infos[4] = tz; //z

                author.myGame.sendDataToAll("z_spell", infos, author);
            }

            if (spell.Equals("HealthRegen"))
            {
                float dmg = 20f + 22f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.natureBon) * 0.45f;

                target.healMyHPs(author, dmg);

                Object[] infos = new Object[3];
                infos[0] = "HealEff"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = target.id; //x

                author.myGame.sendDataToAll("t_spell", infos, author);
            }
        }

        void castSelfSpell()
        {
            if (spell.Equals("avatar"))
            {
                author.setBuff(EffectNames.hpBon, 250 + 120 * (rank - 1), 25);
                author.setBuff(EffectNames.resBon, 40 + 10 * (rank - 1), 25);
                author.setBuff(EffectNames.armorBon, 45 + 11 * (rank - 1), 25);
                author.setBuff(EffectNames.dmg, 40 + 10 * (rank - 1), 25);
                author.healMyHPs(author, 250 + 120 * (rank - 1));
                author.sendDynamicInfosToAll();

                Object[] infos = new Object[4];
                infos[0] = "Aura4"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = 60; //duration
                infos[3] = true; //is Avatar
                author.myGame.sendDataToAll("aura", infos, author);
            }

            /*if (spell.Equals("HealthRegen"))
            {
                author.setBuff(EffectNames.hpRegenBon, 10 * rank, 10);

                Object[] infos = new Object[3];
                infos[0] = "Aura2"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = 60 * 10; //x

                author.myGame.sendDataToAll("aura", infos, author);
            }

            if (spell.Equals("ManaRegen"))
            {
                author.setBuff(EffectNames.mpRegenBon, 10 * rank, 10);

                Object[] infos = new Object[3];
                infos[0] = "Aura3"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = 60 * 10; //x

                author.myGame.sendDataToAll("aura", infos, author);
            }*/

            if (spell.Equals("boneDance"))
            {
                float dmg = 140 + 90 * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.natureBon) * 0.6f;

                delayedZoneMagicDmg myDelayedSpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "nature", author.position.x, author.position.y, author.position.z, 8f, 5);
                myDelayedSpell.centerOnHero = true;
                myDelayedSpell.period = 1000;
                mainInstance.ScheduleCallback(myDelayedSpell.run, 25);

                Object[] infos = new Object[3];
                infos[0] = "Aura5"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = 60 * 7; //x

                author.myGame.sendDataToAll("aura", infos, author);
            }

            if (spell.Equals("Reanimation"))
            {
                int amount = 3 + (rank - 1) * 2;

                foreach (String s in author.myGame.units.Keys)
                {
                    Entity tmpUnit = ((Entity)author.myGame.units[s]);

                    if (tmpUnit.hp < 0 && tmpUnit.getDistance(author) < 10)
                    {
                        /* Entity newUnit = author.myGame.addUnit("Skeleton", author.team);
                         newUnit.infos.vitalInfosBon.hp = author.infos.spellBon.totalBon * 1.5f;
                         newUnit.hp += author.infos.spellBon.totalBon * 1.5f;
                         newUnit.infos.vitalInfosBon.dmg = author.infos.spellBon.totalBon * 0.2f;
                         newUnit.lifeSpan = 4*15;
                         newUnit.x = tmpUnit.x;
                         newUnit.y = tmpUnit.y;
                         newUnit.z = tmpUnit.z;
                         newUnit.viewRange = 30;
                         newUnit.lockPosition();
                         newUnit.agressive = true;
                         newUnit.controlledByServer = true;
                         newUnit.team = author.team;
                         newUnit.master = author;*/

                        Object[] infos = new Object[3];
                        infos[0] = "Aura6"; //id
                        infos[1] = author.id + ""; //myid
                        infos[2] = 80; //x

                        author.myGame.sendDataToAll("aura", infos, author);

                        amount--;
                    }

                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }

            if (spell.Equals("SoulShield"))
            {
                float time = 4 * 3 + 2 * (rank - 1);

                author.soulShield = time;

                Object[] infos = new Object[3];
                infos[0] = "Aura7"; //id
                infos[1] = author.id + ""; //myid
                infos[2] = (int)(60f * time / 4f); //x

                author.myGame.sendDataToAll("aura", infos, author);
            }

            if (spell.Equals("MoonlightArt"))
            {
                int time = 8 + 4 * (rank - 1);

                delayedMoonlightArt mySpell = new delayedMoonlightArt(author, mainInstance, time);
                mySpell.period = 250;
                mainInstance.ScheduleCallback(mySpell.run, 25);


                /* Object[] infos = new Object[3];
                 infos[0] = "Aura8"; //id
                 infos[1] = author.id + ""; //myid
                 infos[2] = (int)(60f * time / 2f); //x

                 author.myGame.sendDataToAll("aura", infos, author);*/
            }
        }
    }
}
