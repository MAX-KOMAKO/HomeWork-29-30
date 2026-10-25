using System;

namespace InventorySystem
{
    [Serializable]
    public class Item
    {
        public string Name { get; }
        public int Count { get; }

        public Item(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}