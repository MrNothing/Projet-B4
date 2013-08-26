using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace ProjetB4
{
    public enum EntityType
    {
        //if an npc has items he is a vendor;
        npc, player, trigger, usables,
    }

    public enum AgressivityLevel
    {
        friendly = 1, neutral = 2, passive = 3, agressive = 4,
    }

    public class Entity
    {
        public String id;
        public String name;
        public EntityType type = EntityType.npc;
        public String team="";
        public AgressivityLevel agressivity = AgressivityLevel.neutral;

        public Vector3 position = new Vector3(0, 0, 0);
        public Vector3 wanderAround = new Vector3(0, 0, 0);
        public Vector3 destination = new Vector3(0, 0, 0);

        public List<Vector3> paths = new List<Vector3>();

        public EntityInfos infos;

        public float hp;
        public float mp;

        public float xp;
        public int level;

        public Dictionary<String, Hashtable> spells = new Dictionary<String, Hashtable>(); //object shall be spell soon...
        public Dictionary<String, Item> items = new Dictionary<string,Item>();

        public GameCode myGame;
        public Player myController;

        //The distance to view this unit as a player.
        public float viewRange = 30;
		
		//This defines the for ranges for visibility
		public Vector3 checkRange = new Vector3(2,0.1f,2);
		
        public String focus = null;
        public Entity master=null;
        public float lifeSpan = 0;
        public Entity riding = null;
        public bool ridable = false;
       
        DateTime lastDate = DateTime.Now;
        public float decalage = 1f;

        public Triggers myTrigger;
        public SpawnZone spawnZone;

        private int visibleCounter = 0;
        public Dictionary<String, String> visibleUnits = new Dictionary<String, String>();
        public Dictionary<String, String> visibleEnemies = new Dictionary<String, String>();
        public Dictionary<String, String> visibleAllies = new Dictionary<String, String>();
        public Dictionary<String, String> visibleEnnemyHeroes = new Dictionary<String, String>();
        public Dictionary<String, String> visiblePlayers = new Dictionary<String, String>();
        public Dictionary<String, String> visibleEnnemyNonHeroes = new Dictionary<String, String>();
        public Dictionary<String, String> visibleCorpses = new Dictionary<String, String>();

        public String watcher="";

        public Timer incantation = null;
        public Timer canalisedSpell = null;

        public Entity(GameCode _myGame, String _id, String _name, EntityInfos _infos, Vector3 _position)
        {
            try
            {
                if (_infos.spells.Length > 0)
                {
                    SpellInfos spellInfos = new SpellInfos();
                    for (int i = 0; i < _infos.spells.Length; i++)
                        spells.Add(i + "", new Hashtable((Hashtable)spellInfos.allSpells[_infos.spells[i]]));
                }

                myTrigger = new Triggers(this);
                myGame = _myGame;
                id = _id;
                name = _name;
                infos = _infos;

                ridable = _infos.ridable;

                level = _infos.level;

                position = _position;
                destination = _position.getNewInstance();
                initialPosition = _position.getNewInstance();

                type = EntityType.npc;
                team = "neutral";
                applyBaseStatsToVitalInfos();

                enablePathFinder(new Dictionary<String, Vector3>());
            }
            catch (Exception e)
            {
                _myGame.PlayerIO.ErrorLog.WriteError("unit failed: "+_id+" cause: "+e);
            }
        }

        PathFinder pathfinder;
        public void enablePathFinder(Dictionary<String, Vector3> wayPoints)
        {
            pathfinder = new PathFinder(wayPoints, myGame.baseStep);
        }

        public int itemsUsageCd = 0;

        public float rezInterval = 4 * 30;
        public float rezCounter = 0;
        public bool isTemp = false;
        public Vector3 initialPosition;

        public Random mainSeed = new Random();

        float IntervalCounter1000 = 0;
        float IntervalCounter250 = 0;

        public int recentlyHit = 0;

        public void run()
        {
            int counter = 0;

            if (lifeSpan > 0)
            {
                lifeSpan--;

                if (lifeSpan == 0)
                {
                    hp = -1;
                }
            }

            counter++;

            DateTime tmpDate = DateTime.Now;

            int diff = (tmpDate - lastDate).Milliseconds;

            decalage = ((diff) / myGame.loopInterval); //the amount of ms passed, supposed: 250ms

            lastDate = tmpDate;

            counter++;

            setRegenPoints();

            counter++;

            /*visibleUnits = new Dictionary<String, String>();
            visibleEnemies = new Dictionary<String, String>();
            visibleAllies = new Dictionary<String, String>();
            visibleEnnemyHeroes = new Dictionary<String, String>();
            visiblePlayers = new Dictionary<String, String>();
            visibleEnnemyNonHeroes = new Dictionary<String, String>();
            visibleCorpses = new Dictionary<String, String>();
            debugMsg = "";

            if (type.Equals(EntityType.player))
            {
                visibleUnits.Add(id, id);
                visiblePlayers.Add(id, id);
            }

            for(float x=-checkRange.x; x<checkRange.x; x++)
            {
                for(float y=-checkRange.y; y<checkRange.y; y++)
                {
                    for(float z=-checkRange.z; z<checkRange.z; z++)
                        checkVisiblePlayers(new Vector3((int)x,(int)y,(int)z));
                }
            }*/
            if (grabbedTarget != null)
            {
                //getMyOwner().Send("err", grabbedTarget.position.toString());
                grabbedTarget.position = position;
                grabbedTarget.destination = position;
                grabbedTarget.paths = new List<Vector3>();
                grabbedTarget.grabbed = 3;
            }

            if (type != EntityType.player)
            {
                if (riding == null)
                    synchronizePosition();

                //if(hasMoved)
                //sendPos();

                if (hp <= 0)
                {
                    if (rezCounter <= 0)
                    {
                        if (isTemp || master != null)
                            myGame.destroyUnit(id);
                        else
                        {
                            position = initialPosition;
                            hp = getMaxHp();
                            mp = getMaxMp();
                            sendDynamicInfosToAll();
                        }
                    }
                    else
                        rezCounter--;
                }

                if (type.Equals(EntityType.trigger))
                {
                    if (myTrigger.autoTrigger > 0)
                    {
                        if (myTrigger.autoTriggerCounter < myTrigger.autoTrigger)
                        {
                            myTrigger.autoTriggerCounter++;
                        }
                        else
                        {
                            myTrigger.activate();
                            myTrigger.autoTriggerCounter = 1;
                        }
                    }
                }
                else
                {
                    try
                    {
                        triggerWanderAround();
                    }
                    catch (Exception e)
                    {
                        if (master != null)
                            master.getMyOwner().Send("err", "triggerWanderAround fail: " + e.Message);
                    }

                    try
                    {
                        if (grabbed <= 0)
                            applyIAMoves();
                    }
                    catch (Exception e)
                    {
                        if (master != null)
                            master.getMyOwner().Send("err", "IA fail: " + e.Message);
                    }


                }

            }
            else //i am a player
            {
                //myGame.PlayerIO.ErrorLog.WriteError("i see " + visibleUnits.Count + " entities "+"my ref is:"+getPosRefId());

                /*if (focus != null)
                {
                    setFocusDistance();

                    if (focusDistance <= infos.range && attackCounter > getAttackSpeed())
                    {
                        attack(focus);
                        attackCounter = 0;
                    }
                }
                */

                //getMyOwner().Send("err", "i see " + visibleUnits.Count + " entities " + "my ref is:" + getPosRefId()+" in total, there are "+myGame.units.Count+" units");

                if (attackCounter <= getAttackSpeed())
                    attackCounter += 0.25f * decalage / 2;
            }

            //setRef();

            if (combatMode > 0)
                combatMode--;

            if (recentlyHit > 0)
                recentlyHit--;
        }

        int focusCounter = 0;
        public float attackCounter = 0;
        String lastTargetPosRefId="";
        private void applyIAMoves()
        {

            if (isSynchronized() && paths.Count > 0)
            {
                destination = paths[paths.Count - 1];
                paths.RemoveAt(paths.Count - 1);
            }

            string debugLocation = "";
            if (focus != null && hp > 0 && infos.range > 0)
            {

                float highestThreat = 0;
                foreach (String tmpId in lastHiter.Keys)
                {
                    if (highestThreat < lastHiter[tmpId])
                    {
                        highestThreat = lastHiter[tmpId];
                        focus = tmpId;
                    }
                }

                setFocusDistance();

                if (lastHiter.Count <= 0 && master == null)
                {
                    hp = getMaxHp();
                    mp = getMaxMp();
                }

                if (focus != null)
                {
                    if (watcher.Length > 0)
                    {
                        myGame.players[watcher].Send("err", "focusDistance: " + focusDistance);
                    }
                    if (focusDistance > infos.range)
                    {
                        if (position.Substract(initialPosition).Magnitude() > viewRange)
                        {
                            try
                            {
                                lastHiter.Remove(focus);
                            }
                            catch (Exception e) { }

                            focus = null;
                            paths = pathfinder.start(position, initialPosition);
                            destination = paths[paths.Count - 1];
                            paths.RemoveAt(paths.Count - 1);
                            lastHiter.Clear();

                            if (lastHiter.Count<=0 && master==null)
                            {
                                hp = getMaxHp();
                                mp = getMaxMp();
                            }
                        }
                        else
                        {
                                
                                //walk to the target...

                                if (infos.baseSpeed > 0 && focusCounter > 2)
                                {
                                   
                                    focusCounter = 0;
                                    lastTargetPosRefId = myGame.units[focus].getStepRefId();
                                    //myPathFinder.go(getTiledPosition(), ((Unit)myGame.units[focus]).getTiledPosition());
                                    
                                    Vector3 newDestination = new Vector3();
                                    newDestination.x = myGame.units[focus].position.x + (float)mainSeed.NextDouble() * 1f - (float)mainSeed.NextDouble() * 1f;
                                    newDestination.y = myGame.units[focus].position.y;
                                    newDestination.z = myGame.units[focus].position.z + (float)mainSeed.NextDouble() * 1f - (float)mainSeed.NextDouble() * 1f;
                                    
                                    paths = pathfinder.start(position, newDestination);
                                    destination = paths[paths.Count-1];
                                    paths.RemoveAt(paths.Count - 1);
                                    // sendPos();
                                }
                                focusCounter++;
                        }
                    }
                    else
                    {
                        paths = new List<Vector3>();
                        destination = position;
                        //myGame.PlayerIO.ErrorLog.WriteError("OK5");
                        if (attackCounter > getAttackSpeed())
                        {
                            //Spell casting:

                            //Spell 1: Offensive Spells
                            //Spell 2: Defensive Spells
                            //Spell 3: Non combat/start Spells (buffs...)

                            bool casting = false;

                            if (spells.Count > 0 && mp > 0)
                            {
                                casting = true;

                                int randomSpell = mainSeed.Next(0, spells.Count);

                                if (randomSpell >= spells.Count)
                                    randomSpell = spells.Count - 1;

                                if (spells[randomSpell+""] != null)
                                {
                                    Entity focusedUnit = myGame.units[focus];
                                    casting = myGame.spellsManager.IAUseSpell(this, focus, randomSpell + "", focusedUnit.position.x, focusedUnit.position.y, focusedUnit.position.z);
                                }
                            }

                            if (!casting)
                            {
                                attack(focus);
                            }

                            attackCounter = 0;
                        }

                        debugLocation = "line 338"; //7
                    }
                }
            }

            if (attackCounter <= getAttackSpeed())
                attackCounter += 0.25f;
        }

        public void attack(String target)
        {
            if (myGame.units[target] != null && grabbed <= 0 && grabbedTarget==null)
            {
                Entity targetUnit = myGame.units[target];
                float tmpDmg = getAttackValue();
                targetUnit.hitMeWithPhysic(id, tmpDmg, lastCrit);
                sendAnim("Attack", target);
            }

            if (type==EntityType.player && grabbedTarget != null)
            {
                getMyOwner().Send("err", "You cannot attack while holding someone!");
            }
        }

        public string debugMsg = "";
        private void checkVisiblePlayers(Vector3 offset)
        {
            //check the units around me, if they are players, send my position to them.
            //startAttacking: if the target is on range
            //focusTarget() //if the target is in attack range and i am not attacking anyone.
            //sendPosition() //if the target is a player and i am in his vision range -> viewRange.
			
			string posRefId = position.Add(offset).toPosRefId(myGame.baseRefSize);
			
            try
            {
				Dictionary<String, String> bloc = myGame.inGameUnitsRefs[posRefId];

                debugMsg += posRefId+" {";

                foreach (String s in bloc.Keys)
                {
                    try
                    {
                        Entity theUnit = myGame.units[s];

                        debugMsg += theUnit.id + ",";

                        visibleUnits.Add(theUnit.id, theUnit.id);

                        if (theUnit.hp <= 0)
                            visibleCorpses.Add(theUnit.id, theUnit.id);

                        if (theUnit.team != team)
                        {
                            visibleEnemies.Add(theUnit.id, theUnit.id);

                            if (type == EntityType.player)
                                visibleEnnemyHeroes = new Dictionary<String, String>();
                            else
                                visibleEnnemyNonHeroes = new Dictionary<String, String>();
                        }
                        else
                            visibleAllies.Add(theUnit.id, theUnit.id);

                        if (theUnit.type == EntityType.player)
                            visiblePlayers.Add(theUnit.id, theUnit.id);

                        if (theUnit.team != team && agressivity == AgressivityLevel.agressive && type == EntityType.npc)
                        {
                            if (theUnit.getDistance(this) <= viewRange)
                                focus = theUnit.id;
                        }
                    }
                    catch (Exception e)
                    {
                        debugMsg += e.Message + ",";
                    }
                }

                debugMsg += "}";
            }
            catch(Exception e)
            {
                // if (type == EntityType.player)
                //  myGame.PlayerIO.ErrorLog.WriteError("checking Zone id: " + tmp_id + " result was failure");
            }
        }

        float focusDistance = 999;
        public void setFocusDistance()
        {
            Entity targetUnit = myGame.units[focus];

            if (targetUnit != null)
            {
                if (targetUnit.hp > 0)
                {
                    Vector3 tmpPos = new Vector3(position);
                    //tmpPos.y = targetUnit.position.y;
                    focusDistance = targetUnit.position.Substract(tmpPos).SqrMagnitude();
					
					//target is out of viewRange
                    if (focusDistance > viewRange + 3)
                    {
                        try{
                            lastHiter.Remove(focus);
                        }
                        catch (Exception e) { }

                        focus = null;
                        focusDistance = 999;
                        lastHiter.Clear();
                    }
                    /*if(wayPoints.Count>0)
                    {
                        if(wayPoints.getClass((wayPointsIndex-1)+"")!=null)
                        {
                            Vector3 tmpPos = (Vector3) wayPoints.getClass((wayPointsIndex-1)+"");
                            if(Math.Abs(x-tmpPos.x)+Math.Abs(z-tmpPos.z)>15)
                            {
                                  focus = null;
                                  sendFocus();
                                  focusDistance = 999;
                            }
                        }
                    }*/
                }
                else
                {
                    try
                    {
                        lastHiter.Remove(focus);
                    }
                    catch (Exception e) { }


					//Target is dead, reset focus
                    focus = null;
                    focusDistance = 999;
                    lastHiter.Clear();
                }
            }
            else
            {
                try
                {
                    lastHiter.Remove(focus);
                }
                catch (Exception e) { }

				//Target has disappeared or something was really wrong with it...
                focus = null;
                focusDistance = 999;
                lastHiter.Clear();
            }
        }

        /*String lastPosRefId = "";
        public void setRef()
        {

            String last_id = lastPosRefId;
            String tmp_id = getPosRefId();

            String char_id = id;
            // System.out.println("last_id="+last_id+" tmp_id="+tmp_id);

            try
            {
                if (!tmp_id.Equals(last_id))
                    myGame.inGameUnitsRefs[last_id].Remove(char_id);

                //if i moved remove my last assignement
            }
            catch (Exception e2) 
            {
                //i was nowhere before!
            }

            lastPosRefId = tmp_id;


            try
            {
                Dictionary<String, String> Tmp_users_list = myGame.inGameUnitsRefs[tmp_id];

                myGame.inGameUnitsRefs[tmp_id].Add(char_id, id);

            }
            catch (Exception e)
            {
                //if(type==EntityType.player)
                //    myGame.PlayerIO.ErrorLog.WriteError("I have to create this block: " + tmp_id + " and i added myself inside...");
                
                Dictionary<String, String> tmp = new Dictionary<String, String>();
                tmp.Add(char_id, id);

                try
                {
                    myGame.inGameUnitsRefs.Remove(tmp_id);
                }
                catch (Exception e4)
                { 
                
                }

                myGame.inGameUnitsRefs.Add(tmp_id, tmp);
            }
        }
       
        public void clearRef()
        {
            String last_id = lastPosRefId;
            String char_id = id;

            if (myGame.inGameUnitsRefs[last_id] != null)
            {
                Dictionary<String, String> Tmp_users_list = myGame.inGameUnitsRefs[last_id];
                if (Tmp_users_list[char_id] != null)
                {
                    Tmp_users_list.Remove(char_id);
                    //System.out.println("I was here before: "+last_id+" , I removed myself from here...");
                    //System.out.println("Now my value in this last block is: "+Tmp_users_list.get(char_id));
                }
            }

        }

         */

        public bool isPatrol = false;
        public float seed=1;
        int patrolK = 1;
        public Vector3 petOffset = new Vector3(0, 0, 0);
        void triggerWanderAround()
        {
            if (master != null)
            {
                try
                {
                    int i = master.level;
                }
                catch(Exception e)
                {
                    myGame.destroyUnit(id);
                }
                wanderAround.x = 15;
                wanderAround.z = 15;

                initialPosition = master.position.Add(petOffset);

                if (focus == null)
                {
                    destination = master.position.Add(petOffset);   
                }

                position.y = master.position.y;

                if (master != null && master.getDistance(this) > 40)
                {
                    focus = null;
                }

                if (master.recentlyHit > 0)
                {
                       focus = master.lastDirectHiter;
                }
            }

            if ((wanderAround.x > 0 || wanderAround.z > 0) && focus == null)
            {
                //&& UnityEngine.Random.Range(0, 100)<10
                if (isSynchronized() && mainSeed.NextDouble()*100 < seed)
                {
                    //print(infos.unitName+" moving..");

                    Vector3 newDestination = new Vector3();

                    if (!isPatrol)
                    {
                        newDestination.x = initialPosition.x + ((float)mainSeed.NextDouble() * wanderAround.x - (float)mainSeed.NextDouble() * wanderAround.x);
                        newDestination.y = position.y;
                        newDestination.z = initialPosition.z + ((float)mainSeed.NextDouble() * wanderAround.z - (float)mainSeed.NextDouble() * wanderAround.z);
                    }
                    else
                    {
                        newDestination.x = initialPosition.x + wanderAround.x * patrolK;
                        newDestination.y = position.y;
                        newDestination.z = initialPosition.z + wanderAround.z * patrolK;
                        patrolK = -patrolK;
                    }

                    paths = pathfinder.start(position, newDestination);
                    destination = paths[paths.Count - 1];
                    paths.RemoveAt(paths.Count - 1);
                }
            }
        }

        public void applyBaseStatsToVitalInfos()
        {
            infos.vitalInfos.hp = infos.baseStats.sta * 11;
            infos.vitalInfos.mp = infos.baseStats.intel * 14;
            infos.vitalInfos.hpRegen = (float)(infos.baseStats.sou * 0.022) + (float)(infos.baseStats.sta * 0.011);
            infos.vitalInfos.mpRegen = (float)(infos.baseStats.sou * 0.028) + (float)(infos.baseStats.intel * 0.014);
            infos.vitalInfos.dmg = (float)(infos.baseStats.str * 0.6);
            infos.vitalInfos.armor = (float)(infos.baseStats.agi * 1.1);
            infos.vitalInfos.attackSpeed = (float)(infos.baseStats.agi * 0.4);
            infos.vitalInfos.crit = (float)((infos.baseStats.agi) * 0.125);

            hp = (int)infos.vitalInfos.hp;
            mp = (int)infos.vitalInfos.mp;

            //speed = 0.6f+infos.baseStats.agi/15;


        }

        public void applyAllBaseStatsToVitalInfos()
        {
            infos.vitalInfos.hp = (infos.baseStats.sta + infos.baseStatsBon.sta) * 11;
            infos.vitalInfos.mp = (infos.baseStats.intel + infos.baseStatsBon.intel) * 14;
            infos.vitalInfos.hpRegen = (float)((infos.baseStats.sou + infos.baseStatsBon.sou) * 0.022) + (float)((infos.baseStats.sta + infos.baseStatsBon.sta) * 0.011);
            infos.vitalInfos.mpRegen = (float)((infos.baseStats.sou + infos.baseStatsBon.sou) * 0.028) + (float)((infos.baseStats.intel + infos.baseStatsBon.intel) * 0.014);
            infos.vitalInfos.dmg = (float)((infos.baseStats.str + infos.baseStatsBon.str) * 0.6);
            infos.vitalInfos.armor = (float)((infos.baseStats.agi + infos.baseStatsBon.agi) * 1.1);
            infos.vitalInfos.attackSpeed = (float)((infos.baseStats.agi + infos.baseStatsBon.agi) * 0.4);
            infos.vitalInfos.crit = (float)((infos.baseStats.agi + infos.baseStatsBon.agi) * 0.125);

            //speed = 0.6f+(infos.baseStats.agi+infos.baseStatsBon.agi)/15;
        }

        public void setEffet(EffectNames effect, float amount)
        {
            //if (effect.Equals("speed"))
            //    speed += amount;

            if (effect == EffectNames.range)
                infos.range += amount;

            if (effect==EffectNames.souBon)
                infos.baseStatsBon.sou += amount;
            if (effect == EffectNames.staBon)
                infos.baseStatsBon.sta += amount;
            if (effect == EffectNames.intBon)
                infos.baseStatsBon.intel += amount;
            if (effect == EffectNames.strBon)
                infos.baseStatsBon.str += amount;
            if (effect == EffectNames.agiBon)
                infos.baseStatsBon.agi += amount;

            if (effect == EffectNames.hpBon)
                infos.vitalInfosBon.hp += amount;
            if (effect == EffectNames.mpBon)
                infos.vitalInfosBon.mp += amount;
            if (effect == EffectNames.hpRegenBon)
                infos.vitalInfosBon.hpRegen += amount;
            if (effect == EffectNames.mpRegenBon)
                infos.vitalInfosBon.mpRegen += amount;
            if (effect == EffectNames.armorBon)
                infos.vitalInfosBon.armor += amount;
            if (effect == EffectNames.resBon)
                infos.resBon.totalRes += amount;
            if (effect == EffectNames.dmg)
                infos.vitalInfosBon.dmg += amount;
            if (effect == EffectNames.crit)
                infos.vitalInfosBon.crit += amount;
            if (effect == EffectNames.spellcrit)
                infos.vitalInfosBon.spellCrit += amount;
            if (effect == EffectNames.critBon)
                infos.vitalInfosBon.critBon += amount;
            if (effect == EffectNames.spellcritBon)
                infos.vitalInfosBon.spellCritBon += amount;
            if (effect == EffectNames.spell_dmg)
                infos.spellBon.totalBon += amount;

            if (effect == EffectNames.dmg_livings)
                infos.vitalInfosBon.dmgLiving += amount;
            if (effect == EffectNames.dmg_undeads)
                infos.vitalInfosBon.dmgUndead += amount;
            if (effect == EffectNames.dmg_monsters)
                infos.vitalInfosBon.dmgMonsters += amount;
            if (effect == EffectNames.dmg_humanoids)
                infos.vitalInfosBon.dmgHumanoids += amount;
            if (effect == EffectNames.dmg_humans)
                infos.vitalInfosBon.dmgHumans += amount;
            if (effect == EffectNames.dmg_spirits)
                infos.vitalInfosBon.dmgSpirits += amount;
            if (effect == EffectNames.dmg_ogres)
                infos.vitalInfosBon.dmgOgres += amount;
            if (effect == EffectNames.dmg_dragons)
                infos.vitalInfosBon.dmgDragons += amount;

            if (effect == EffectNames.attackSpeed)
                infos.vitalInfosBon.attackSpeed += amount;

            if (effect == EffectNames.fireRes)
                infos.resBon.fireRes += amount;
            if (effect == EffectNames.arcaneRes)
                infos.resBon.arcaneRes += amount;
            if (effect == EffectNames.iceRes)
                infos.resBon.iceRes += amount;
            if (effect == EffectNames.natureRes)
                infos.resBon.natureRes += amount;
            if (effect == EffectNames.shadowRes)
                infos.resBon.shadowRes += amount;

            if (effect == EffectNames.fireBon)
                infos.spellBon.fireBon += amount;
            if (effect == EffectNames.arcaneBon)
                infos.spellBon.arcaneBon += amount;
            if (effect == EffectNames.iceBon)
                infos.spellBon.iceBon += amount;
            if (effect == EffectNames.natureBon)
                infos.spellBon.natureBon += amount;
            if (effect == EffectNames.shadowBon)
                infos.spellBon.shadowBon += amount;

            if (effect == EffectNames.chaosBon)
                infos.spellBon.chaosBon += amount;

            if (effect == EffectNames.drainHp)
                infos.specialEffects.drainHp += amount;

            if (effect == EffectNames.drainMp)
                infos.specialEffects.drainMp += amount;

            if (effect == EffectNames.ignoreArmor)
                infos.specialEffects.ignoreArmor += amount;

            if (effect == EffectNames.ignoreRes)
                infos.specialEffects.ignoreRes += amount;

            if (effect == EffectNames.slow)
                infos.specialEffects.slow += amount;

            if (effect == EffectNames.stun)
                infos.specialEffects.stun1 += amount;

            //if (effect.Equals("stun2"))
            //    infos.specialEffects.stun2 += amount;

            if (effect == EffectNames.poison1)
                infos.specialEffects.poison1 += amount;

            if (effect == EffectNames.poison2)
                infos.specialEffects.poison2 += amount;

            if (effect == EffectNames.poison3)
                infos.specialEffects.poison3 += amount;

            if (effect == EffectNames.spellVamp)
                infos.specialEffects.spellVamp += amount;

            if (effect == EffectNames.manaVamp)
                infos.specialEffects.manaVamp += amount;

            if (effect == EffectNames.spikes)
                infos.specialEffects.spikes += amount;

            if (effect == EffectNames.resilience)
                infos.specialEffects.resilience += amount;
        }

        public void setPos(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
        }

        public Hashtable dots = new Hashtable();
        public Hashtable buffs = new Hashtable();

        public void applyDots()
        {
            Hashtable tmpDot = new Hashtable(dots);
            foreach (Object o in tmpDot.Keys)
            {
                Hashtable myEffect = (Hashtable)tmpDot[o + ""];
                String eName = myEffect["effect"] + "";
                float eValue = (float)myEffect["amount"];
                int turns = (int)myEffect["turns"];

                if (turns > 0)
                {
                    if (eName.Equals("poison"))
                    {
                        this.hitMeWithMagic(myEffect["author"]+"", eValue, "nature");
                    }

                    if (eName.Equals("burn"))
                    {
                        this.hitMeWithMagic(myEffect["author"] + "", eValue, "fire");
                    }

                    if (eName.Equals("stun"))
                    {
                        stun = 3;
                    }

                    if (eName.Equals("snare"))
                    {
                        //TODO
                        snareRatio=0;
                    }

                    myEffect["turns"] = turns - 1;
                }
                else
                {
                    dots.Remove(o + "");
                }
            }


        }

        public void clearDots()
        {
            dots = new Hashtable();
        }

        private int dotCounter = 0;
        public void setDot(String effect, float amount, int turns, string author)
        {
            Hashtable myEffect = new Hashtable();
            myEffect.Add("effect", effect);
            myEffect.Add("amount", amount);
            myEffect.Add("turns", turns);
            myEffect.Add("author", author);
            dots.Add(dotCounter + "", myEffect);
            dotCounter++;
        }

        private int buffCounter = 0;
        public void setBuff(EffectNames effect, float amount, int duration)
        {
            Hashtable myEffect = new Hashtable();
            myEffect.Add("effect", effect);
            myEffect.Add("amount", amount);
            myEffect.Add("turns", duration);

            setEffet(effect, amount);

            if (type==EntityType.player)
            {
                ((Hero)this).sendStatsToMe();
            }

            sendDynamicInfosToAll("");

            buffs.Add(buffCounter + "", myEffect);
            buffCounter++;
        }

        public void clearBuffs()
        {
            bool shouldSend = false;
            Hashtable tmpBuffs = new Hashtable(buffs);
            foreach (Object o in tmpBuffs.Keys)
            {
                Hashtable myEffect = (Hashtable)tmpBuffs[o + ""];
                EffectNames eName = (EffectNames)myEffect["effect"];
                float eValue = (float)myEffect["amount"];
                int turns = (int)myEffect["turns"];

                if (turns <= 0)
                {
                    setEffet(eName, -eValue);
                    buffs.Remove(o + "");

                    shouldSend = true;
                }
                else
                {
                    myEffect.Remove("turns");
                    myEffect.Add("turns", turns - 1);
                }
            }

            if (type==EntityType.player && shouldSend)
            {
                ((Hero)this).sendStatsToMe();
            }

            if (shouldSend)
            sendDynamicInfosToAll("");
        }

        public void clearShields()
        {
            magicShield.arcaneBon = 0;
            magicShield.chaosBon = 0;
            magicShield.fireBon = 0;
            magicShield.iceBon = 0;
            magicShield.natureBon = 0;
            magicShield.shadowBon = 0;
            magicShield.totalBon = 0;
            physicShield = 0;
        }

        public void refreshCds()
        {
            if (spells.Count > 0)
            {
                foreach (Object o in spells.Keys)
                {
                    Hashtable mySpell = (Hashtable)spells[o + ""];
                    if ((int)mySpell["cd"] > 0)
                    {
                        int lastCd = (int)mySpell["cd"];
                        mySpell.Remove("cd");
                        mySpell.Add("cd", (lastCd) - 1);
                    }
                }
            }

            if (itemsUsageCd > 0)
                itemsUsageCd--;
        }

        float lastHp = 0;
        float lastMp = 0;

        public void setRegenPoints()
        {
            applyDots();
            clearBuffs();
            refreshCds();

            if (soulShield > 0)
            {
                if (mp <= 0)
                {
                    soulShield = 0;

                    Object[] infos = new Object[2];
                    infos[0] = soulShield; //
                    infos[1] = id + ""; //myid
                    myGame.sendDataToAll("soulShield", infos, this);
                }
                else
                    mp -= getMaxMp() / 10;
            }

            if (hp > 0 && combatMode<=0) //only heroes or specific units regenerate hp over time.
            {
                if (hp < getMaxHp())
                {
                    hp += (infos.vitalInfos.hpRegen + infos.vitalInfosBon.hpRegen) * decalage * (float)myGame.loopInterval/ 500f;
                }

                if (mp < getMaxMp())
                {
                    mp += (infos.vitalInfos.mpRegen + infos.vitalInfosBon.mpRegen) * decalage * (float)myGame.loopInterval / 500f;
                }

            }

            if (hp > getMaxHp() && hp > 0)
                hp = getMaxHp();

            if (mp > getMaxMp() && hp > 0)
                mp = getMaxMp();


            if (hp != lastHp || mp != lastMp)
                 sendDynamicInfosToAll("");

            lastHp = hp;
            lastMp = mp;

            if (focus != null)
            {
                if (!focus.Equals(lastFocus))
                {
                    lastFocus = focus;
                    sendFocus();
                }
            }
        }

        String lastFocus = null;

        string lastDirectHiter = "";
        public SortedDictionary<String, float> lastHiter = new SortedDictionary<String, float>();
        public bool lastCrit = false;
        public float physicShield = 0;
        public void hitMeWithPhysic(String _author, float dmg, bool crit)
        {
            if (_author.Equals(id))
                return;

            lastCrit = false;
            if (hp > 0)
            {
                Entity author = (Entity)myGame.units[_author];

                recentlyHit = 70;
                lastDirectHiter = _author;

                if (author.infos.specialEffects.drainHp > 0 || author.infos.specialEffects.drainMp > 0 && author.hp > 0)
                {
                    author.hp += dmg * author.infos.specialEffects.drainHp / 100f;
                    author.mp += dmg * author.infos.specialEffects.drainMp / 100f;

                    author.sendDynamicInfosToAll(author.id, false);
                }

                if (infos.specialEffects.spikes > 0 && author.hp > 0)
                {
                    author.hp -= dmg * infos.specialEffects.spikes / 100f;
                    /* author.sendDynamicInfosToAll(id, false);

                     author.checkIfDead(this, (int)(dmg * infos.specialEffects.spikes / 100f));*/

                    if (master != null)
                    {
                        author.sendDynamicInfosToAll(master.id, crit);
                        author.checkIfDead(this, (int)(dmg * infos.specialEffects.spikes / 100f));
                    }
                    else
                    {
                        author.sendDynamicInfosToAll(id, crit);
                        author.checkIfDead(this, (int)(dmg * infos.specialEffects.spikes / 100f));
                    }
                }


                /*bool crit = false;
                if ((mainSeed).Next(0, 100) < author.infos.vitalInfos.crit + author.infos.vitalInfosBon.crit)
                {
                    dmg = dmg * (150f + author.infos.vitalInfos.critBon + author.infos.vitalInfosBon.critBon) / 100f;
                    crit = true;
                }*/

                if (crit && infos.specialEffects.resilience > 0)
                {
                    dmg -= dmg * infos.specialEffects.resilience / 100f;
                }

                float armor = -author.infos.specialEffects.ignoreArmor + infos.vitalInfos.armor + infos.vitalInfosBon.armor;

                if (armor < 0)
                    armor = armor / 10;

                if (armor < -99)
                    armor = -99;

                float division = (armor) + (float)1000;
                if (division <= 0)
                    division = 1;
                dmg = dmg * (1000 / (division));

                //System.out.println(dmg+" division: "+division);

                float AbsorbedValue = physicShield;
                if (physicShield > dmg)
                {
                    physicShield = physicShield - dmg;
                }
                else
                {
                    physicShield = 0;
                }

                dmg -= AbsorbedValue;

                if (dmg < 0)
                    dmg = 0;

                //if (author.attackPropel > 0)
                //{
                    //propelMe(author, author.attackPropel);
                //}

                hp -= dmg;

                if (author.master != null)
                {
                    sendDynamicInfosToAll(author.master.id, crit);
                    checkIfDead(author, (int)dmg);
                }
                else
                {
                    sendDynamicInfosToAll(author.id, crit);
                    checkIfDead(author, (int)dmg);
                }
            }
        }

        public Entity grabbedTarget = null;
        public int grabbed = 0;
        public float soulShield = 0;
        public SpellBonusInfos magicShield = new SpellBonusInfos();
        public void hitMeWithMagic(String _author, float dmg, String dmgType)
        {

            if (_author.Equals(id))
                return;

            if (hp > 0)
            {
                recentlyHit = 70;
                lastDirectHiter = _author;

                Entity author = (Entity)myGame.units[_author];
                bool crit = false;
                if ((mainSeed).NextDouble() * 100 < author.infos.vitalInfos.spellCrit + author.infos.vitalInfosBon.spellCrit)
                {
                    dmg = dmg * (150 + author.infos.vitalInfos.spellCritBon + author.infos.vitalInfosBon.spellCritBon) / 100;
                    crit = true;
                }

                if (author.infos.specialEffects.drainHp > 0 || author.infos.specialEffects.drainMp > 0 && author.hp > 0)
                {
                    author.hp += dmg * author.infos.specialEffects.spellVamp / 100f;
                    author.mp += dmg * author.infos.specialEffects.manaVamp / 100f;
                    author.sendDynamicInfosToAll(author.id, false);

                }

                float armor = 0;
                if (dmgType.Equals("fire"))
                    armor = (-author.infos.specialEffects.ignoreRes + infos.resBon.totalRes + infos.resBon.fireRes);

                if (dmgType.Equals("nature"))
                    armor = (-author.infos.specialEffects.ignoreRes + infos.resBon.totalRes + infos.resBon.natureRes);

                if (dmgType.Equals("shadow"))
                    armor = (-author.infos.specialEffects.ignoreRes + infos.resBon.totalRes + infos.resBon.shadowRes);

                if (dmgType.Equals("arcane"))
                    armor = (-author.infos.specialEffects.ignoreRes + infos.resBon.totalRes + infos.resBon.arcaneRes);

                if (dmgType.Equals("ice"))
                    armor = (-author.infos.specialEffects.ignoreRes + infos.resBon.totalRes + infos.resBon.iceRes);

                if (armor < 0)
                    armor = armor / 10;

                if (armor < -99)
                    armor = -99;

                if (dmgType.Equals("chaos"))
                    armor = 0;

                dmg = dmg * (100 / (armor + 100));

                float AbsorbedValue = magicShield.getBonusByName(dmgType);
                if (magicShield.getBonusByName(dmgType) > dmg)
                {
                    magicShield.setBonusByName(dmgType, magicShield.getBonusByName(dmgType) - dmg);
                }
                else
                {
                    magicShield.setBonusByName(dmgType, 0);
                }

                dmg -= AbsorbedValue;

                if (dmg < 0)
                    dmg = 0;

                hp -= dmg;



                if (author.master != null)
                {
                    sendDynamicInfosToAll(author.master.id, crit);
                    checkIfDead(author, (int)dmg);
                }
                else
                {
                    sendDynamicInfosToAll(author.id, crit);
                    checkIfDead(author, (int)dmg);
                }
            }
        }

        public void hitMeWithPhysicInZone(String _author, float dmg, float zone)
        {
            Entity author = (Entity)myGame.units[_author + ""];
            foreach (Object o in myGame.units.Keys)
            {
                if (myGame.units[o + ""] != null)
                {
                    Entity tmpUnit = (Entity)myGame.units[o + ""];
                    float tmpDistance = Math.Abs(tmpUnit.position.x - position.x) + Math.Abs(tmpUnit.position.y - position.y)/2 + Math.Abs(tmpUnit.position.z - position.z);
                    if (tmpDistance <= zone)
                    {
                        if (author.team != tmpUnit.team)
                            tmpUnit.hitMeWithPhysic(_author, dmg, lastCrit);
                    }
                }
            }
        }

        public int combatMode = 0;
        public bool enableRewards = true;
        void checkIfDead(Entity _author, int _dmg)
        {
            if (hp < 0)
            {
                if (_author.master != null)
                    _author = _author.master;

                focus = null;
                focusDistance = 999;
                lastHiter.Clear();

                rezCounter = rezInterval;

                if (enableRewards)
                {
                    //set Reward...

                    if (master==null || (master!=null && master.type!=EntityType.player))
                    {
                        if (type == EntityType.player)
                        {
                            //give special rewards...
                        }

                        if (type == EntityType.npc && enableRewards)
                        {
                            float totalDmg = 0;
                            foreach (string s in lastHiter.Keys)
                            {
                                totalDmg += lastHiter[s];
                            }

                            foreach (string s in lastHiter.Keys)
                            {
                                Entity killer = myGame.units[s];
                                if (killer.type == EntityType.player)
                                {
                                    Hero theHero = (Hero)killer;
                                    theHero.addXp(((float)infos.toInt()) * 2 * (lastHiter[s] / totalDmg));

                                    List<Item> rewards = new List<Item>();

                                    float mobRarity = ((float)infos.toInt()) / (5 * infos.level);

                                    for (int i = 0; i < mobRarity; i++)
                                    {
                                        if ((mainSeed).NextDouble() * 100 < 10)
                                        {
                                            Item newItem = myGame.itemGenerator.generateItem("gen", mobRarity, infos.level);
                                            rewards.Add(newItem);
                                        }
                                        else
                                        { 
                                            //try to loot a non generated Item...
                                            if ((mainSeed).NextDouble() * 100 < 10)
                                            {
                                                //fetch a random item around the level of this mob or an ingedient...;
                                                //myGame.worldInfos.getRandomItemByLevel();
                                                //myGame.worldInfos.getCraftItem;
                                                //rewards.Add(myItem);
                                            }
                                        }
                                    }

                                    if (rewards.Count>0)
                                        theHero.itemRewardsByEntity.Add(s, rewards);
                                    float moneyReward = (int)((float)infos.toInt() * (lastHiter[s] / totalDmg));

                                    moneyReward = moneyReward + (float)mainSeed.NextDouble() * moneyReward;

                                    theHero.goldRewardsByEntity.Add(s, (int) moneyReward);
                                }
                            }
                            /*Hero theHero = (Hero)_author;
                            theHero.addXp(((float)infos.toInt()) * 2);
                            theHero.getMyOwner().money += infos.toInt();
                            theHero.sendMoney(id);

                            //show items for drop;
                            if ((mainSeed).NextDouble() * 100 < 10)
                            {
                                float mobRarity = ((float)infos.toInt())/(5*infos.level);

                                Item newItem = myGame.itemGenerator.generateItem("gen", mobRarity, infos.level);

                                theHero.addItem(newItem);
                            }*/
                        }

                    }
                }
            }
            else //if i am not dead
            {
                if (type == EntityType.npc && !_author.Equals(master))
                {
                    try
                    {
                        lastHiter.Add(_author.id, _dmg);
                    }
                    catch (Exception e)
                    {
                        lastHiter[_author.id] = _dmg + (float)lastHiter[_author.id];
                    }
                }

                combatMode = 15;

                if (lastHiter.Count == 1)
                    focus = _author.id;
            }
        }

        public void hitMeWithMagicInZone(String _author, float dmg, String dmgType, float zone)
        {
            Entity author = (Entity)myGame.units[_author + ""];
            foreach (Object o in myGame.units)
            {
                if (myGame.units[o + ""] != null)
                {
                    Entity tmpUnit = (Entity)myGame.units[o + ""];
                    float tmpDistance = Math.Abs(tmpUnit.position.x - position.x) + Math.Abs(tmpUnit.position.y - position.y)/2 + Math.Abs(tmpUnit.position.z - position.z);
                    if (tmpDistance <= zone)
                    {
                        if (author.team != tmpUnit.team)
                            tmpUnit.hitMeWithMagic(_author, dmg, dmgType);
                    }
                }
            }
        }


        public void healMyHPs(Entity author, float dmg)
        {
            hp += dmg;
            if (hp > getMaxHp())
                hp = getMaxHp();

            sendDynamicInfosToAll(author.id);
        }

        public void healMyMPs(Entity author, float dmg)
        {
            mp += dmg;
            if (mp > getMaxMp())
                mp = getMaxMp();

            sendDynamicInfosToAll(author.id);
        }

        public float getMaxHp()
        {
            return infos.vitalInfos.hp + infos.vitalInfosBon.hp;
        }

        public float getMaxMp()
        {
            return infos.vitalInfos.mp + infos.vitalInfosBon.mp;
        }

        public float getAttackValue()
        {
            lastCrit = false;
            float dmg = infos.vitalInfos.dmg + infos.vitalInfosBon.dmg;

            if ((mainSeed).NextDouble()*100 < (infos.vitalInfos.crit + infos.vitalInfosBon.crit))
            {
                lastCrit = true;
                dmg *= (2f + (infos.vitalInfos.critBon + infos.vitalInfosBon.critBon) / 100);
            }

            return dmg;
        }

        public float getAttackSpeed()
        {
            float attackSpeed = 0.5f + 1.5f * 100 / (infos.vitalInfos.attackSpeed + infos.vitalInfosBon.attackSpeed + 100);

            attackSpeed *= 500f/(float)myGame.loopInterval;
            /*if(type.Equals("Tower"))
            {
                attackSpeed = 3f;
            }*/

            return attackSpeed;
        }

        public Vector3 getPosition()
        {
            return position;
        }

        public void sendInfosToMe()
        {
            /*Object[] data = new Object[?];
            data[0] = id; //id


            data.Add("stats", infos.toSFSObject());
            myGame.sendDataToGroup("infos", myGame.players, data);*/

            //SEND ENTITYINFOS HERE
            Object[] data = new Object[15];

            data[0] = id; //id
            data[1] = infos.range; //range
            data[2] = infos.vitalInfos.dmg;
            data[3] = infos.vitalInfosBon.dmg; //dmgBon
            data[4] = infos.vitalInfos.armor + infos.vitalInfosBon.armor; //armor
            data[5] = infos.resBon.totalRes; //res
            data[6] = infos.spellBon.totalBon; //spellBon
            data[7] = infos.vitalInfos.attackSpeed + infos.vitalInfosBon.attackSpeed; //attackSpeed
            data[8] = infos.vitalInfos.spellCrit + infos.vitalInfosBon.spellCrit; //spellCrit
            data[9] = infos.vitalInfos.crit + infos.vitalInfosBon.crit; //crit
            data[10] = infos.vitalInfos.hpRegen + infos.vitalInfosBon.hpRegen; //hpRegen
            data[11] = infos.vitalInfos.mpRegen + infos.vitalInfosBon.mpRegen; //mpRegen
            data[12] = getMaxHp(); //getMaxHp
            data[13] = getMaxMp(); //getMaxMp
            data[14] = xp; //xp
            myGame.sendDataToAll("infos", data, this);
        }

        public void sendDynamicInfosToAll(String author)
        {
            Object[] data = new Object[5];
            data[0] = id; //id
            data[1] = author; //a
            data[2] = false; //crit

            //STATS:
            data[3] = (int)hp;//hp
            data[4] = (int)mp;//mp
            myGame.sendDataToAll("dinfos", data, this);

        }

        public void sendDynamicInfosToAll()
        {
            Object[] data = new Object[9];
            data[0] = id; //id
            data[1] = ""; //a
            data[2] = false; //crit

            //DYNAMIC STATS:
            data[3] = (int)hp;//hp
            data[4] = (int)mp;//mp
            data[5] = (int)getMaxHp();//maxhp
            data[6] = (int)getMaxMp();//maxmp
            data[7] = xp;//xp
            data[8] = level;//level

            myGame.sendDataToAll("dinfos", data, this);
            /*Hashtable tmp = new Hashtable();
           tmp.Add("hp", (int)hp);
           tmp.Add("mp", (int)mp);
    	
           return tmp;*/

            /*ISFSObject tmp = new SFSObject();
            tmp.Add("hp", (int)hp);
            tmp.Add("mp", (int)mp);
            tmp.Add("maxhp", (int)getMaxHp());
            tmp.Add("maxmp", (int)getMaxMp());
            tmp.Add("xp", xp);
            tmp.Add("level", level);
            return tmp;*/
        }

        public void sendDynamicInfosToAll(String author, bool crit)
        {
            Object[] data = new Object[5];
            data[0] = id; //id
            data[1] = author; //a
            data[2] = crit; //crit

            //STATS:
            data[3] = (int)hp;//hp
            data[4] = (int)mp;//mp

            myGame.sendDataToAll("dinfos", data, this);
        }

        public void sendCast(String spell, String target)
        {
            Object[] data = new Object[5];
            data[0] = id; //i
            data[1] = "Cast";  //anim
            data[2] = 15;//time
            data[3] = spell;//spell
            data[4] = target; //target if any

            myGame.sendDataToAll("anim", data, this);
        }

        public void sendSkillCast(String spell, String target)
        {
            Object[] data = new Object[5];
            data[0] = id; //i
            data[1] = "Skill";  //anim
            data[2] = 15;//time
            data[3] = spell;//spell
            data[4] = target; //target if any

            myGame.sendDataToAll("anim", data, this);
        }

        public void sendIncant(String spell, int duration)
        {
            Object[] data = new Object[4];
            data[0] = id; //i
            data[1] = "incant";  //anim
            data[2] = duration;//time
            data[3] = spell;//spell
            
            myGame.sendDataToAll("anim", data, this);
        }

        public void sendAnim(String _anim, string target)
        {
            Object[] data = new Object[4];
            data[0] = id; //i
            data[1] = _anim;  //anim
            data[2] = 15;//time
            data[3] = target;//target

            myGame.sendDataToAll("anim", data, this);
        }

        public void sendCast(int duration)
        {
            Object[] data = new Object[3];
            data[0] = id; //i
            data[1] = "Cast";  //anim
            data[2] = duration;//time


            myGame.sendDataToAll("anim", data, this);
        }

        public void sendCast(float tx, float ty, float tz)
        {
            Object[] data = new Object[6];
            data[0] = id; //i
            data[1] = "Cast";  //anim
            data[2] = 15;//time
            data[3] = (int)tx;//tx
            data[4] = (int)ty; //ty
            data[5] = (int)tz; //tz

            myGame.sendDataToAll("anim", data, this);
        }

        public void sendCast(Item myItem)
        {
            Object[] data = new Object[3];
            data[0] = id; //i
            data[1] = "Cast";  //anim
            data[2] = 15;//time
            data[3] = myItem.id;

            myGame.sendDataToAll("anim", data, this);
        }

        public void sendTeleport()
        {
            Object[] data = new Object[4];
            data[0] = id; //i
            data[1] = position.x;  //x
            data[2] = position.y;
            data[3] = position.z; //z

            myGame.sendDataToAll("teleport", data, this);
        }

        public void sendFocus()
        {
            Object[] data = new Object[1];
            data[0] = focus; //i
            myGame.sendDataToAll("focus", data, this);
        }

        public void sendSpells()
        {
            /*ISFSObject data = new SFSObject();
            data.Add("spells", spells);
            data["id", id);
            myGame.send("spells",data,getMyOwner());*/

            Object[] data = new Object[9];
            data[0] = id; //i
            for (int n = 1; n < 9; n++)
            {
                try
                {
                    data[n] = ((Hashtable)spells["" + (n - 1)])["id"];
                }
                catch (Exception e)
                {
                    data[n] = "";
                }
            }

            myGame.sendDataToAll("spells", data, this);
        }

        public void sendSpells(Player player)
        {
            /*ISFSObject data = new SFSObject();
            data.Add("spells", spells);
            data["id", id);
            myGame.send("spells",data,getMyOwner());*/

            Object[] data = new Object[1 + spells.Count*3];
            data[0] = id; //i
            int counter = 0;
            foreach (string s in spells.Keys)
            {
                data[counter + 1] = ((Hashtable)spells[s])["id"];
                data[counter + 2] = ((Hashtable)spells[s])["rank"];
                data[counter + 3] = ((Hashtable)spells[s])["cd"];

                counter += 3;
            }

            player.Send("spells", data);

            //myGame.PlayerIO.ErrorLog.WriteError("spells: " + spells.Count);
        }


        public float getDistance(Entity otherUnit)
        {
            return otherUnit.position.Substract(position).SqrMagnitude();
        }

        public float getDistance(Vector3 point)
        {
            return point.Substract(position).SqrMagnitude();
        }

        public float get2DDistance(Vector3 point)
        {
            return (float)Math.Sqrt((point.x - position.x) * (point.x - position.x) + (point.z - position.z) * (point.z - position.z));
        }

        public Player getMyOwner()
        {
            return myController;
        }

        public void sendPos(Vector3 diff)
        {
            Object[] data = new Object[4];
            data[0] = id; //i
            data[1] = position.x;  //x
            data[2] = position.y;
            data[3] = position.z; //z

            myGame.sendDataToAll("p", data, this);

        }

        public void sendLocalPos(Vector3 _localPos, String _parentName)
        {
            Object[] data = new Object[5];
            data[0] = id; //i
            data[1] = _localPos.x;  //x
            data[2] = _localPos.y;
            data[3] = _localPos.z; //z


            data[4] = _parentName; //ix

            myGame.sendDataToAll("lp", data, this);

        }

        public void sendPos()
        {
            //myGame.PlayerIO.ErrorLog.WriteError("sendPos id: " + id + " visiblePlayers: "+visiblePlayers.Count);
            Object[] data = new Object[7];
            data[0] = id; //i
            data[1] = position.x;  //x
            data[2] = position.y;
            data[3] = position.z; //z


            data[4] = 0; //ix
            data[5] = 0; //iy
            data[6] = 0; //iz
            myGame.sendDataToAll("p", data, this);

        }

        public void sendRider()
        {
            Object[] data = new Object[2];
            data[0] = id; //i
            try
            {
                data[1] = riding.id;  //x
            }
            catch (Exception e)
            {
                data[1] = "";  //x
            }
            myGame.sendDataToAll("mount", data, this);
        }

        bool hasMoved = false;
        public int movementCounter = 0;
        public float stun = 0;
        public float snare = 0;
        public float snareRatio = 0;
        void synchronizePosition()
        {
            if (grabbed > 0)
            {
                grabbed--;
                return;
            }

            if (!isSynchronized())
            {
                hasMoved = true;
                movementCounter = 60;

                float calculatedSpeed = infos.baseSpeed;//infos.baseSpeed * ((float)myGame.loopInterval/1000f);

                if (incantation != null || canalisedSpell != null || stun>0)
                    calculatedSpeed = 0;

                if (position.x < destination.x)
                    position.x += calculatedSpeed;
                if (position.x > destination.x)
                    position.x -= calculatedSpeed;

                if (position.y < destination.y)
                    position.y += calculatedSpeed;
                if (position.y > destination.y)
                    position.y -= calculatedSpeed;

                if (position.z < destination.z)
                    position.z += calculatedSpeed;
                if (position.z > destination.z)
                    position.z -= calculatedSpeed;

                if (Math.Abs(position.z - destination.z) <= calculatedSpeed)
                {
                    position.z = destination.z;
                }

                if (Math.Abs(position.x - destination.x) <= calculatedSpeed)
                {
                    position.x = destination.x;
                }

                if (Math.Abs(position.y - destination.y) <= calculatedSpeed)
                {
                    position.y = destination.y;
                }
            }
            else
                hasMoved = false;

            if (movementCounter>0)
                movementCounter--;

            if (stun > 0)
            {
                stun--;
            }
        }

        public bool isSynchronized()
        {
            Vector3 tmpPos = new Vector3(position);
            //tmpPos.y = destination.y;
            return !(tmpPos.Substract(destination).Magnitude() > infos.baseSpeed && !destination.isZero());
        }

        public String getPosRefId()
        {
            return position.toPosRefId(myGame.baseRefSize);
        }

        public String getStepRefId()
        {
            return position.toPosRefId(myGame.baseStep);
        }

        public Entity clone()
        {
            Entity tmpEntity = new Entity(myGame, id, name, infos, position);
            tmpEntity.agressivity = agressivity;
            tmpEntity.checkRange = checkRange;
            tmpEntity.viewRange = viewRange;
            tmpEntity.wanderAround = wanderAround;
            tmpEntity.hp = hp;
            tmpEntity.mp = mp;
            tmpEntity.master = master;
            tmpEntity.level = level;
            tmpEntity.items = items;
            tmpEntity.spells = spells;
            tmpEntity.spawnZone = spawnZone;
            tmpEntity.team = team;
            tmpEntity.ridable = ridable;
            tmpEntity.myController = myController;
            tmpEntity.myTrigger = myTrigger;
            tmpEntity.isPatrol = isPatrol;
            tmpEntity.initialPosition = initialPosition;
            tmpEntity.focus = focus;
            tmpEntity.enableRewards = enableRewards;
            tmpEntity.destination = destination;
            tmpEntity.decalage = decalage;
            tmpEntity.buffs = buffs;

            return tmpEntity;
        }

        public Entity getRandomEnnemy()
        {
            foreach(String s in visibleEnemies.Keys)
            {
                return myGame.units[s];
            }
            return null;
        }

        public String toString()
        {
            return name;
        }
    }
}
