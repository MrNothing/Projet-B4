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
            seeds = new ItemSeed[10];

            //Potions seed...
            String[] potionIcons = { "potion1", "potion2", "potion3" };
            seeds[0] = new ItemSeed("Potion", ItemTypes.simple, ItemAdvancedTypes.usableAndDestroyAfterTimes, SlotTypes.head, potionIcons, 0.1f);
           
            //Scrolls seed
            String[] ScrollIcons = { "scroll1", "scroll2", "scroll3" };
            seeds[1] = new ItemSeed("Scroll", ItemTypes.simple, ItemAdvancedTypes.usableAndDestroyAfterTimes, SlotTypes.head, ScrollIcons, 0.1f);

            //Wand seed
            String[] WandIcons = { "wand1", "wand2", "wand3" };
            seeds[2] = new ItemSeed("Wand", ItemTypes.simple, ItemAdvancedTypes.usableAndDestroyAfterTimes, SlotTypes.head, WandIcons, 1);

            //1H Swords seed
            String[] SwordIcons = { "sword1", "sword2", "sword3" };
            seeds[3] = new ItemSeed("Sword", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.leftHand, SwordIcons, 1.5f);

            //2H Swords seed
            seeds[4] = new ItemSeed("Sword", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, SwordIcons, 2);

            //1H Axe seed
            String[] AxeIcons = {"axe1", "axe2", "axe3" };
            seeds[5] = new ItemSeed("Axe", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.leftHand, AxeIcons, 1.5f);

            //2H Axe seed 2
            seeds[6] = new ItemSeed("Axe", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, AxeIcons, 2);

            //2H Staff seed 2
            String[] StaffIcons = { "staff1", "staff2", "staff3" };
            seeds[7] = new ItemSeed("Staff", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.bothHands, StaffIcons, 2.5f);

            //1H Dagger seed 1
            String[] DaggerIcons = { "dagger1", "dagger2", "dagger3" };
            seeds[8] = new ItemSeed("Dagger", ItemTypes.equipement, ItemAdvancedTypes.none, SlotTypes.rightHand, DaggerIcons, 1f);
            
            //Head 1
            // -> Helmet
            // -> Hat
            // -> Headband
            // -> Glasses
            // -> Capuche

            //Shoulders 1
            // -> Pauldrons

            //Neck 0.3
            // -> Necklace

            //Body 1.2
            // -> Breastplate
            // -> Shirt
            // -> Coat

            //Waist 0.7
            // -> Belt

            //Wrists 0.3
            // -> Wrists

            //Hands 0.5
            // -> Gloves

            //Legs 1
            // -> Leggings
            // -> Pants

            //Feets 0.5
            // -> Shoes

            //Ring 0.5

            //Back 0.5
            // -> Cape
            
            //Trinket 1
            // -> Orb
            // -> ...

            //Shield 1.5
        }

        public Item generateItem()
        { 
            //should be imported from Lost Legacy
            return null;
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
            //import this from Eternity
            return null;
        }

        public String hashMapToData(Hashtable table)
        {
            //import this from Eternity
            return null;
        }
    }
}