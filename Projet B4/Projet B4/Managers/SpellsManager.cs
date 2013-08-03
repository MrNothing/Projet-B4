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
			
		    if((mySpell["usage"].Equals("target")))
		    {
			
			    if((int)mySpell["zone"]>0)
			    {
				    //proceed...
                    castZoneSpell(caster, (string)mySpell["id"], 1, zoneX, zoneY, zoneZ);
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

                    castTargetSpell(caster, (string)mySpell["id"], 1, targetUnit, 0);
			    }
			
		    }
		
		    if((mySpell["usage"].Equals("self")))
		    {
                castSelfSpell(caster, (string)mySpell["id"], 1);
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
            caster.sendCast(myItem);
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
                    //send message: The spell is not ready!
                    return;
                }

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
                            castZoneSpell(caster, (string)mySpell["id"], (int)mySpell["rank"], zoneX, zoneY, zoneZ);
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
                            caster.getMyOwner().Send("err", "s2");
                            //send message: wrong target! -> the target is in my team
                            return;
                        }

                        if (mySpell["targets"].Equals("ally") && !caster.team.Equals(targetUnit.team))
                        {
                            caster.getMyOwner().Send("err", "s2");
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

                        castTargetSpell(caster, (string)mySpell["id"], (int)mySpell["rank"], targetUnit, 0);
                    }

                }

                if ((mySpell["usage"].Equals("self")))
                {
                    castSelfSpell(caster, (string)mySpell["id"], (int)mySpell["rank"]);
                }

                //System.out.println("Casting spell: "+(string)mySpell["id"]);

                //set spell used...
                mySpell.Remove("cd");
                mySpell.Add("cd", (int)mySpell["coolDown"]);
                caster.mp -= ((int)mySpell["mana"] + (int)mySpell["manaPerRank"] * (-1 + (int)mySpell["rank"]));
                caster.sendDynamicInfosToAll("");

                //send CD infos
                Object[] cdinfos = new Object[2];
                cdinfos[0] = spellId; //i
                cdinfos[1] = mySpell["coolDown"]; //cd
                caster.getMyOwner().Send("cd", cdinfos);

               /* if ((int)mySpell["zone"] > 0)
                    caster.sendCast(zoneX, zoneY, zoneZ);
                else*/
                    caster.sendCast();

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
                caster.sendCast();

                if ((mySpell["usage"].Equals("target")))
                {

                    if ((int)mySpell["zone"] > 0)
                    {
                        //proceed...
                        castZoneSpell(caster, (string)mySpell["id"], (int)mySpell["rank"], zoneX, zoneY, zoneZ);

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

                        castTargetSpell(caster, (string)mySpell["id"], (int)mySpell["rank"], targetUnit, 0);
                    }

                }

                if ((mySpell["usage"].Equals("self")))
                {
                    castSelfSpell(caster, (string)mySpell["id"], (int)mySpell["rank"]);
                }

                return true;
                //System.out.println("Casting spell: "+(string)mySpell["id"]);

            }

            public void castZoneSpell(Entity author, String spell, int rank, float ix, float iy, float iz)
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

                    mainInstance.ScheduleCallback(myTeleport.run, 2500 - 500 *(rank - 1));

                    Object[] infos = new Object[3];
                    infos[0] = "Aura1"; //id
                    infos[1] = author.id + ""; //myid
                    infos[2] = 300 - 60 * (rank - 1); //x

                    author.myGame.sendDataToAll("aura", infos, author);

                    author.sendCast(300- 60 * (rank - 1));
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
                    
                    myDelayedSpell.decreaseWithDistance = 1 ;
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
                    float dmg = 3f + 6f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.fireBon) * 0.2f;

                    delayedZoneMagicDmg mySpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "fire", ix, iy, iz, 5f, 3 + ((int)Math.Floor(rank / 2f)));
                    mySpell.period = 1300;
                    mainInstance.ScheduleCallback(mySpell.run, 1000);

                    Object[] infos = new Object[6];
                    infos[0] = "RainOfFire"; //id
                    infos[1] = author.id + ""; //myid
                    infos[2] = ix; //x
                    infos[3] = iy; //y
                    infos[4] = iz; //z

                    infos[5] = 3 + ((int)Math.Floor(rank / 2f)); //waves

                    author.myGame.sendDataToAll("z_spell", infos, author);
                }
            }

            public void castTargetSpell(Entity author, String spell, int rank, Entity target, float fixedDmg)
            {
                if (spell.Equals("fireBall"))
                {
                    float dmg = 5f + 8f * (rank - 1) + (author.infos.spellBon.totalBon + author.infos.spellBon.fireBon) * 0.45f;

                    float tx = target.position.x;
                    float ty = target.position.y;
                    float tz = target.position.z;

                    float bruteDistance = ((tx - author.position.x) * (tx - author.position.x) + (ty - author.position.y) * (ty - author.position.y) + (tz - author.position.z) * (tz - author.position.z));
                    float distance = (float)Math.Sqrt(bruteDistance);

                    float projectileSpeed = 0.2f;

                    float timeToHit = (float)(distance / (projectileSpeed * 60f)) * 250; //in ms

                    if (timeToHit < 25)
                        timeToHit = 25;

                    delayedZoneMagicDmg mySpell = new delayedZoneMagicDmg(author, mainInstance, dmg, "fire", tx, ty, tz, 2f, 1);
                    mySpell.period = 0;
                    mainInstance.ScheduleCallback(mySpell.run, (int)timeToHit);

                    Object[] infos = new Object[6];
                    infos[0] = "proj1"; //id
                    infos[1] = author.id + ""; //myid
                    infos[2] = target.id; //x
                    infos[3] = target.position.x;
                    infos[4] = target.position.y;
                    infos[5] = target.position.z;

                    author.myGame.sendDataToAll("t_spell", infos, author);
                }
            }

            public void castSelfSpell(Entity author, String spell, int rank)
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

                if (spell.Equals("HealthRegen"))
                {
                    author.setBuff(EffectNames.hpRegenBon, 10*rank, 10);

                    Object[] infos = new Object[3];
                    infos[0] = "Aura2"; //id
                    infos[1] = author.id + ""; //myid
                    infos[2] = 60*10; //x

                    author.myGame.sendDataToAll("aura", infos, author);
                }

                if (spell.Equals("ManaRegen"))
                {
                    author.setBuff(EffectNames.mpRegenBon, 10*rank, 10);

                    Object[] infos = new Object[3];
                    infos[0] = "Aura3"; //id
                    infos[1] = author.id + ""; //myid
                    infos[2] = 60 * 10; //x

                    author.myGame.sendDataToAll("aura", infos, author);
                }

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
                    infos[2] = (int)(60f * time/4f); //x

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
