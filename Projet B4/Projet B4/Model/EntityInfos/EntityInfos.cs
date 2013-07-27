using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class EntityInfos
    {
        public VitalInfos vitalInfos = new VitalInfos();
        //ResistanceInfos res = new ResistanceInfos();
        //Str, intel etc...
        public BaseStatsInfos baseStats = new BaseStatsInfos();

        //Buffs and items
        public VitalInfos vitalInfosBon = new VitalInfos();
        public BaseStatsInfos baseStatsBon = new BaseStatsInfos();
        public ResistanceInfos resBon = new ResistanceInfos();
        public SpellBonusInfos spellBon = new SpellBonusInfos();

        public SpecialEffects specialEffects = new SpecialEffects();

        //appearance for players and npcs
        public String model = "";
        public float range = 4f;
        public float attackMoveSpeed = 0.1f;
        public float baseSpeed = 0.3f;
        public int level=1;

        public EntityInfos()
        {}

        public EntityInfos(String _model)
        {
            model = _model;
        }

        public String toString()
        {
            return "EntityInfos";
        }

        public int toInt()
        {
            float totalXp = baseStats.agi + baseStats.intel + baseStats.sou + baseStats.sta + baseStats.str;
            totalXp += baseStatsBon.agi + baseStatsBon.intel + baseStatsBon.sou + baseStatsBon.sta + baseStatsBon.str;

            return (int)totalXp;
        }
    }
}
