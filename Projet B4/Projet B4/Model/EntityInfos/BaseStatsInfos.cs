using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    public class BaseStatsInfos
    {
        public float str = 0;
        public float intel = 0;
        public float sta = 0;
        public float agi = 0;
        public float sou = 0;

        public BaseStatsInfos()
        {}

        public BaseStatsInfos(BaseStatsInfos original)
        {
            str = original.str;
            sta = original.sta;
            agi = original.agi;
            sou = original.sou;
            intel = original.intel;
        }

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
