using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    class ItemSeed
    {
        public String name;
        public ItemTypes type;
        ItemAdvancedTypes advancedType = ItemAdvancedTypes.none;
        SlotTypes slot = SlotTypes.head;

        public String[] icons; //= {"apple","bottle"};
        public String[] models = { "1", "2", "3", "4", "5" };
        public float ratio=1;

        public ItemSeed(String _name, ItemTypes _type, ItemAdvancedTypes _advancedType, SlotTypes _slot, String[] _icons, float _ratio)
        {
            name = _typeName;
            type = _type;
            advancedType = _advancedType;
            slot = _slot;
            icons = _icons;
            ratio = _ratio;
        }
    }
}
