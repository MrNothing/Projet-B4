using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class Item
    {
        public String id;

        public float cooldown = 0; //triggered after use
        public float uses = 0; //if the item is usable only a specific amount of times.

        public bool equipped=false;

        public ItemPattern infos;
        public Vector3 position; //position on the Bag;

        public Item(ItemPattern _infos)
        {
            infos = _infos;
        }
    }
}
