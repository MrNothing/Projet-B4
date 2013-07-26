using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class Effect 
    {
        public float value = 0;
        public EffectNames effect;
        public Effect(EffectNames _effect, float _value)
        {
            effect = _effect;
            value = _value;
        }
    }

    public enum EffectNames
    {
        souBon,
        staBon,
        intBon, 
        strBon,
        agiBon,

        hpBon,
        mpBon,
        hpRegenBon,
        mpRegenBon,
        armorBon,
        resBon,
        dmg,
        crit,
        spellcrit,
        critBon,
        spellcritBon,
        spell_dmg,

        dmg_livings,
        dmg_undeads,
        dmg_monsters,
        dmg_humanoids,
        dmg_humans,
        dmg_spirits,
        dmg_ogres,
        dmg_dragons,

        fireRes,
        arcaneRes,
        iceRes,
        natureRes,
        shadowRes,

        fireBon,
        arcaneBon,
        iceBon,
        natureBon,
        shadowBon,
        chaosBon,

        drainHp,
        drainMp,
        ignoreArmor,
        ignoreRes,
        slow,
        stun,
        poison1,
        poison2,
        poison3,
        spellVamp,
        manaVamp,
        spikes,
        resilience,

        attackSpeed,

        range,
    }

    public enum EffectQuotas //1 level == 1000 quotas
    {
        souBon = 200,
        staBon = 200,
        intBon = 200,
        strBon = 200,
        agiBon = 200,

        hpBon = 10,
        mpBon = 6,
        hpRegenBon = 100,
        mpRegenBon = 60,
        armorBon = 30,
        resBon = 45,
        dmg = 80,
        crit = 800,
        spellcrit = 1000,
        critBon = 100,
        spellcritBon = 150,
        spell_dmg = 40,

        dmg_livings = 40,
        dmg_undeads = 20,
        dmg_monsters = 30,
        dmg_humanoids = 50,
        dmg_humans = 40,
        dmg_spirits = 20,
        dmg_ogres = 20,
        dmg_dragons = 20,

        fireRes = 20,
        arcaneRes = 20,
        iceRes = 20,
        natureRes = 20,
        shadowRes = 20,

        fireBon = 15,
        arcaneBon = 15,
        iceBon = 15,
        natureBon = 15,
        shadowBon = 15,
        chaosBon = 30,

        drainHp = 300,
        drainMp = 300,
        ignoreArmor = 60,
        ignoreRes = 45,
        slow = 350,
        stun = 600,
        poison1 = 100,
        poison2 = 200,
        poison3 = 300,
        spellVamp = 300,
        manaVamp = 300,
        spikes = 600,
        resilience = 80,

        attackSpeed = 250,

        range = 0,
    }

    public enum ItemTypes
    {
        simple, equipement,
    }

    public enum ItemAdvancedTypes
    {
        none, usable, usableAndDestroyAfterTimes, book, 
    }

    public enum SlotTypes
    {
        head, shoulders, neck, chest, hands, wrists, waist, legs, feets, fingers, trinket, leftHand, rightHand, bothHands, back
    }

    public class ItemPattern
    {
        public String name;
        public String icon="default";
        public String description="";
        public String material="default";
        public String model="default";
        public bool canBeTraded = true;
        public bool isQuestItem = false;
        public int price;
        public float weight = 1;
        public int minLevel=0;

        public String specialString="";

        public float coolDown=0; //if it can be used
        public float charges = 0; //if >0 it disappears when all charges are used.

        public String spell = ""; //the spell to cast on use...
        public int spellRank = 0;

        public ItemTypes type=ItemTypes.simple;
        public ItemAdvancedTypes advancedType = ItemAdvancedTypes.none;
        public SlotTypes slot = SlotTypes.head;
        public Effect[] effects = new Effect[5];

        public ItemPattern(String _name)
        {
            name = _name;
        }

        public void setAllEffects(Entity target)
        {
            foreach (Effect e in effects)
            {
                try
                {
                    target.setEffet(e.effect, e.value);
                }
                catch (Exception s)
                { 
                }
            }

            target.applyAllBaseStatsToVitalInfos();
        }

        public void clearAllEffects(Entity target)
        {
            foreach (Effect e in effects)
            {
                try
                {
                    target.setEffet(e.effect, -e.value);
                }
                catch (Exception s)
                {
                }
            }
            target.applyAllBaseStatsToVitalInfos();
        }

        public float getBruteQuota()
        {
            float total = 0;
            foreach (Effect e in effects)
            {
                try
                {

                    total += e.value * ((float)(EffectQuotas)Enum.Parse(typeof(EffectQuotas), e.effect.ToString()));
                }
                catch (Exception s)
                {
                }
            }

            return total;
        }

        public string getEffectDescription(EffectNames effect, float value)
        {
            String des = "";

            if (effect == EffectNames.souBon)
            {
                des += "\n +" + value + " Spirit";
            }
            if (effect == EffectNames.staBon)
            {
                des += "\n +" + value + " Endurance";
            }
            if (effect == EffectNames.intBon)
            {
                des += "\n +" + value + " Intelligence";
            }
            if (effect == EffectNames.strBon)
            {
                des += "\n +" + value + " Strength";
            }
            if (effect == EffectNames.agiBon)
            {
                des += "\n +" + value + " Agility";
            }

            if (effect == EffectNames.hpBon)
            {
                des += "\n +" + value + " Health";
            }

            if (effect == EffectNames.mpBon)
            {
                des += "\n +" + value + " Mana";
            }

            if (effect == EffectNames.hpRegenBon)
            {
                des += "\n +" + value + " Health regenerated every seconds";
            }

            if (effect == EffectNames.mpRegenBon)
            {
                des += "\n +" + value + " Mana regenerated every seconds";
            }
            if (effect == EffectNames.armorBon)
            {
                des += "\n +" + value + " Armor";
            }
            if (effect == EffectNames.resBon)
            {
                des += "\n +" + value + " Resistance";
            }
            if (effect == EffectNames.dmg)
            {
                des += "\n +" + value + " to Physical Damages";
            }
            if (effect == EffectNames.crit)
            {
                des += "\n +" + value + "% chances to preform a critical Strike";
            }
            if (effect == EffectNames.spellcrit)
            {
                des += "\n +" + value + "% chances to perform a critical strike with your spells";
            }
            if (effect == EffectNames.critBon)
            {
                des += "\n +" + value + "% critical Damage";
            }
            if (effect == EffectNames.spellcritBon)
            {
                des += "\n +" + value + "% critical Damage with spells";
            }
            if (effect == EffectNames.spell_dmg)
            {
                des += "\n +" + value + " to Magical Effects";
            }

            if (effect == EffectNames.dmg_livings)
            {
                des += "\n +" + value + " physical damages to Living entities";
            }
            if (effect == EffectNames.dmg_undeads)
            {
                des += "\n +" + value + " physical damages to Undead entities";
            }
            if (effect == EffectNames.dmg_monsters)
            {
                des += "\n +" + value + " physical damages to monsters";
            }
            if (effect == EffectNames.dmg_humanoids)
            {
                des += "\n +" + value + " physical damages to humanoids";
            }
            if (effect == EffectNames.dmg_humans)
            {
                des += "\n +" + value + " physical damages to humans";
            }
            if (effect == EffectNames.dmg_spirits)
            {
                des += "\n +" + value + " physical damages to spirits";
            }
            if (effect == EffectNames.dmg_ogres)
            {
                des += "\n +" + value + " physical damages to ogres";
            }
            if (effect == EffectNames.dmg_dragons)
            {
                des += "\n +" + value + " physical damages to dragons";
            }

            if (effect == EffectNames.attackSpeed)
            {
                des += "\n +" + value + " to attack speed score";
            }

            if (effect == EffectNames.fireRes)
            {
                des += "\n +" + value + " resistance to Fire";
            }
            if (effect == EffectNames.arcaneRes)
            {
                des += "\n +" + value + " resistance to Arcane";
            }
            if (effect == EffectNames.iceRes)
            {
                des += "\n +" + value + " resistance to Ice";
            }
            if (effect == EffectNames.natureRes)
            {
                des += "\n +" + value + " resistance to Nature";
            }
            if (effect == EffectNames.shadowRes)
            {
                des += "\n +" + value + " resistance to Shadow";
            }

            if (effect == EffectNames.fireBon)
            {
                des += "\n +" + value + " to Fire effects";
            }
            if (effect == EffectNames.arcaneBon)
            {
                des += "\n +" + value + " to Arcane effects";
            }
            if (effect == EffectNames.iceBon)
            {
                des += "\n +" + value + " to Ice effects";
            }
            if (effect == EffectNames.natureBon)
            {
                des += "\n +" + value + " to Nature effects";
            }
            if (effect == EffectNames.shadowBon)
            {
                des += "\n +" + value + " to Shadow effects";
            }

            if (effect == EffectNames.chaosBon)
            {
                des += "\n +" + value + " to Chaos effects";
            }

            if (effect == EffectNames.drainHp)
            {
                des += "\n drains " + value + " health per melee attack";
            }

            if (effect == EffectNames.drainMp)
            {
                des += "\n drains " + value + " mana per melee attack";
            }

            if (effect == EffectNames.ignoreArmor)
            {
                des += "\n Ignores " + value + " Armor";
            }

            if (effect == EffectNames.ignoreRes)
            {
                des += "\n Ignores " + value + " resistance";
            }

            if (effect == EffectNames.slow)
            {
                des += "\n Slows the target by " + value + "% for 1 second";
            }

            if (effect == EffectNames.stun)
            {
                des += "\n " + value + "% chances to Stun the target for 0.5 seconds on melee attacks";
            }

            //if (effect.Equals("stun2"))
            //    infos.specialEffects.stun2 += amount;

            if (effect == EffectNames.poison1)
            {
                des += "\n Poisons the target. Deals " + value + " damages every second for 1 second";
            }

            if (effect == EffectNames.poison2)
            {
                des += "\n Poisons the target. Deals " + value + " damages every second for 2 second";
            }

            if (effect == EffectNames.poison3)
            {
                des += "\n Poisons the target. Deals " + value + " damages every second for 3 second";
            }

            if (effect == EffectNames.spellVamp)
            {
                des += "\n You regenerate " + value + " health when you deal magic damages";
            }

            if (effect == EffectNames.manaVamp)
            {
                des += "\n You regenerate " + value + " mana when you deal magic damages";
            }

            if (effect == EffectNames.spikes)
            {
                des += "\n Reflects " + value + " damages when you take physical hits";
            }

            if (effect == EffectNames.resilience)
            {
                des += "\n +" + value + " resilience. Resilience reduces the ennemy's chances to perform a critical strike on you.";
            }

            return des;
        }
    }
}
