using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class BaseStatsInfos
    {
        public float str = 0;
        public float intel = 0;
        public float sta = 0;
        public float agi = 0;
        public float sou = 0;

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
