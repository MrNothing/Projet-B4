using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    public class VitalInfos {
	    public float hp=0;
	    public float mp=0;
	    public float hpRegen=0;
	    public float mpRegen=0;
	    public float dmg=0;
	    public float armor=0;
	    public float attackSpeed=0;
	    public float crit=0;
	    public float spellCrit=0;
	    public float critBon=0;
	    public float spellCritBon=0;
	
	    public float dmgLiving=0;
	    public float dmgUndead=0;
	    public float dmgMonsters=0;
	    public float dmgHumanoids=0;
	    public float dmgHumans=0;
	    public float dmgSpirits=0;
	    public float dmgOgres=0; //equivalent to golems
	    public float dmgDragons=0;

        public VitalInfos()
        { }

        public VitalInfos(VitalInfos original)
        {
            hp = original.hp;
            mp = original.mp;
            hpRegen = original.hpRegen;
            mpRegen = original.mpRegen;
            dmg = original.dmg;
            armor = original.armor;
            attackSpeed = original.attackSpeed;
            crit = original.crit;
            spellCrit = original.spellCrit;
            critBon = original.critBon;
            spellCritBon = original.spellCritBon;

            dmgLiving = original.dmgLiving;
            dmgUndead = original.dmgUndead;
            dmgMonsters = original.dmgMonsters;
            dmgHumanoids = original.dmgHumanoids;
            dmgHumans = original.dmgHumans;
            dmgSpirits = original.dmgSpirits;
            dmgOgres = original.dmgOgres;
            dmgDragons = original.dmgDragons;

        }

        public Hashtable toHashtable()
        {
            Hashtable tmp = new Hashtable();
            return tmp;
        }
    }
}
