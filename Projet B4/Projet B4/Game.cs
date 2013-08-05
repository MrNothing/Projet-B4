using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PlayerIO.GameLibrary;

namespace ProjetB4 {
	[RoomType("Game")]
public class GameCode : Game<Player> {

        public String zoneName;
        public MapsData mapsInfos;
		public WorldInfos worldInfos;
		
        public Dictionary<String, Entity> units = new Dictionary<String, Entity>(); //contains all entities
        public Dictionary<String, Player> players = new Dictionary<String, Player>(); //contains all entities
        public Dictionary<String, Dictionary<String, String>> inGameUnitsRefs = new Dictionary<String, Dictionary<String, String>>();

        public Dictionary<int, Event> events = new Dictionary<int, Event>();
        public Dictionary<String, SpawnZone> spawnZones = new Dictionary<String, SpawnZone>();

        public EventsManager eventsManager = new EventsManager();
        public SpellsManager spellsManager;
        public ChatManager chatManager;
        public GameManager gameManager;
		
		public ItemGenerator itemGenerator = new ItemGenerator();

        public float baseStep = 1;
        public float baseRefSize=100;
        public int loopInterval=100;

        public int defaultUnitCounter = 0;

        public Random mainSeed = new Random();

        Timer mainTimer;

		// This method is called when an instance of your the game is created
		public override void GameStarted() {
		
			//load the classes generated with the World Editor
            mapsInfos = new MapsData();
			worldInfos = new WorldInfos();
			
			//enable this for persistence
            PreloadPlayerObjects = true;
			
			//load current map
            try
			{
                mapsInfos.loadMap(this, RoomData["map"]);
                zoneName = RoomData["map"];
            }
			catch(Exception e)
			{
				//the requested map does not exist, disconnect user and abord map initialization.
				//return;
                PlayerIO.ErrorLog.WriteError("Map failed: " + RoomData["map"]);
			}
			
			//this method load all entities, and events for this zone.
			initializeZone();

            spellsManager = new SpellsManager(this);
            chatManager = new ChatManager(this);
            gameManager = new GameManager(this, spellsManager);

            PlayerIO.ErrorLog.WriteError("test Item infos:"+itemGenerator.generateItem("0", 10, 10).toString());

			//this is the main routine 
            mainTimer = AddTimer(run, loopInterval);
		}

        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message)
        {
            if (message.Type.Equals("game"))
            {
                //go to game manager...
                gameManager.handleClientRequest(player, message.GetString(0), message);
            }

            if (message.Type.Equals("chat"))
            {
                //go to chat manager...
                chatManager.handleClientRequest(player, message.GetString(0), message);
            }

            if (message.Type.Equals("ping"))
            {
                player.Send("ping", 0);
            }
        }


		// This method is called when the last player leaves the room, and it's closed down.
		public override void GameClosed() {
            mainTimer.Stop();
		}

		// This method is called whenever a player joins the game
        public override void UserJoined(Player player)
        {
            loadUser(player);
            /*EntityInfos tmpInfos = new EntityInfos();
            tmpInfos.model = "PlayerMaleHuman";
            player.myCharacter = new Hero(this, player.ConnectUserId, player, tmpInfos, player.ConnectUserId, new Dictionary<String, Hashtable>(), new Dictionary<String, Item>());
            units[player.ConnectUserId] = player.myCharacter;

            player.Send("map", zone.zoneName);*/

        }
		
