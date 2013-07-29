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