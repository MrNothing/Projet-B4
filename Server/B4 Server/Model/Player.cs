using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayerIO.GameLibrary;

namespace ProjetB3
{
    public class Player : BasePlayer
    {
        public String id;
        public float money;

        public bool GM=false;
        //public Hashtable myFriends;

        //public String myGroupId="";

        public Hero myCharacter;
    }
}
