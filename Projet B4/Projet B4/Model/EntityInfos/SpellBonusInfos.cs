using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    public class SpellBonusInfos
    {
        public float totalBon = 0;
        public float shadowBon = 0;
        public float fireBon = 0;
        public float iceBon = 0;
        public float natureBon = 0;
        public float arcaneBon = 0;
        public float chaosBon = 0;

        public SpellBonusInfos()
        { }

        public SpellBonusInfos(SpellBonusInfos original)
        {
            totalBon = original.totalBon;
            shadowBon = original.shadowBon;
            fireBon = original.fireBon;
            iceBon = original.iceBon;
            natureBon = original.natureBon;
            arcaneBon = original.arcaneBon;
            chaosBon = original.chaosBon;
        }

        public float getBonusByName(String bonusName)
        {
            if (bonusName.Equals("total"))
                return totalBon;

            if (bonusName.Equals("shadow"))
                return shadowBon;

            if (bonusName.Equals("fire"))
                return fireBon;

            if (bonusName.Equals("ice"))
                return iceBon;

            if (bonusName.Equals("nature"))
                return natureBon;

            if (bonusName.Equals("arcane"))
                return arcaneBon;

            if (bonusName.Equals("chaos"))
                return chaosBon;

            return 0;
        }

        public void setBonusByName(String bonusName, float value)
        {
            if (bonusName.Equals("total"))
                totalBon += value;

            if (bonusName.Equals("shadow"))
                shadowBon += value;

            if (bonusName.Equals("fire"))
                fireBon += value;

            if (bonusName.Equals("ice"))
                iceBon += value;

            if (bonusName.Equals("nature"))
                natureBon += value;

            if (bonusName.Equals("arcane"))
                arcaneBon += value;

            if (bonusName.Equals("chaos"))
                chaosBon += value;
        }

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
