using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class delayedMagicDmg
    {
        private Entity parentUnit;
        private String target;
        private float dmg;
        private String dmgType;
        private GameCode mainInstance;
        private string spell;

        public int waves = 1;
        public int period = 0;

        public delayedMagicDmg(Entity _parentUnit, GameCode _mainInstance, String _target, float _dmg, String _dmgType, String _spell)
        {
            mainInstance = _mainInstance;
            dmg = _dmg;
            target = _target;
            parentUnit = _parentUnit;
            dmgType = _dmgType;
            spell = _spell;
        }
        public void run()
        {
            if (waves > 0)
            {
                //System.out.println("Wave "+waves+" fell");

                Entity targetUnit = (Entity)parentUnit.myGame.units[target];

                if (targetUnit.soulShield <= 0)
                {
                    targetUnit.incantation = null;
                    targetUnit.hitMeWithMagic(parentUnit.id, dmg, dmgType);
                }
                else
                {
                    SpellsCaster spellsCaster = new SpellsCaster(mainInstance, targetUnit, spell, 1, parentUnit, dmg);
                    spellsCaster.noIncant = true;
                }

                waves--;

                if (period >= 25)
                    parentUnit.canalisedSpell = mainInstance.ScheduleCallback(run, period);
            }
            else
            {
                parentUnit.canalisedSpell = null;
                //myTimer.cancel();
            }
        }
    }

    public class delayedPhysicDmg
    {
        private Entity parentUnit;
        private String target;
        private float dmg;
        private GameCode mainInstance;
        private string spell;

        public int waves = 1;
        public int period = 0;

        public delayedPhysicDmg(Entity _parentUnit, GameCode _mainInstance, String _target, float _dmg, String _spell)
        {
            mainInstance = _mainInstance;
            dmg = _dmg;
            target = _target;
            parentUnit = _parentUnit;
            spell = _spell;
        }
        public void run()
        {
            if (waves > 0)
            {
                //System.out.println("Wave "+waves+" fell");

                Entity targetUnit = (Entity)parentUnit.myGame.units[target];

                if (targetUnit.soulShield <= 0)
                {
                    targetUnit.incantation = null;
                    bool lastCrit = false;
                    if ((targetUnit.mainSeed).Next(0, 100) < (targetUnit.infos.vitalInfos.crit + targetUnit.infos.vitalInfosBon.crit))
                    {
                        lastCrit = true;
                        dmg *= (2f + (targetUnit.infos.vitalInfos.critBon + targetUnit.infos.vitalInfosBon.critBon) / 100);
                    }

                    targetUnit.hitMeWithPhysic(parentUnit.id, dmg, lastCrit);
                }
                else
                {
                    SpellsCaster spellsCaster = new SpellsCaster(mainInstance, targetUnit, spell, 1, parentUnit, dmg);
                    spellsCaster.noIncant = true;
                }

                waves--;

                if (period >= 25)
                    parentUnit.canalisedSpell = mainInstance.ScheduleCallback(run, period);
            }
            else
            {
                parentUnit.canalisedSpell = null;
                //myTimer.cancel();
            }
        }
    }
}
