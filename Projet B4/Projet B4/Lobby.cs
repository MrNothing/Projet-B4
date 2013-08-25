using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PlayerIO.GameLibrary;

namespace ProjetB4
{
    //The lobby is the character creation interface.
    [RoomType("Lobby")]
    public class Lobby : Game<Player>
    {

        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message)
        {
            if (message.Type.Equals("create"))
            {
                if (player.PlayerObject.GetString("model", "").Equals(""))
                {
                    //create new character...
                    player.PlayerObject.Set("name", message.GetString(0));
                    player.PlayerObject.Set("class", message.GetString(1));

                    player.PlayerObject.Set("model", message.GetString(2));

                    player.PlayerObject.Set("hairs", message.GetString(3));
                    player.PlayerObject.Set("hairsColor", message.GetString(4));

                    player.PlayerObject.Set("eyes", message.GetString(5));
                    player.PlayerObject.Set("eyesColor", message.GetString(6));

                    player.PlayerObject.Set("map", "Map1");

                    player.PlayerObject.Save();
                }
            }

            if (message.Type.Equals("destroy"))
            {
                //one character per account, destroying is not recommended
            }
        }

        // This method is called whenever a player joins the game
        public override void UserJoined(Player player)
        {
            //massage format: Eternity
            if (player.PlayerObject.GetString("model", "").Equals(""))
            {
                //go to character creation interface...
                player.Send("noChar", new Object[0]);
            }
            else
            {
                Object[] data = new Object[2];
                data[0] = player.PlayerObject.GetString("model");
                data[1] = player.PlayerObject.GetString("map");
                player.Send("charInfos", data);
            }
        }
    }
}