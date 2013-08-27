using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class Hero : Entity
    {
        public bool locked = false;
        public int skillPoints=1;
        public EntityInfos perLevelInfos = new EntityInfos();

        public Player myPlayer;
        public Dictionary<String, float> itemsByName = new Dictionary<String, float>();
        public Hashtable itemsCdsByName = new Hashtable();

        public Hashtable spellsByName = new Hashtable();

        public float bagWeight = 0;
        public int bagMaxWeight = 6;

        public int itemsCounter = 0;

        public Dictionary<String, String> equippedItems = new Dictionary<String, String>();

        public Dictionary<String, List<Item>> itemRewardsByEntity = new Dictionary<string, List<Item>>();
        public Dictionary<String, int> goldRewardsByEntity = new Dictionary<string, int>();

        public int minionsSlain=0;
        public int heroKills = 0;
        public int assists = 0;
        public int deaths = 0;
        public int killingSpree = 0;
        //public Party myParty;

        public Hero(GameCode _myGame, String _id, Player _owner, EntityInfos _infos, String _name, Dictionary<String, Hashtable> _spells, Dictionary<String, Item> _items)
            : base(_myGame, _id, _name, _infos, new Vector3(0,0,0))
        {
            perLevelInfos.baseStats.sou = 1;
            perLevelInfos.baseStats.sta = 1;
            perLevelInfos.baseStats.str = 1;
            perLevelInfos.baseStats.intel = 1;
            perLevelInfos.baseStats.agi = 1;

            myController = _owner;
            //spells = _spells;
            bagMaxWeight = 16;
            type = EntityType.player;

            spells = _spells;
            items = _items;

            myPlayer = _owner;
        }

        public void sendStatsToMe()
        {
            sendInfosToMe();
        }

        public void addXp(float _xp)
        {
            xp += (int)_xp;

            if (xp >= level * 120)
            {
                xp = 0;
                level++;

                skillPoints += 1;

                addPerLevelStats();

                sendStatsToMe();

                infos.level = level;
            }

            sendDynamicInfosToAll();

        }

        public int spellsCounter = 0;
        public void addSpell(String spell)
        {
            SpellInfos spellInfos = new SpellInfos();

            if (spellInfos.allSpells[spell] != null)
            {
                  if (spellsByName[spell] == null)
                 {
                    spells.Add(spellsCounter + "", (Hashtable)spellInfos.allSpells[spell]);
                    spellsByName.Add(spell, true);
                    spellsCounter++;
                    myPlayer.Send("err", "new Spell Added: " + ((Hashtable)spellInfos.allSpells[spell])["id"]);
                    sendSpells(myController);
                 }
                  else
                  {
                      myPlayer.Send("err", "s9");
                //send message: wrong target! -> you already know this spell!
               }
            }
            else
            {
                Hashtable infos = new Hashtable();
                myPlayer.Send("err", "s8");
                //send message: wrong target! -> this spell does not exist!
                return;
            }
        }

        public void addPerLevelStats()
        {
            infos.baseStats.sou += perLevelInfos.baseStats.sou;
            infos.baseStats.sta += perLevelInfos.baseStats.sta;
            infos.baseStats.intel += perLevelInfos.baseStats.intel;
            infos.baseStats.str += perLevelInfos.baseStats.str;
            infos.baseStats.agi += perLevelInfos.baseStats.agi;
            infos.vitalInfosBon.hp += perLevelInfos.vitalInfosBon.hp;
            infos.vitalInfosBon.mp += perLevelInfos.vitalInfosBon.mp;
            infos.vitalInfosBon.hpRegen += perLevelInfos.vitalInfosBon.hpRegen;
            infos.vitalInfosBon.mpRegen += perLevelInfos.vitalInfosBon.mpRegen;
            infos.vitalInfosBon.armor += perLevelInfos.vitalInfosBon.armor;
            infos.resBon.totalRes += perLevelInfos.resBon.totalRes;
            infos.vitalInfosBon.dmg += perLevelInfos.vitalInfosBon.dmg;
            infos.vitalInfosBon.crit += perLevelInfos.vitalInfosBon.crit;
            infos.vitalInfosBon.spellCrit += perLevelInfos.vitalInfosBon.spellCrit;
            infos.spellBon.totalBon += perLevelInfos.spellBon.totalBon;

        }

        public void levelUpSpell(String spellId)
        {
            Hashtable mySpell = (Hashtable)spells[spellId];
            int rank = (int)mySpell["rank"];
            int sp_period = (int)mySpell["sp_period"];
            int maxlvl = (int)mySpell["maxlvl"];

            int price = rank * sp_period * 10;

            //lvl.1 1-0>1*0, true
            //lvl.2 2-1>1*1, false
            //lvl.3 3-1>1*1, true
            //lvl.4 4-2>1*2  false

            /*if (price > myPlayer.money)
            {
                ISFSObject infos = new SFSObject();
                infos.Add("msg", "You dont have enough money!");
                myGame.send("err", infos, myPlayer.user);
                //send message: wrong target! -> the target is not in my team
                return;
            }*/

            if ((level - rank) > sp_period * rank && level >= sp_period && rank < maxlvl && skillPoints>0)
            {
                mySpell.Remove("rank");
                mySpell.Add("rank", rank + 1);

                myGame.spellsManager.applyPassiveEffects(this, spellId);

                sendSpellLvlUp(spellId);
            }
            else
            {
                myPlayer.Send("err", "s7");
                //send message: wrong target! -> the target is not in my team
                return;
            }
        }

        public void sendSpellLvlUp(String spellId)
        {
            Object[] data = new Object[2];
            data[0] = id;
            data[1] = spellId;
            getMyOwner().Send("lvlUPSp", data);
        }
    
        public void buyItem(String itemName)
	    {
		    WorldInfos itemsInfos = myGame.worldInfos;
		    
		    try
		    {
                Item item = itemsInfos.getItemByName(itemName);
               
                item.id = itemsCounter+"";
                itemsCounter++;

                Hashtable dependencies = new Hashtable(); //useless in an mmorpg.
                int price = item.infos.price;
			    
			    if(price<=myPlayer.money)
			    {
                    if (bagWeight + Math.Ceiling(item.infos.weight) - equippedItems.Count> bagMaxWeight)
				    {
                        if (itemsByName[item.infos.name] != null)
					    {
                            float amount = (float)itemsByName[item.infos.name];
						    if(amount<1)
						    {
							    //proceed...
						    }
						    else
						    {
                                getMyOwner().Send("err", "i3");
							    return;
						    }
					    }
					    else
					    {
						    getMyOwner().Send("err", "i3");
						    return;
					    }
				    }
				    else
				    {
					    //proceed...
				    }
				    
					    //check dependencies...
					    /*if(checkItemDependencies(dependencies))
					    {
					    }
					    else*/
					    {
                            //getMyOwner().Send("err", "i4");
						    //return;
						
						    //auto buy the missing items if i have enough money...
						
						
						    /*	for(Object o:dependencies.getKeys())
							    {
								    buyItem(o+"");
							    }
						    */
					    }
				        
					    items.Add(itemsCounter+"", item);
                        try
					    {
                            float amount = (float)itemsByName[item.infos.name];

                            amount += item.infos.weight;
                            itemsByName.Remove(item.infos.name);
                            itemsByName.Add(item.infos.name, amount);
					    }
					    catch(Exception e)
					    {
                            itemsByName.Add(item.infos.name, item.infos.weight);
					    }

                        bagWeight += item.infos.weight;
					    myPlayer.money-=price;
					    
					    /*foreach(Object i in effects.Keys)
					    {
                            Hashtable tmpEffect = (Hashtable)effects[i + ""];
						    setEffet(tmpEffect["effect"]+"", (float)tmpEffect["amount"]);
					    }*/

                        //sendDynamicInfosToAll();
					    //sendInfosToMe();
                        sendAddItem(itemName, itemsCounter+"");
					    sendMoney();
					
					    itemsCounter++;
				
			    }
			    else
			    {
				    getMyOwner().Send("err", "i5");
				    return;
			    }
		    }
		    catch(Exception e)
		    {
			    getMyOwner().Send("err", "i6");
			    return;
		    }
	    }

        public void addItem(String itemName)
        {
            WorldInfos itemsInfos = myGame.worldInfos;
		   
            try
            {
                Item item = itemsInfos.getItemByName(itemName);

                item.id = itemsCounter + "";
                itemsCounter++;

                Hashtable dependencies = new Hashtable(); //useless in an mmorpg.
                //int price = 0;

                if (true)
                {
                    if (bagWeight + Math.Ceiling(item.infos.weight) - equippedItems.Count > bagMaxWeight)
                    {
                        try
                        {
                            float amount = (float)itemsByName[item.infos.name];
                            if (amount < 1)
                            {
                                //proceed...
                            }
                            else
                            {
                                getMyOwner().Send("err", "i3");
                                return;
                            }
                        }
                        catch(Exception e)
                        {
                            getMyOwner().Send("err", "i3");
                            return;
                        }
                    }
                    else
                    {
                        //proceed...
                    }

                    //check dependencies...
                    /*if(checkItemDependencies(dependencies))
                    {
                    }
                    else*/
                    {
                        //getMyOwner().Send("err", "i4");
                        //return;

                        //auto buy the missing items if i have enough money...


                        /*	for(Object o:dependencies.getKeys())
                            {
                                buyItem(o+"");
                            }
                        */
                    }

                    items.Add(itemsCounter + "", item);
                    try
                    {
                        float amount = (float)itemsByName[item.infos.name];

                        amount += item.infos.weight;
                        itemsByName.Remove(item.infos.name);
                        itemsByName.Add(item.infos.name, amount);
                    }
                    catch (Exception e)
                    {
                        itemsByName.Add(item.infos.name, item.infos.weight);
                    }

                    bagWeight += item.infos.weight;
                    //myPlayer.money -= price;

                    /*foreach(Object i in effects.Keys)
                    {
                        Hashtable tmpEffect = (Hashtable)effects[i + ""];
                        setEffet(tmpEffect["effect"]+"", (float)tmpEffect["amount"]);
                    }*/

                    //sendDynamicInfosToAll();
                    //sendInfosToMe();
                    sendAddItem(itemName, itemsCounter + "");
                    //sendMoney();

                    itemsCounter++;

                }
                else
                {
                    getMyOwner().Send("err", "i5");
                    return;
                }
            }
            catch (Exception e)
            {
                getMyOwner().Send("err", "i6");
                return;
            }
        }

        public void addItem(Item item)
        {
            int debugCounter = 0;
            try
            {
                item.id = itemsCounter + "";
                itemsCounter++;

                Hashtable dependencies = new Hashtable(); //useless in an mmorpg.
                //int price = 0;

                if (bagWeight + Math.Ceiling(item.infos.weight) - equippedItems.Count > bagMaxWeight)
                {
                    try
                    {
                        float amount = (float)itemsByName[item.infos.name];
                        if (amount < 1)
                        {
                            //proceed...
                        }
                        else
                        {
                            getMyOwner().Send("err", "i3");
                            return;
                        }
                    }
                    catch(Exception e)
                    {
                        getMyOwner().Send("err", "i3");
                        return;
                    }
                }
                else
                {
                    //proceed...
                }

                debugCounter++; //1

                //check dependencies...
                /*if(checkItemDependencies(dependencies))
                {
                }
                else*/
                {
                    //getMyOwner().Send("err", "i4");
                    //return;

                    //auto buy the missing items if i have enough money...


                    /*	for(Object o:dependencies.getKeys())
                        {
                            buyItem(o+"");
                        }
                    */
                }

                items.Add(item.id, item);

                debugCounter++; //2

                try
                {
                    float amount = (float)itemsByName[item.infos.name];

                    amount += item.infos.weight;
                    itemsByName.Remove(item.infos.name);
                    itemsByName.Add(item.infos.name, amount);
                }
                catch (Exception e)
                {
                    itemsByName.Add(item.infos.name, item.infos.weight);
                }

                debugCounter++; //3

                bagWeight += item.infos.weight;
                //myPlayer.money -= price;
                debugCounter++; //4
                /*foreach(Object i in effects.Keys)
                {
                    Hashtable tmpEffect = (Hashtable)effects[i + ""];
                    setEffet(tmpEffect["effect"]+"", (float)tmpEffect["amount"]);
                }*/

                //sendDynamicInfosToAll();
                //sendInfosToMe();
                sendAddItem(item);
                //sendMoney();
                debugCounter++; //5
                itemsCounter++;
            }
            catch (Exception e)
            {
                getMyOwner().Send("err", "i6");
                return;
            }
        }
       
        public void sellItem(String itemId) //items are sold for 70% of their initial value.
	    {
		    if(items[itemId]!=null)
		    {
			    Item item = (Item)items[itemId];
			    int sellingPrice = (int)((item.infos.price)*0.7f);
			    String itemName = item.infos.name;
			
			
			    /*Hashtable effects = (Hashtable)item["effects"];

                foreach (Object i in effects.Keys)
                {
                    Hashtable tmpEffect = (Hashtable)effects[i + ""];
                    setEffet(tmpEffect["effect"] + "", -(float)tmpEffect["amount"]);
                }*/
			
			    items.Remove(itemId);
			    myPlayer.money += sellingPrice;
			    bagWeight -= item.infos.weight;


                if ((float)itemsByName[itemName] > item.infos.weight)
			    {
				    float amount = (float)itemsByName[itemName];
                    amount -= item.infos.weight;
                    itemsByName.Remove(itemName);
				    itemsByName.Add(itemName, amount);
			    }
			    else
			    {
				    itemsByName.Remove(itemName);
			    }

                //sendDynamicInfosToAll();
					    
			    //sendInfosToAll();
                sendRemoveItem(itemId);
			    sendMoney();
		    }
		    else
		    {
			    getMyOwner().Send("err", "i6");
			    return;
		    }
	    }

        
        public void sendInfos()
        {
           /* ISFSObject data = new SFSObject();
            data.Add("id", id);
            data.Add("stats", infos.toSFSObject());
            myGame.send("infos", data, getMyOwner());*/

            //TODO WAIT FOR A GENERIC WAY TO DO THIS!
        }

        

        public void destroyItem(String itemId)
        {
            if (items[itemId] != null)
            {
                Item item = (Item)items[itemId];

                if (item.equipped)
                    return;

                String itemName = item.infos.name;

                /*ISFSObject effects = item.getSFSObject("effects");
			
                for(Object i:effects.getKeys())
                {
                    ISFSObject tmpEffect = effects.getSFSObject(i+"");
                    setEffet(tmpEffect.getUtfString("effect"), -tmpEffect.getFloat("amount"));
                }*/

                items.Remove(itemId);
                bagWeight -= (float) item.infos.weight;


                if ((float)itemsByName[itemName] > (float)item.infos.weight)
                {
                    float amount = (float)itemsByName[itemName];
                    amount -= (float)item.infos.weight;
                    itemsByName.Remove(itemName);
                    itemsByName.Add(itemName, amount);
                }
                else
                {
                    itemsByName.Remove(itemName);
                }

                //sendDynamicInfosToAll();
					    
                //sendInfosToAll();
                sendRemoveItem(itemId);
                //sendMoney();
            }
            else
            {
                getMyOwner().Send("err", "i6");
                return;
            }
        }

        public Player getMyOwner()
        {
            return myController;
        }

        private bool checkItemDependencies(Hashtable dependencies)
	    {
		    bool allowed = true;
		
		    foreach(Object o in dependencies.Keys)
		    {
                try
                {
                    if (itemsByName[o + ""] != null)
                    {
                        allowed = false;
                    }
                }
                catch (Exception e)
                { }
		    }
		
		    return allowed;
	    }

        public void sendAddItem(String itemName, String itemId) 
        {
            Object[] data = new Object[4];
            data[0] = id;
            data[1] = itemName;
            data[2] = itemId;
            data[3] = "null";
            myGame.sendDataToAll("i+", data, this);
        }

        public void sendAddItem(Item item) 
        {
            Object[] data = new Object[4];
            data[0] = id;
            data[1] = item.infos.name;
            data[2] = item.id;

            if (!item.generated)
                data[3] = "null";
            else
            {
               // getMyOwner().Send("sMsg", "exportItem");
                //getMyOwner().Send("sMsg", "toHashTable" + item.infos.toReducedHashtable());
                data[3] = myGame.itemGenerator.exportItemCompact(item);
            }
            getMyOwner().Send("i+", data);
        }

        public void sendRemoveItem(String itemId) 
        {
            Object[] data = new Object[2];
            data[0] = id;
            data[1] = itemId;
            getMyOwner().Send("i-", data);
        }

        public void sendItems(Player requester) 
        {
           Object[] data = new Object[1 + items.Count*6];
            data[0] = id;
            int counter=0;
            foreach (string s in items.Keys)
            {
                data[counter + 1] = ((Item)items[s]).infos.name;
                data[counter + 2] = s;
                data[counter + 3] = ((Item)items[s]).cooldown;
                data[counter + 4] = ((Item)items[s]).uses;
                data[counter + 5] = ((Item)items[s]).equipped;

                if(!((Item)items[s]).generated)
                    data[counter + 6] = "null";
                else
                    data[counter + 6] = myGame.itemGenerator.exportItem((Item)items[s]);

                counter+=6;
            }

            requester.Send("items", data);
        }

        public void sendEquippedItems()
        {
            /*ISFSObject infos = new SFSObject();
            infos.Add("id", id);
            infos.Add("items", equippedItems);
            myGame.send("eqitems", infos, getMyOwner());*/
        }

        public void sendMoney()
        {
            if(getMyOwner()!=null)
                getMyOwner().Send("money", myPlayer.money);
        }

        public void sendMoney(String sender)
        {
            Object[] infos = new Object[2];
            infos[0] = myPlayer.money; //money
            infos[1] = sender; //id
            getMyOwner().Send("money", infos);
        }

        public void sendEquipItem(String itemId, bool isEquipped)
        {
            Object[] infos = new Object[3];
            infos[0] = id; //id
            infos[1] = itemId; //itemId
            infos[2] = isEquipped; //isEquipped
            getMyOwner().Send("eq", infos);
        }

        public void equipItem(String itemId, bool isSlient)
        {
            try
            {
                Item myItem = items[itemId];

                if (myItem.infos.minLevel <= level)
                {

                    if (!myItem.equipped || isSlient)
                    {
                        if (!isSlient)
                        {
                            if (myItem.infos.slot == SlotTypes.bothHands)
                            {
                                unEquipItem(SlotTypes.rightHand + "", false);
                                unEquipItem(SlotTypes.leftHand + "", false);
                            }

                            if (myItem.infos.slot == SlotTypes.rightHand || myItem.infos.slot == SlotTypes.leftHand)
                            {
                                unEquipItem(SlotTypes.bothHands + "", false);
                            }

                            unEquipItem(myItem.infos.slot + "", false);
                        }

                        myItem.equipped = true;
                        equippedItems.Add(myItem.infos.slot + "", myItem.id);
                        myItem.infos.setAllEffects(this);

                        if (!isSlient)
                        {
                            sendEquipItem(myItem.id, true);
                            sendInfosToMe();
                            sendDynamicInfosToAll();
                        }
                    }
                    else
                    {
                        if (!isSlient)
                            getMyOwner().Send("err", "i8"); //You are already equiping this item!
                        return;
                    }
                }
                else
                {
                    if (!isSlient)
                        getMyOwner().Send("err", "You dont have the required level!"); //You are already equiping this item!
                    return;
                }
            }
            catch (Exception e)
            {
                if (!isSlient)
                    getMyOwner().Send("err", "i6"); //item not found!
                return;
            }
        }

        public bool unEquipItem(String slot, bool displayError)
        {
            try
            {
                //if (bagWeight - equippedItems.Count + 1 <= bagMaxWeight || !displayError)
                //{ 
                    String myId = equippedItems[slot];
                    Item myItem = items[myId];
                    myItem.infos.clearAllEffects(this);
                    myItem.equipped = false;
                    equippedItems.Remove(myItem.infos.slot + "");
                    sendEquipItem(myItem.id, false);

                    if (displayError)
                    {
                        sendInfosToMe();
                        sendDynamicInfosToAll();
                    }
                //}
                //else
                //{
                //    getMyOwner().Send("err", "i3");
                //    return;
                //}

                    return true;
            }
            catch(Exception e)
            {
                if (displayError)
                    getMyOwner().Send("err", "i9"); //you have not equiped this item!
                return false;
            }
        }


    }
}
