using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace ProjetB4
{
    public class ItemGenerator
    {
        public ItemSeed[] seeds;

        public ItemGenerator()
        { 
            seeds = new ItemSeed[28];

            //note: in the client, the icon has the same name as the model.

            //Potions seed... 
            String[] potionIcons = { "potion1", "potion2", "potion3" };
            seeds[0] = new ItemSeed("Potion", ItemTypes.usable, ItemAdvancedTypes.potion, SlotTypes.head, potionIcons, 0.1f);
           
            //Scrolls seed
            String[] ScrollIcons = { "scroll1", "scroll2", "scroll3" };
            seeds[1] = new ItemSeed("Scroll", ItemTypes.usable, ItemAdvancedTypes.scroll, SlotTypes.head, ScrollIcons, 0.3f);

            //Wand seed
            String[] WandIcons = { "wand1", "wand2", "wand3" };
            seeds[2] = new ItemSeed("Wand", ItemTypes.usable, ItemAdvancedTypes.wand, SlotTypes.head, WandIcons, 0.1f);

            //1H Swords seed
            String[] SwordIcons = { "sword1", "sword2", "sword3" };
            seeds[3] = new ItemSeed("Sword", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.leftHand, SwordIcons, 1.5f);
            seeds[3].defaultEffects = new EffectNames[1];
            seeds[3].defaultEffects[0] = EffectNames.dmg;

            //2H Swords seed
            seeds[4] = new ItemSeed("Sword", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, SwordIcons, 2);
            seeds[4].defaultEffects = new EffectNames[1];
            seeds[4].defaultEffects[0] = EffectNames.dmg;

            //1H Axe seed
            String[] AxeIcons = {"axe1", "axe2", "axe3" };
            seeds[5] = new ItemSeed("Axe", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.leftHand, AxeIcons, 1.5f);
            seeds[5].defaultEffects = new EffectNames[1];
            seeds[5].defaultEffects[0] = EffectNames.dmg;

            //2H Axe seed 2
            seeds[6] = new ItemSeed("Axe", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, AxeIcons, 2);
            seeds[6].defaultEffects = new EffectNames[1];
            seeds[6].defaultEffects[0] = EffectNames.dmg;

            //2H Staff seed 2
            String[] StaffIcons = { "staff1", "staff2", "staff3" };
            seeds[7] = new ItemSeed("Staff", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, StaffIcons, 2.5f);
            seeds[7].defaultEffects = new EffectNames[1];
            seeds[7].defaultEffects[0] = EffectNames.dmg;

            //1H Dagger seed 1
            String[] DaggerIcons = { "dagger1", "dagger2", "dagger3" };
            seeds[8] = new ItemSeed("Dagger", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.rightHand, DaggerIcons, 1f);
            seeds[8].defaultEffects = new EffectNames[1];
            seeds[8].defaultEffects[0] = EffectNames.dmg;

            //Head 1
            // -> Helmet
            String[] HelmetIcons = { "Helmet1", "Helmet2", "Helmet3" };
            seeds[9] = new ItemSeed("Helmet", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.head, HelmetIcons, 1f);
            seeds[9].defaultEffects = new EffectNames[1];
            seeds[9].defaultEffects[0] = EffectNames.armorBon;

            // -> Hat
            String[] HatIcons = { "Hat1", "Hat2", "Hat3" };
            seeds[10] = new ItemSeed("Hat", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.head, HatIcons, 1f);

            // -> Headband
            String[] HeadbandIcons = { "Headband1", "Headband2", "Headband3" };
            seeds[11] = new ItemSeed("Headband", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.head, HeadbandIcons, 0.5f);

            // -> Glasses
            String[] GlassesIcons = { "Glasses1", "Glasses2", "Glasses3" };
            seeds[12] = new ItemSeed("Glasses", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.head, GlassesIcons, 0.5f);

            // -> Hood
            String[] HoodIcons = { "Hood1", "Hood2", "Hood3" };
            seeds[13] = new ItemSeed("Hood", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.head, HoodIcons, 0.7f);

            //Shoulders 1
            // -> Pauldrons
            String[] PauldronsIcons = { "Pauldrons1", "Pauldrons2", "Pauldrons3" };
            seeds[14] = new ItemSeed("Pauldrons", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.shoulders, PauldronsIcons, 1f);
            seeds[14].defaultEffects = new EffectNames[1];
            seeds[14].defaultEffects[0] = EffectNames.armorBon;

            //Neck 0.3
            // -> Necklace
            String[] NecklaceIcons = { "Necklace1", "Necklace2", "Necklace3" };
            seeds[15] = new ItemSeed("Necklace", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.neck, NecklaceIcons, 0.3f);

            //Body 1.2
            // -> Breastplate
            String[] BreastplateIcons = { "Breastplate1", "Breastplate2", "Breastplate3" };
            seeds[16] = new ItemSeed("Breastplate", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.chest, BreastplateIcons, 1.2f);
            seeds[16].defaultEffects = new EffectNames[1];
            seeds[16].defaultEffects[0] = EffectNames.armorBon;

            // -> Shirt
            String[] ShirtIcons = { "Shirt1", "Shirt2", "Shirt3" };
            seeds[17] = new ItemSeed("Necklace", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.chest, ShirtIcons, 0.5f);

            // -> Coat
            String[] CoatIcons = { "Coat1", "Coat2", "Coat3" };
            seeds[18] = new ItemSeed("Coat", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.chest, CoatIcons, 1f);

            //Waist 0.7
            // -> Belt
            String[] BeltIcons = { "Belt1", "Belt2", "Belt3" };
            seeds[19] = new ItemSeed("Coat", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.waist, BeltIcons, 0.7f);
            seeds[19].defaultEffects = new EffectNames[1];
            seeds[19].defaultEffects[0] = EffectNames.armorBon;

            //Wrists 0.3
            // -> Wrists
            String[] WristsIcons = { "Wrists1", "Wrists2", "Wrists3" };
            seeds[20] = new ItemSeed("Wrists", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.wrists, WristsIcons, 0.3f);
            
            //Hands 0.5
            // -> Gloves
            String[] GlovesIcons = { "Gloves1", "Gloves2", "Gloves3" };
            seeds[21] = new ItemSeed("Gloves", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.hands, GlovesIcons, 0.5f);
            
            //Legs 1
            // -> Leggings
            String[] LeggingsIcons = { "Leggings1", "Leggings2", "Leggings3" };
            seeds[22] = new ItemSeed("Leggings", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.legs, LeggingsIcons, 1);
            seeds[22].defaultEffects = new EffectNames[1];
            seeds[22].defaultEffects[0] = EffectNames.armorBon;

            // -> Pants
            String[] PantsIcons = { "Pants1", "Pants2", "Pants3" };
            seeds[23] = new ItemSeed("Pants", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.legs, PantsIcons, 1);
            
            //Feets 0.5
            // -> Shoes
            String[] ShoesIcons = { "Shoes1", "Shoes2", "Shoes3" };
            seeds[24] = new ItemSeed("Shoes", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.feets, ShoesIcons, 0.5f);
            seeds[24].defaultEffects = new EffectNames[1];
            seeds[24].defaultEffects[0] = EffectNames.armorBon;

            //Ring 0.5
            String[] RingIcons = { "Ring1", "Ring2", "Ring3" };
            seeds[25] = new ItemSeed("Ring", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.fingers, RingIcons, 0.5f);
           
            //Back 0.5
            // -> Cape
            String[] CapeIcons = { "Cape1", "Cape2", "Cape3" };
            seeds[26] = new ItemSeed("Cape", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.back, CapeIcons, 0.5f);
           
            //Trinket 1
            // -> Orb
            // -> ...

            //Shield 1.5
            String[] ShieldIcons = { "Shield1", "Shield2", "Shield3" };
            seeds[27] = new ItemSeed("Shield", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.leftHand, ShieldIcons, 1.5f);
            seeds[27].defaultEffects = new EffectNames[1];
            seeds[27].defaultEffects[0] = EffectNames.armorBon;

        }

        System.Random random = new System.Random();
        NameGenerator nameGenerator = new NameGenerator();
        public Item generateItem(String id, float rewardRarity, int rewardLevel)
        {
            float rarity = rewardRarity;
            int level = rewardLevel + random.Next(0, 1) - random.Next(0, 1);

            if (level < 1)
                level = 1;

            ItemPattern tmpItem = new ItemPattern();
            ItemSeed randomTypeInfo = seeds[random.Next(0, seeds.Length - 1)];

            tmpItem = generateItemPattern(level, rarity*randomTypeInfo.ratio, tmpItem.type, tmpItem.advancedType, randomTypeInfo.defaultEffects);

            tmpItem.name = nameGenerator.generateItemName(randomTypeInfo.name, (int)rarity);
            tmpItem.type = randomTypeInfo.type;
            tmpItem.advancedType = randomTypeInfo.advancedType;
            tmpItem.slot = randomTypeInfo.slot;
            tmpItem.minLevel = level;

            String model = randomTypeInfo.models[random.Next(0, randomTypeInfo.models.Length - 1)];
            String icon = randomTypeInfo.icons[random.Next(0, randomTypeInfo.icons.Length - 1)];

            tmpItem.icon = icon;
            tmpItem.model = model;

            Item newItem = new Item(tmpItem);
            newItem.uses = tmpItem.charges;
            newItem.generated = true;
            newItem.id = id;
            return newItem;
        }

        public ItemPattern generateItemPattern(int minLevel, float _rarity, ItemTypes type, ItemAdvancedTypes advancedType, EffectNames[] defaultEffects)
        {
            ItemPattern newItem = new ItemPattern();

            float _value = _rarity * minLevel * 10;

            newItem.price = (int)(_rarity) * minLevel;

            float valueLeft = _value;

            int effectsAmount = random.Next(5);
            effectsAmount = effectsAmount - defaultEffects.Length;
            if (effectsAmount <= 0)
                effectsAmount = 1;

            if (effectsAmount > 5)
                effectsAmount = 0;

            if (type == ItemTypes.simple)
                return newItem;

            if (advancedType == ItemAdvancedTypes.none)
            {
                for (int i = 0; i < defaultEffects.Length; i++)
                {
                    if (valueLeft >= 0)
                    {
                        float tmpAmount = ((float)random.NextDouble()) * valueLeft / (defaultEffects.Length - i);

                        if (i == effectsAmount + defaultEffects.Length - 1)
                        {
                            tmpAmount = valueLeft;
                        }



                        newItem.effects[i] = new Effect(defaultEffects[i], (int)(tmpAmount / getEffectValue(defaultEffects[i].ToString())));

                        valueLeft -= tmpAmount;
                    }
                }

                int maxVal = defaultEffects.Length;
                if (maxVal < 0)
                    maxVal = 0;

                for (int i = maxVal; i < effectsAmount; i++)
                {
                    if (valueLeft >= 0)
                    {
                        float tmpAmount = ((float)random.NextDouble()) * valueLeft / (effectsAmount - i);

                        if (i == effectsAmount + maxVal)
                        {
                            tmpAmount = valueLeft;
                        }
                        EffectNames randomEffect = getRandomEffect();
                        newItem.effects[i] = new Effect(randomEffect, (int)(tmpAmount / getEffectValue(randomEffect.ToString())));

                        valueLeft -= tmpAmount;
                    }
                }
            }

            if (advancedType == ItemAdvancedTypes.potion)
            {
                float tmpAmount = valueLeft;

                EffectNames randomEffect = getRandomEffect();
                newItem.effects[0] = new Effect(randomEffect, (int)(tmpAmount / getEffectValue(randomEffect.ToString())));

                valueLeft -= tmpAmount;
            }

            if (advancedType == ItemAdvancedTypes.scroll || advancedType == ItemAdvancedTypes.spellBook)
            {
                float _spellRank = valueLeft;
                newItem.spell = getRandomSpell();
                newItem.spellRank= (int)_spellRank;
                newItem.charges = 1;
            }

            if (advancedType == ItemAdvancedTypes.wand)
            {
                float _spellRank = (float)Math.Ceiling(((float)random.NextDouble()) * valueLeft / 2f);
                newItem.spell = getRandomSpell();
                newItem.spellRank = (int)_spellRank;
                newItem.charges = valueLeft - _spellRank;
            }

            return newItem;
        }

        public EffectNames getRandomEffect()
        {
            Array values = EffectNames.GetValues(typeof(EffectNames));
            return (EffectNames)values.GetValue(random.Next(values.Length));
        }

        public float getEffectValue(string effect)
        {
            float effectPrice = ((float)(EffectQuotas)Enum.Parse(typeof(EffectQuotas), effect));
            return effectPrice;
        }

        public String getRandomSpell()
        {
            return "";
        }

        public Item parseItem(String rawData)
        {
            Hashtable itemInfos = dataToHashMap(rawData);

            ItemPattern newPattern = new ItemPattern(itemInfos);
            Item newItem = new Item(newPattern);
            newItem.generated = true;

            return newItem;
        }

        public String exportItem(Item myItem)
        {
           return hashMapToData(myItem.infos.toHashtable());
        }

        public Hashtable dataToHashMap(String data)
        {
            /*
             String param_Name = data.Substring(data.IndexOf("{")+1, data.IndexOf("=")-(data.IndexOf("{")+1));
               String param_Data = data.Substring(data.IndexOf("=")+1, data.IndexOf("}")-(data.IndexOf("=")+1));
				
		
            UnityEngine.Debug.Log("param_Name: "+param_Name);
            UnityEngine.Debug.Log("param_Data: "+param_Data);
            return null;
		
            */
            int solveDoublesParams = 0; //in case there is many params with the same name.

            Hashtable params1 = new Hashtable();
            while (data.IndexOf("{") != -1 && data.IndexOf("=") != -1 && data.IndexOf("}") != -1)
            {
                try
                {
                    String param_Name = data.Substring(data.IndexOf("{") + 1, data.IndexOf("=") - (data.IndexOf("{") + 1));
                    String param_Data = data.Substring(data.IndexOf("=") + 1, data.IndexOf("}") - (data.IndexOf("=") + 1));

                    if (param_Name.Equals("sub"))
                    {
                        Hashtable subHashMap;

                        String firstString = "{sub=" + param_Data + "}";
                        String lastString = "{endsub=" + param_Data + "}";

                        String sub_raw_data = data.Substring(data.IndexOf(firstString) + firstString.Length, data.IndexOf(lastString) - (data.IndexOf(firstString) + firstString.Length));
                        data = data.Replace("{sub=" + param_Data + "}" + sub_raw_data + "{endsub=" + param_Data + "}", "");
                        subHashMap = (Hashtable)dataToHashMap(sub_raw_data);

                        params1.Add(param_Data, subHashMap);
                    }
                    else
                    {
                        data = data.Replace("{" + param_Name + "=" + param_Data + "}", "");

                        String dataType = "";

                        if (param_Name.IndexOf("~") != -1)
                        {
                            dataType = param_Name.Substring(param_Name.IndexOf("~") + 1, param_Name.Length - (param_Name.IndexOf("~") + 1));
                            param_Name = param_Name.Replace("~" + dataType, "");
                        }

                        param_Name = param_Name.Replace("*ti*", "~");
                        param_Data = param_Data.Replace("~eq~", "=");
                        param_Data = param_Data.Replace("~a+~", "{");
                        param_Data = param_Data.Replace("~a-~", "}");

                        System.Object finalData = param_Data;
                        if (dataType.Equals("i"))
                        {
                            finalData = int.Parse(param_Data);
                        }

                        if (dataType.Equals("n"))
                        {
                            finalData = float.Parse(param_Data);
                        }

                        if (dataType.Equals("b"))
                        {
                            finalData = bool.Parse(param_Data);
                        }

                        if (params1[param_Name] == null)
                        {
                            //...
                        }
                        else
                        {
                            solveDoublesParams++;
                            param_Name = param_Name + solveDoublesParams;
                        }

                        params1.Add(param_Name, finalData);
                    }
                }
                catch (Exception e)
                {
                    //System.out.println("Error while parsing data: "+e);
                }
            }

            return params1;
        }
        /**
         *  Converts the HashMap into data.
         *
         *  HashMap Structure: <String param1,String value>,<String param2,String value>
         *
         *  data Structure: "{param1=value}{param2=value}..."
         */
        public String hashMapToData(Hashtable params1)
        {

            String data = "";

            foreach (System.Object o in params1.Keys)
            {
                String param = o + "";
                if (params1[param] == null)
                {
                    data += "{" + param + "=null}";
                }
                else
                {
                    if (params1[param].GetType().Equals(typeof(Hashtable)))
                    {
                        data += "{sub=" + param + "}";
                        data += hashMapToData((Hashtable)params1[param]);
                        data += "{endsub=" + param + "}";
                    }
                    else
                    {
                        param = param.Replace("~", "*ti*");

                        bool known = false;
                        System.Object paramValue = params1[param];
                        if (params1[param].GetType().Equals(typeof(byte)) || params1[param].GetType().Equals(typeof(short)) || params1[param].GetType().Equals(typeof(int)) || params1[param].GetType().Equals(typeof(long)))
                        {
                            known = true;
                            param = param + "~i";
                        }
                        else
                        {
                            if (params1[param].GetType().Equals(typeof(float)) || params1[param].GetType().Equals(typeof(double)))
                            {
                                known = true;
                                param = param + "~n";
                            }
                            else
                            {
                                if (params1[param].GetType().Equals(typeof(bool)))
                                {
                                    known = true;
                                    param = param + "~b";
                                }
                            }
                        }

                        String controlledValue = paramValue + "";
                        controlledValue = controlledValue.Replace("=", "~eq~");
                        controlledValue = controlledValue.Replace("{", "~a+~");
                        controlledValue = controlledValue.Replace("}", "~a-~");

                        data += "{" + param + "=" + controlledValue + "}";
                    }
                }
            }

            //if(Eternity.debug)
            //System.out.println("[SENDING] "+data);

            return data;
        }
    }
}