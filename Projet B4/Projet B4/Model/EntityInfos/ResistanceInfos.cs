using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace PhotonB4
{
    public class ResistanceInfos
    {
        public float totalRes = 0;
        public float shadowRes = 0;
        public float fireRes = 0;
        public float iceRes = 0;
        public float natureRes = 0;
        public float arcaneRes = 0;

        public ResistanceInfos()
        { }

        public ResistanceInfos(ResistanceInfos original)
        {
            totalRes = original.totalRes;
            shadowRes = original.shadowRes;
            fireRes = original.fireRes;
            iceRes = original.iceRes;
            natureRes = original.natureRes;
            arcaneRes = original.arcaneRes;
        }

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