        public void loadUser(Player player)
        {
            //create player Object.

            PlayerIO.ErrorLog.WriteError("User Joined: " + player.ConnectUserId);

            String location = "";

            try
            {
                /*if (player.PlayerObject.GetBool("online", false) == false) { player.PlayerObject.Set("online", true); player.PlayerObject.Save(); }
                else
                {
                    player.Send("err", "e3");
                    //return;
                }*/

                player.myCharacter = new Hero(this, player.ConnectUserId, player, new EntityInfos(), player.ConnectUserId, new Dictionary<String, Hashtable>(), new Dictionary<String, Item>());

                location = "0";

                if (player.PlayerObject.GetInt("GMLevel", -1) == -1) {player.PlayerObject.Set("GMLevel", 0); player.PlayerObject.Save(); }
                
                    player.GM = player.PlayerObject.GetInt("GMLevel") > 0;

                location = "1";

                if (player.PlayerObject.GetString("map", "").Equals("")) { player.PlayerObject.Set("map", "Map0"); player.PlayerObject.Save(); }

                location = "2";

                if (player.PlayerObject.GetInt("level", -1) == -1) { player.PlayerObject.Set("level", 1); player.PlayerObject.Save(); }
                
                    player.myCharacter.level = player.PlayerObject.GetInt("level");

                location = "3";

                if (player.PlayerObject.GetString("model", "").Equals("")) {
                    player.PlayerObject.Set("model", "GuardMale"); 
                    player.PlayerObject.Save(); 
                }
                
                    player.myCharacter.infos.model = player.PlayerObject.GetString("model");

                location = "4";

                if (player.PlayerObject.GetInt("money", -1) == -1) { player.PlayerObject.Set("money", 0); player.PlayerObject.Save(); }
                else
                    player.money = player.PlayerObject.GetInt("money");

                location = "5";

                if (player.PlayerObject.GetInt("xp", -1) == -1) { player.PlayerObject.Set("xp", 0); player.PlayerObject.Save(); }
                
                    player.myCharacter.xp = player.PlayerObject.GetInt("xp");

                location = "6";

                if (player.PlayerObject.GetInt("skillPoints", -1) == -1) { player.PlayerObject.Set("skillPoints", 0); player.PlayerObject.Save(); }
                
                    player.myCharacter.skillPoints = player.PlayerObject.GetInt("skillPoints");

                location = "7";

                if (player.PlayerObject.GetFloat("x", -1) == -1) { player.PlayerObject.Set("x", 0f); player.PlayerObject.Save(); }
                
                    player.myCharacter.position.x = (float)player.PlayerObject.GetFloat("x");


                if (player.PlayerObject.GetFloat("y", -1) == -1) { player.PlayerObject.Set("y", 0f); player.PlayerObject.Save(); }
                
                    player.myCharacter.position.y = (float)player.PlayerObject.GetFloat("y");

                if (player.PlayerObject.GetFloat("z", -1) == -1) { player.PlayerObject.Set("z", 0f); player.PlayerObject.Save(); }
                
                    player.myCharacter.position.z = (float)player.PlayerObject.GetFloat("z");

                location = "10";

                if (player.PlayerObject.GetInt("sta", -1) <= 0) { player.PlayerObject.Set("sta", 5); player.PlayerObject.Save(); }
                
                    player.myCharacter.infos.baseStats.sta = (float)player.PlayerObject.GetInt("sta");

                location = "10.5";

                if (player.PlayerObject.GetInt("agi", -1) < 0) { player.PlayerObject.Set("agi", 5); player.PlayerObject.Save(); }
                
                    player.myCharacter.infos.baseStats.agi = (float)player.PlayerObject.GetInt("agi");

                    if (player.PlayerObject.GetInt("str", -1) < 0) { player.PlayerObject.Set("str", 5); player.PlayerObject.Save(); }
                
                    player.myCharacter.infos.baseStats.str = (float)player.PlayerObject.GetInt("str");

                location = "11";

                if (player.PlayerObject.GetInt("int", -1) < 0) { player.PlayerObject.Set("int", 5); player.PlayerObject.Save(); }
                
                    player.myCharacter.infos.baseStats.intel = (float)player.PlayerObject.GetInt("int");

                    if (player.PlayerObject.GetInt("sou", -1) < 0) { player.PlayerObject.Set("sou", 5); player.PlayerObject.Save(); }
                
                    player.myCharacter.infos.baseStats.sou = (float)player.PlayerObject.GetInt("sou");

                player.myCharacter.applyBaseStatsToVitalInfos();

                if (player.PlayerObject.GetInt("hp", -1) == -1) { player.PlayerObject.Set("hp", 0); player.PlayerObject.Save(); }

                player.myCharacter.hp = player.PlayerObject.GetInt("hp");

                location = "8";

                if (player.PlayerObject.GetInt("mp", -1) == -1) { player.PlayerObject.Set("mp", 0); player.PlayerObject.Save(); }

                player.myCharacter.mp = player.PlayerObject.GetInt("mp");

                location = "9";

                //load Bag Items
                for (int n = 0; ; n++)
                {
                    if (player.PlayerObject.GetInt("item_" + n, -1) != -1)
                    {
                        String tL = "0";
                        //PlayerIO.ErrorLog.WriteError("item FOUND: "+player.PlayerObject.GetString("item_" + n + "_name"));
                        try
                        {
                            //load item Infos...
                            ItemPattern errorItemPattern = new ItemPattern("Error");
                            errorItemPattern.icon = "error";
                            errorItemPattern.description = "This item could not be loaded";

							Item loadedItem =  new Item(errorItemPattern);

							if(!player.PlayerObject.GetString("item_" + n + "_generated", "N/A").Equals("N/A"))
							{
                                try
                                {
                                    loadedItem = itemGenerator.parseItem(player.PlayerObject.GetString("item_" + n + "_generated", "N/A"), this);
                                }
                                catch (Exception e)
                                {
                                    errorItemPattern.description = "This item could not be parsed, it must be corrupted. Please report this."; 
                                }
                            }
							else
							{
                                try
                                {
								    loadedItem = worldInfos.getItemByName(player.PlayerObject.GetString("item_" + n + "_name"));
                                }
                                catch (Exception e)
                                {
                                    errorItemPattern.description = "This item could not be found. The database was alterated? Please report this.";
                                } 
                                
                                tL = "1";
							}

                            loadedItem.id = n + "";
                            loadedItem.cooldown = player.PlayerObject.GetInt("item_" + n + "_cd");
                            loadedItem.uses = player.PlayerObject.GetInt("item_" + n + "_uses");
                            loadedItem.equipped = player.PlayerObject.GetBool("item_" + n + "_equipped");
                               
                            player.myCharacter.itemsCounter = n + 1;

                            player.myCharacter.items.Add(n + "", loadedItem);

                            player.myCharacter.bagWeight += loadedItem.infos.weight;

                            try
                            {
                                float amount = (float)player.myCharacter.itemsByName[loadedItem.infos.name];

                                amount += loadedItem.infos.weight;
                                player.myCharacter.itemsByName.Remove(loadedItem.infos.name);
                                player.myCharacter.itemsByName.Add(loadedItem.infos.name, amount);
                            }
                            catch(Exception e)
                            {
                                player.myCharacter.itemsByName.Add(loadedItem.infos.name, loadedItem.infos.weight);
                            }

                            if (loadedItem.equipped)
                            {
                                player.myCharacter.equipItem(loadedItem.id, true);
                                player.myCharacter.equippedItems.Add(loadedItem.infos.slot + "", loadedItem.id);
                            }
                        }
                        catch (Exception e2)
                        {
                            PlayerIO.ErrorLog.WriteError("itemLoad error, location: " + tL + "error: " + e2.Message);                        
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //load Spells
                for (int n = 0; ; n++)
                {
                    int decal = 0;
                    if (player.PlayerObject.GetInt("spell_" + n + "_rank", -1) != -1)
                    {
                        PlayerIO.ErrorLog.WriteError("spell FOUND: " + player.PlayerObject.GetString("spell_" + n + "_name"));
                        //load item Infos...
                        Hashtable loadedSpell = (Hashtable)(new SpellInfos()).allSpells[player.PlayerObject.GetString("spell_" + n + "_name")]; // (player.PlayerObject.GetString("spell_" + n + "_name"));
                        loadedSpell["rank"] = player.PlayerObject.GetInt("spell_" + n + "_rank");
                        loadedSpell["cd"] = player.PlayerObject.GetInt("spell_" + n + "_cd");
                        int loc = 0;
                        try
                        {
                            player.myCharacter.spellsByName.Add(loadedSpell["name"], true);
                            loc++;
                            player.myCharacter.spells.Add((n - decal) + "", loadedSpell);
                            loc++;
                            player.myCharacter.spellsCounter = (n - decal) + 1;
                            loc++;
                            spellsManager.applyPassiveEffects(player.myCharacter, loadedSpell["name"]+"");
                        }
                        catch (Exception e)
                        {
                            decal += 1;
                            PlayerIO.ErrorLog.WriteError("spell FAILED: " + loc);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                location = "17";

                units[player.ConnectUserId] = player.myCharacter;
                players[player.ConnectUserId] = player;

                //units[player.ConnectUserId].setRef();

                //player.Send("map", zone.zoneName);

                /*foreach (String s in units.Keys)
                {
                   if (!s.Equals( player.ConnectUserId))
                        sendEntityInfos(player, units[s]);
                }*/

                sendEntityInfos(player, player.myCharacter);

                location = "sendSpells ";
                //send my spells -> disabled since the infos are sent in a different way now
                player.myCharacter.sendSpells(player);

                location = "sendItems ";
                //send Items -> disabled since the infos are sent in a different way now
                player.myCharacter.sendItems(player);

                player.myCharacter.sendMoney();
            }
            catch(Exception e)
            {
                PlayerIO.ErrorLog.WriteError("playerJoined error, location: " + location + "error: "+e.Message);
            }
        }
            
		// This method is called when a player leaves the game
		public override void UserLeft(Player player) {
            String location = "";
            try
            {
                player.PlayerObject.Set("online", false);

                try
                {
                    player.PlayerObject.Set("map", zoneName);
                }
                catch (Exception e)
                { 
                
                }

                player.PlayerObject.Set("level", (int)player.myCharacter.level);
                player.PlayerObject.Set("model", player.myCharacter.infos.model);
                player.PlayerObject.Set("money", (int)player.money);
                player.PlayerObject.Set("hp", (int)player.myCharacter.hp);
                player.PlayerObject.Set("mp", (int)player.myCharacter.mp);
                player.PlayerObject.Set("xp", (int)player.myCharacter.xp);
                player.PlayerObject.Set("skillPoints", (int)player.myCharacter.skillPoints);

                location = "1";

                player.PlayerObject.Set("x", player.myCharacter.position.x);
                player.PlayerObject.Set("y", player.myCharacter.position.y);
                player.PlayerObject.Set("z", player.myCharacter.position.z);
                player.PlayerObject.Set("sta", (int)player.myCharacter.infos.baseStats.sta);
                player.PlayerObject.Set("agi", (int)player.myCharacter.infos.baseStats.agi);
                player.PlayerObject.Set("str", (int)player.myCharacter.infos.baseStats.str);
                player.PlayerObject.Set("int", (int)player.myCharacter.infos.baseStats.intel);
                player.PlayerObject.Set("sou", (int)player.myCharacter.infos.baseStats.sou);

                location = "2";

                /*player.PlayerObject.Set("head", player.myCharacter.equippedItems["head"] + "");
                player.PlayerObject.Set("shoulders", player.myCharacter.equippedItems["shoulders"] + "");
                player.PlayerObject.Set("neck", player.myCharacter.equippedItems["neck"] + "");
                player.PlayerObject.Set("chest", player.myCharacter.equippedItems["chest"] + "");
                player.PlayerObject.Set("hands", player.myCharacter.equippedItems["hands"] + "");
                player.PlayerObject.Set("wrists", player.myCharacter.equippedItems["wrists"] + "");
                player.PlayerObject.Set("waist", player.myCharacter.equippedItems["waist"] + "");
                player.PlayerObject.Set("legs", player.myCharacter.equippedItems["legs"] + "");
                player.PlayerObject.Set("feets", player.myCharacter.equippedItems["feets"] + "");
                player.PlayerObject.Set("fingers", player.myCharacter.equippedItems["fingers"] + "");
                player.PlayerObject.Set("jewel", player.myCharacter.equippedItems["jewel"] + "");
                player.PlayerObject.Set("leftHand", player.myCharacter.equippedItems["leftHand"] + "");
                player.PlayerObject.Set("rightHand", player.myCharacter.equippedItems["rightHand"] + "");
                player.PlayerObject.Set("bothHands", player.myCharacter.equippedItems["bothHands"] + "");

                location = "3";
                */

                //flush Items
                for(int tmpD1 = 0; player.PlayerObject.GetInt("item_" + tmpD1, -1) != -1; tmpD1++)
                {
                    player.PlayerObject.Remove("item_" + tmpD1);
                }

                //save items
                int tmpC1 = 0;
                foreach (String s in player.myCharacter.items.Keys)
                {
                    Item tmpitem = player.myCharacter.items[s];

                    if (!tmpitem.generated)
                    {
                        player.PlayerObject.Set("item_" + tmpC1, 1);
                        player.PlayerObject.Set("item_" + tmpC1 + "_name", tmpitem.infos.name);
                        player.PlayerObject.Set("item_" + tmpC1 + "_cd", (int)tmpitem.infos.coolDown);
                        player.PlayerObject.Set("item_" + tmpC1 + "_uses", (int)tmpitem.uses);
                        player.PlayerObject.Set("item_" + tmpC1 + "_equipped", tmpitem.equipped);
                    }
                    else
                    {
                        try
                        {
                            player.PlayerObject.Set("item_" + tmpC1, 1);
                            player.PlayerObject.Set("item_" + tmpC1 + "_generated", itemGenerator.exportItem(tmpitem));
                            player.PlayerObject.Set("item_" + tmpC1 + "_name", tmpitem.infos.name);
                            player.PlayerObject.Set("item_" + tmpC1 + "_cd", (int)tmpitem.infos.coolDown);
                            player.PlayerObject.Set("item_" + tmpC1 + "_uses", (int)tmpitem.uses);
                            player.PlayerObject.Set("item_" + tmpC1 + "_equipped", tmpitem.equipped);
                        }
                        catch (Exception e)
                        {
                            PlayerIO.ErrorLog.WriteError("Could not save generated item!");
                        }
                    }

                    tmpC1++;
                }

                //save spells
                int tmpC2 = 0;
                foreach (String s in player.myCharacter.spells.Keys)
                {
                    Hashtable tmpitem = player.myCharacter.spells[s];
                    player.PlayerObject.Set("spell_" + tmpC2 + "_name", tmpitem["id"] + "");
                    player.PlayerObject.Set("spell_" + tmpC2 + "_rank", (int)tmpitem["rank"]);
                    player.PlayerObject.Set("spell_" + tmpC2 + "_cd", (int)tmpitem["cd"]);
                    tmpC2++;
                }

                player.PlayerObject.Save();
                
                //PlayerIO.ErrorLog.WriteError("My Position is: " + player.myCharacter.position.toString());
            }
            catch (Exception e)
            {
                PlayerIO.ErrorLog.WriteError("userLeft error, location: " + location + "error: " + e.Message);
            }

            try
            {
                units.Remove(player.ConnectUserId);
                players.Remove(player.ConnectUserId);
            }
            catch (Exception e)
            {
                PlayerIO.ErrorLog.WriteError("there was no player to remove!");
            }
            Broadcast("uLeave", player.ConnectUserId);
		}

        public void sendDataToAll(String id, Object[] Data, Entity sender)
        {
            foreach (Object o in players.Keys)
            {
                if ((Player)units[o + ""].getMyOwner() != null)
                {
                    Player tmpPlayer = players[o + ""];

                    try
                    {
                        if (sender.position.Substract(tmpPlayer.myCharacter.position).Magnitude()<baseRefSize)
                            tmpPlayer.Send(id, Data);
                    }
                    catch (Exception e)
                    {
                        //System.out.println("sendDataToGroup failed! msg: "+e.getMessage());
                    }


                }
            }
        }

        /*public void sendDataToGroup(String _cmd, Dictionary<String, String> group, Object[] data)
        {
            try
            {

                foreach (Object o in group.Keys)
                {
                    if ((Player)units[o + ""].getMyOwner() != null)
                    {
                        Player tmpPlayer = (Player)units[o + ""].getMyOwner();

                        try
                        {
                            tmpPlayer.Send(_cmd, data);
                        }
                        catch (Exception e)
                        {
                            //System.out.println("sendDataToGroup failed! msg: "+e.getMessage());
                        }


                    }
                }
            }
            catch (Exception e)
            {
                //System.out.println("sendDataToGroup failed! reason: "+e.getMessage());
            }
        }
        */

        public void run()
        {
            //main loop
            int testCounter = 0;
            int exceptionsCounter = 0;
            string lastException = "";
            foreach (String s in units.Keys)
            {
                try
                {
                    Entity tmpUnit = (Entity)units[s];
                    tmpUnit.run();
                    testCounter++;
                }
                catch (Exception e)
                {
                    exceptionsCounter++;
                    lastException = e.Message;
                    //print error!
                }
            }

            //PlayerIO.ErrorLog.WriteError("testCounter reached: " + testCounter + "exceptions: " + exceptionsCounter + "lastException: " + lastException);

            foreach (String s in spawnZones.Keys)
            {
                SpawnZone tmpZone=null;
                try
                {
                    tmpZone = (SpawnZone)spawnZones[s];
                    tmpZone.run();
                }
                catch (Exception e)
                {
                    //print error!
                    PlayerIO.ErrorLog.WriteError("spawZones Location: " + tmpZone.counter);
                }
            }

            eventsManager.run();
        }

        public void sendEntityInfos(Player player, Entity myUnit)
        {
            string errLocation = "";
            try
            {
                Object[] data = new Object[29];
               
               /* if (myUnit.master.Equals(player.myCharacter))
                {*/
                    //data[0] = -1; //mine
                /*}*/
                //else
                {
                    if (myUnit.team.Equals(player.myCharacter.team))
                    {
                        data[0] = 0; //agressive
                    }
                    else
                    {
                        if(myUnit.agressivity == AgressivityLevel.passive)
                        data[0] = 1; //agressive
                        else
                        data[0] = 2; //agressive
                    }
                }
               
                data[1] = myUnit.infos.model;              //name
                data[2] = myUnit.type.ToString();              //type

                //DYNAMIC STATS:
                data[3] = (int)myUnit.hp;           //hp
                data[4] = (int)myUnit.mp;           //mp
                data[5] = (int)myUnit.getMaxHp();   //maxhp
                data[6] = (int)myUnit.getMaxMp();   //maxmp
                data[7] = myUnit.xp;                //xp
                data[8] = myUnit.level;             //level

                //data.Add("messages", myUnit.messages);

                //data.Add("maxHp", myUnit.getMaxHp());
                //data.Add("maxMp", myUnit.getMaxMp());

                errLocation = "4";

                try
                {
                    data[9] = myUnit.myController.ConnectUserId; //owner
                }
                catch (Exception e) 
                {
                    data[9] = ""; //owner
                }
                data[10] = myUnit.id; //id
                data[11] = myUnit.infos.baseSpeed; //speed
                data[12] = myUnit.team; //team

                errLocation = "5";

                //data.Add("items", myUnit.items);
                //SEND ITEMS HERE!

                /*if (myUnit.owner.Equals(player.name))
                {
                    //System.out.println("I have spells!");
                    //data.Add("spells", myUnit.spells);
                    //SEND SPELLS HERE!

                    for (int n = 0; n < 8; n++)
                    {
                        try
                        {
                            data[27 + n] = ((Hashtable)myUnit.spells["" + (27 + n)])["id"];
                        }
                        catch (Exception e)
                        {
                            data[27 + n] = "";
                        }
                    }

                }
                else
                {
                    for (int n = 0; n < 8; n++)
                    {
                        data[27 + n] = "";
                    }
                }*/

                //data.Add("entityInfos", myUnit.infos.toHashtable());
                //SEND ENTITYINFOS HERE
                data[16] = myUnit.infos.range; //range
                data[17] = myUnit.infos.vitalInfos.dmg;
                data[18] = myUnit.infos.vitalInfosBon.dmg; //dmgBon
                data[19] = myUnit.infos.vitalInfos.armor + myUnit.infos.vitalInfosBon.armor; //armor
                data[20] = myUnit.infos.resBon.totalRes; //res
                data[21] = myUnit.infos.spellBon.totalBon; //spellBon
                data[22] = myUnit.infos.vitalInfos.attackSpeed + myUnit.infos.vitalInfosBon.attackSpeed; //attackSpeed
                data[23] = myUnit.infos.vitalInfos.spellCrit; //spellCrit
                data[24] = myUnit.infos.vitalInfos.crit; //crit
                data[25] = myUnit.infos.vitalInfos.hpRegen; //hpRegen
                data[26] = myUnit.infos.vitalInfos.mpRegen; //mpRegen

                data[27] = myUnit.myTrigger.activated; //trigger status


                try
                {
                    data[28] = myUnit.riding.id; //trigger status
                }
                catch (Exception e)
                {
                    data[28] = ""; //owner
                }
                //}
                //System.out.println("myUnit.speed: "+myUnit.speed);

                errLocation = "5.1";

                data[13] = myUnit.position.x; //x
                data[14] = myUnit.position.y; //y
                data[15] = myUnit.position.z; //z
                player.Send("req", data);

                errLocation = "6";

                if (myUnit.type.Equals("Hero"))
                {

                   //  ((Hero)myUnit).sendSpells(player);

                   //   ((Hero)myUnit).sendItems(player);
                }
            }
            catch (Exception e)
            {
                PlayerIO.ErrorLog.WriteError("sendEntityInfos failed at:" + errLocation + " error: " + e.Message);
                player.Send("_err", "sendEntityInfos failed at:" + errLocation + " error: " + e.Message);
            }

        }

        public void sendEntityInfosToAll(Entity myUnit)
        {
            string errLocation = "";
            try
            {
                Object[] data = new Object[29];

                /* if (myUnit.master.Equals(player.myCharacter))
                 {*/
                //data[0] = -1; //mine
                /*}*/
                //else
               // {
                  //  if (myUnit.team.Equals(player.myCharacter.team))
                   // {
                        data[0] = 0; //agressive
                   // }
                   // else
                   // {
                   //     if (myUnit.agressivity == AgressivityLevel.passive)
                   //         data[0] = 1; //agressive
                   //     else
                   //         data[0] = 2; //agressive
                   // }
               // }

                data[1] = myUnit.infos.model;              //name
                data[2] = myUnit.type.ToString();              //type

                //DYNAMIC STATS:
                data[3] = (int)myUnit.hp;           //hp
                data[4] = (int)myUnit.mp;           //mp
                data[5] = (int)myUnit.getMaxHp();   //maxhp
                data[6] = (int)myUnit.getMaxMp();   //maxmp
                data[7] = myUnit.xp;                //xp
                data[8] = myUnit.level;             //level

                //data.Add("messages", myUnit.messages);

                //data.Add("maxHp", myUnit.getMaxHp());
                //data.Add("maxMp", myUnit.getMaxMp());

                errLocation = "4";

                try
                {
                    data[9] = myUnit.myController.ConnectUserId; //owner
                }
                catch (Exception e)
                {
                    data[9] = ""; //owner
                }
                data[10] = myUnit.id; //id
                data[11] = myUnit.infos.baseSpeed; //speed
                data[12] = myUnit.team; //team

                errLocation = "5";

                //data.Add("items", myUnit.items);
                //SEND ITEMS HERE!

                /*if (myUnit.owner.Equals(player.name))
                {
                    //System.out.println("I have spells!");
                    //data.Add("spells", myUnit.spells);
                    //SEND SPELLS HERE!

                    for (int n = 0; n < 8; n++)
                    {
                        try
                        {
                            data[27 + n] = ((Hashtable)myUnit.spells["" + (27 + n)])["id"];
                        }
                        catch (Exception e)
                        {
                            data[27 + n] = "";
                        }
                    }

                }
                else
                {
                    for (int n = 0; n < 8; n++)
                    {
                        data[27 + n] = "";
                    }
                }*/

                //data.Add("entityInfos", myUnit.infos.toHashtable());
                //SEND ENTITYINFOS HERE
                data[16] = myUnit.infos.range; //range
                data[17] = myUnit.infos.vitalInfos.dmg;
                data[18] = myUnit.infos.vitalInfosBon.dmg; //dmgBon
                data[19] = myUnit.infos.vitalInfos.armor + myUnit.infos.vitalInfosBon.armor; //armor
                data[20] = myUnit.infos.resBon.totalRes; //res
                data[21] = myUnit.infos.spellBon.totalBon; //spellBon
                data[22] = myUnit.infos.vitalInfos.attackSpeed + myUnit.infos.vitalInfosBon.attackSpeed; //attackSpeed
                data[23] = myUnit.infos.vitalInfos.spellCrit; //spellCrit
                data[24] = myUnit.infos.vitalInfos.crit; //crit
                data[25] = myUnit.infos.vitalInfos.hpRegen; //hpRegen
                data[26] = myUnit.infos.vitalInfos.mpRegen; //mpRegen

                data[27] = myUnit.myTrigger.activated; //trigger status

                try
                {
                    data[28] = myUnit.riding.id; //trigger status
                }
                catch (Exception e)
                {
                    data[28] = ""; //owner
                }

                //}
                //System.out.println("myUnit.speed: "+myUnit.speed);

                errLocation = "5.1";

                data[13] = myUnit.position.x; //x
                data[14] = myUnit.position.y; //y
                data[15] = myUnit.position.z; //z
                sendDataToAll("req", data, myUnit);

            }
            catch (Exception e)
            {
                PlayerIO.ErrorLog.WriteError("unitInfos failed at:" + errLocation + " error: " + e.Message);
            }

        }

        public Player getPlayerById(String id)
        {
            return (Player)(units[id].getMyOwner());
        }

        public Player getPlayerByName(String name)
        {
            return (Player)(units[name].getMyOwner());
        }

        public void sendMsg(Player _player, String msg)
        {
            Object[] infos = new Object[2];

            infos[1] = msg; //msg
            infos[0] = _player.ConnectUserId; //name
            // infos[2] = _player.id; //id
            sendDataToAll("msg", infos, _player.myCharacter);
        }

        void initializeZone()
        { 
            //load units
            /*foreach (string s in zone.entities.Keys)
            {
                //Create a new Unit using the refrence
                Entity newEntity = zone.entities[s].clone();
                newEntity.myGame = this;
                units.Add(s, newEntity);
                PlayerIO.ErrorLog.WriteError("Unit loaded: "+zone.entities[s].name);
            }

            //load spawnZones
             foreach (string s in zone.spawnZones.Keys)
            {
                zone.spawnZones[s].mainInstance = this;
                spawnZones.Add(s, zone.spawnZones[s]);
                PlayerIO.ErrorLog.WriteError("spawnZone loaded: "+s);
            }
            
            
            //load events

            int k = 0;
            foreach (string s in zone.events.Keys)
            {
                events.Add(k, zone.events[s]);
                PlayerIO.ErrorLog.WriteError("Event loaded: " + zone.events[s].eventName.ToString());
                k++;
            }*/

        }

        public void addUnit(Entity theUnit)
        {
            //PlayerIO.ErrorLog.WriteError("adding unit: " + theUnit.name+" position: "+theUnit.position.toString());
            theUnit.id = "#u" + defaultUnitCounter;
            theUnit.myGame = this;
            //theUnit.setRef();

            units.Add(theUnit.id, theUnit);

            //theUnit.setRef();

            defaultUnitCounter++;

            //sendEntityInfosToAll(theUnit);
        }

        public void destroyUnit(string id)
        {
            try
            {
                units[id].spawnZone.totalAmount--;
            }
            catch (Exception e) { }

            //units[id].clearRef();

            units.Remove(id);

            //Object[] infos = new Object[1];
            //infos[1] = id; //msg

            //sendDataToAll("dest", infos);
        }
    }
}