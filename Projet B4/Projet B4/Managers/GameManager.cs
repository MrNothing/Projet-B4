using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace ProjetB4
{
    public class GameManager
    {
        GameCode mainInstance;
        SpellsManager spellsInstance;
        public GameManager(GameCode _mainInstance, SpellsManager _spellsInstance)
        {
            spellsInstance = _spellsInstance;
            mainInstance = _mainInstance;
        }

        public void handleClientRequest(Player sender,string _cmd, Message message) 
	    {
            try
            {
                if (sender.myCharacter.locked)
                    return;
            }
            catch (Exception e)
            {
                mainInstance.PlayerIO.ErrorLog.WriteError("handleClientRequest Cancelled: " + _cmd + " there was no Hero loaded");
            }


                //generateItem
                if (_cmd.Equals("generateItem"))
                {
                    sender.myCharacter.addItem(mainInstance.itemGenerator.generateItem("", 10, 10));
                }
        
                //getVisibleUnits
                if (_cmd.Equals("getVisibleUnits"))
                {
                    try
                    {
                        mainInstance.units[message.GetString(1)].watcher = sender.myCharacter.id;
                    }
                    catch (Exception e)
                    {
                        sender.Send("err", "Unit not found!");
                    }
                }

                //getVisibleUnits
                if (_cmd.Equals("ul"))
                {
                    Entity myPlayer = sender.myCharacter;

                    List<String> pList = new List<String>();
                    foreach (string s in mainInstance.units.Keys)
                    {
                        if(myPlayer.position.Substract(mainInstance.units[s].position).Magnitude()<mainInstance.baseRefSize)
                            pList.Add(s + "|x" + mainInstance.units[s].position.x + "y" + mainInstance.units[s].position.y + "z" + mainInstance.units[s].position.z);
                    }

                    sender.Send("ul", pList.ToArray());
                }
                
                //lvlUpSpell
                if (_cmd.Equals("lvlUpSpell"))
                {
                    Player myPlayer = sender;

                    myPlayer.myCharacter.levelUpSpell(message.GetString(1));   
                }

                //lvlUpSpell
                if (_cmd.Equals("teleport"))
                {
                    sender.myCharacter.position = new Vector3(message.GetFloat(1), message.GetFloat(2), message.GetFloat(3));
                    sender.myCharacter.locked = true;
                }

                /*if (_cmd.Equals("invoke"))
                {
                    Player myPlayer = sender;

                    if (myPlayer.currentRoom != null)
                    {
                        if (myPlayer.hero != null) //[TODO] should be for GMs only!!!
                        {
                            if (myPlayer.GM)
                            {
                                Unit newUnit = myPlayer.hero.myGame.addUnit(message.GetString(1), myPlayer.team);
                                newUnit.lifeSpan = 100;
                                newUnit.x = myPlayer.hero.x;
                                newUnit.y = myPlayer.hero.y;
                                newUnit.z = myPlayer.hero.z;
                                newUnit.team = myPlayer.team;
                                newUnit.controlledByServer = false;
                                newUnit.master = myPlayer.hero;
                                newUnit.lockPosition();
                            }
                            else
                            {
                                sender.Send("err", "e1");
                            }
                        }
                    }
                }*/

                /*if(_cmd.Equals("atk"))
                {
                    Player myPlayer = sender;
			
                    if(myPlayer.currentRoom!=null)
                    {
                        if(myPlayer.hero!=null)
                        {
                            myPlayer.hero.directAttack(params.getFloat("x"), params.getFloat("y"), params.getFloat("z"));
                        }
                    }
                }*/

                if (_cmd.Equals("addSpell"))
                {
                    //TODO: do this in a way specific to my class or with a spell Master
                    Player myPlayer = sender;

                   
                    if (myPlayer.myCharacter.spells.Count < 10)
                        myPlayer.myCharacter.addSpell(message.GetString(1));
                    else
                        sender.Send("err", "You cannot learn any more spells");
                      
                }

                if (_cmd.Equals("spells"))
                {
                    //TODO: do this in a way specific to my class or with a spell Master
                    sender.myCharacter.sendSpells(sender);
                }

                if (_cmd.Equals("items"))
                {
                    //TODO: do this in a way specific to my class or with a spell Master
                    sender.myCharacter.sendItems(sender);
                }

                /*if(_cmd.Equals("profile"))
                {
                    if(FlashFighters.getPlayerByName(params.getUtfString("name"))!=null)
                    {
                        Player target = FlashFighters.getPlayerByName(params.getUtfString("name"));
				
                        if(target.hero!=null)
                        {
                            ISFSObject infos = new SFSObject();
                            infos.putUtfString("id", target.hero.id);
                            infos.putSFSObject("items", target.hero.equippedItems);
                            infos.putSFSObject("stats", target.hero.infos.toSFSObject());
                            send("profile", infos, sender);
                        }
                        else
                        {
                            ISFSObject infos = new SFSObject();
                            infos.putUtfString("msg", "Player not found: "+params.getUtfString("name"));
                            send("sMsg", infos, sender);
                        }
                    }
                    else
                    {
                        ISFSObject infos = new SFSObject();
                        infos.putUtfString("msg", "Player not found: "+params.getUtfString("name"));
                        send("sMsg", infos, sender);
                    }
                }*/

                if(_cmd.Equals("equipItem"))
                {
                    Player myPlayer = sender;

                    myPlayer.myCharacter.equipItem(message.GetString(1), false);
                      
                }
		
                if(_cmd.Equals("unEquipItem"))
                {
                    Player myPlayer = sender;

                    myPlayer.myCharacter.unEquipItem(message.GetString(1), true);
                }

                if (_cmd.Equals("req"))
                {
                    mainInstance.sendEntityInfos(sender, mainInstance.units[message.GetString(1)]);
                }

                if (_cmd.Equals("shop"))
                {
                    Player myPlayer = sender;
  
                }

                if (_cmd.Equals("useItem"))
                {
                    Player myPlayer = sender;

                    if (((Entity)(mainInstance).units[message.GetString(1)]).getMyOwner().ConnectUserId.Equals(sender.ConnectUserId))
                    {
                        spellsInstance.UseItem(((Entity)(mainInstance).units[message.GetString(1)]), message.GetString(3), message.GetString(2), message.GetFloat(4), message.GetFloat(5), message.GetFloat(6));
                    }
                    else
                    {
                        sender.Send("err", "e1");
                    }
                     
                }

                if (_cmd.Equals("buyItem"))
                {
                    Player myPlayer = sender;

                    //TODO: Check if this item is sold by this npc using message.GetString(3)
                    ((Hero)mainInstance.units[message.GetString(1)]).buyItem(message.GetString(2));
                    
                }

                if (_cmd.Equals("sellItem"))
                {
                    Player myPlayer = sender;
                    //TODO: Check if i am close to a vendor with message.GetString(3)
                    ((Hero)mainInstance.units[message.GetString(1)]).sellItem(message.GetString(2));
                    
                }



                if (_cmd.Equals("cast"))
                {
                    Player myPlayer = sender;

                    if (((Entity)(mainInstance).units[message.GetString(1)]).getMyOwner().ConnectUserId.Equals(sender.ConnectUserId))
                    {
                        spellsInstance.UseSpell(((Entity)(mainInstance).units[message.GetString(1)]), message.GetString(3), message.GetString(2), message.GetFloat(4), message.GetFloat(5), message.GetFloat(6));
                    }
                    else
                    {
                        sender.Send("err", "e1");
                    }
                     
                }

                
                if (_cmd.Equals("p"))
                {
                    Entity myCharacter = sender.myCharacter;

                    if (myCharacter.hp > 0)
                        myCharacter.setPos(message.GetFloat(1), message.GetFloat(2), message.GetFloat(3));

                    myCharacter.sendPos(new Vector3(0, 0, 0));

                    List<String> pList = new List<String>();
                    foreach (string s in mainInstance.units.Keys)
                    {
                        if (myCharacter.position.Substract(mainInstance.units[s].position).Magnitude() < mainInstance.baseRefSize)
                            pList.Add(s + "|x" + mainInstance.units[s].position.x + "y" + mainInstance.units[s].position.y + "z" + mainInstance.units[s].position.z);
                    }

                    sender.Send("ul", pList.ToArray());
                }

                if (_cmd.Equals("lp"))
                {
                    Entity myCharacter = mainInstance.units[message.GetString(8)];

                    if (myCharacter.master.Equals(sender.myCharacter) || myCharacter.Equals(sender.myCharacter))
                    {
                        if (myCharacter.hp > 0)
                            myCharacter.setPos(message.GetFloat(1), message.GetFloat(2), message.GetFloat(3));

                        myCharacter.sendLocalPos(new Vector3(message.GetFloat(4), message.GetFloat(5), message.GetFloat(6)), message.GetString(7));
                    }
                }

                if (_cmd.Equals("trigger"))
                {
                    if (mainInstance.units[message.GetString(1)].getDistance(sender.myCharacter.position)<5)
                        mainInstance.units[message.GetString(1)].myTrigger.activate(sender.myCharacter);
                    else
                        sender.Send("err", "s6");
                }

                if (_cmd.Equals("useUnit"))
                {
                    //mainInstance.units[message.GetString(1)].myTrigger.activate();
                }

                if (_cmd.Equals("money"))
                {
                    sender.myCharacter.sendMoney();
                }  

                if(_cmd.Equals("atk"))
                {
                    if (sender.myCharacter.getDistance(mainInstance.units[message.GetString(1)]) <= sender.myCharacter.infos.range)
                    {
                        if (sender.myCharacter.attackCounter > sender.myCharacter.getAttackSpeed())
                        {
                            sender.myCharacter.attack(message.GetString(1));
                            sender.myCharacter.attackCounter = 0;
                        }
                    }
                    else
                    {
                        sender.Send("err", "s6");
                    }
                }

                if (_cmd.Equals("invoke"))
                {
                    try
                    {
                        WorldInfos tmpInfos = mainInstance.worldInfos;
                        Entity invokedCreature = new Entity(mainInstance, "", "", tmpInfos.getEntityInfosByName(message.GetString(1)), sender.myCharacter.position.Add(new Vector3(1,0,1)));
                        invokedCreature.infos.baseSpeed = 2;
                        invokedCreature.master = sender.myCharacter;
                        mainInstance.addUnit(invokedCreature);
                    }
                    catch(Exception e)
                    {
                        sender.Send("err", "Unit not found!");
                    }
                }

                if (_cmd.Equals("mount"))
                {
                    try
                    {
                        Entity tmpEntity = mainInstance.units[message.GetString(1)];
                        if (tmpEntity.ridable)
                        {
                            if (tmpEntity.master.Equals(sender.myCharacter))
                            {
                                sender.myCharacter.riding = tmpEntity;
                                sender.myCharacter.sendRider();
                            }
                            else
                            {
                                sender.Send("err", "This unit is not serving you!");
                            }
                        }
                        else
                        {
                            sender.Send("err", "You cannot mount this unit!");
                        }
                    }
                    catch (Exception e)
                    {
                        sender.myCharacter.riding = null;
                        sender.myCharacter.sendRider();
                    }
                }
	    }
    }
}
