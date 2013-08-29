using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    public class SpecialEffects
    {
        public float drainHp = 0;
        public float drainMp = 0;
        public float ignoreRes = 0;
        public float ignoreArmor = 0;
        public float slow = 0;
        public float stun1 = 0;
        public float stun2 = 0;
        public float poison1 = 0;
        public float poison2 = 0;
        public float poison3 = 0;
        public float spikes = 0;
        public float spellVamp = 0;
        public float manaVamp = 0;
        public float resilience = 0;

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
