using System;
using System.Collections.Generic;
using PlayerIO.GameLibrary;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class ChatManager
    {
        GameCode mainInstance;

        public ChatManager(GameCode _mainInstance)
        {
            mainInstance = _mainInstance;
        }

        public void handleClientRequest(Player sender, String _cmd, Message message) {
		
		/*if(_cmd.Equals("avgFps"))
		{
			Player myPlayer = sender;
			if(myPlayer!=null)
			{
                Object[] res = new Object[1];
				res[0] = mainInstance.avgFPS; //fps
				sender.Send("avgFps", res);
			}
		}*/
        
        /*if(_cmd.Equals("players"))
        {
            Player myPlayer = sender;
            if(myPlayer!=null)
            {
                sender.Send("msg", "There are " + myPlayer.currentRoom.players.Count+" Player in this room");
            }
        }*/
        
		if(_cmd.Equals("profile"))
		{
                Object[] infos = new Object[1];
				infos[0]= sender.ConnectUserId; //name
				//send("profile", infos, sender);
		}
		
		if(_cmd.Equals("msg"))
		{
			if(!message.GetString(1).Equals(""))
			{
				mainInstance.sendMsg(sender, message.GetString(1)); //msg
			}
		}
		
		/*if(_cmd.Equals("pm"))
		{
			Player myPlayer = sender;
			Player targetPlayer = mainInstance.getPlayerByName(message.GetString(1)); //target player
			
			if(targetPlayer!=null)
			{
				if(!message.GetString(2).Equals("")) //message
				{
                    Object[] res = new Object[2];
					res[0]= message.GetString(2); //msg
					res[1]= sender.ConnectUserId; //name
					targetPlayer.Send("pm", res) ;
				}
			}
			else
			{
				Object[] infos = new Object[1];
				infos[0] = "Player not found: "+message.GetString(1); //infos[0] = msg
				sender.Send("sMsg", infos);
			}
		}
        */
        //TODO HANDLE PM WITH BIGDB!
		
		
		
		
	}
    }
}
