using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory
    {
        private readonly List<Item> _items = new();
        private readonly int _maxSize;
        private int _currentSize;

        public Inventory(int maxSize)
        {
            _maxSize = maxSize;
        }

        public int CurrentSize => _currentSize;
        public int MaxSize => _maxSize;
        
        public IReadOnlyList<Item> Items => _items;

        public bool TryAdd(Item item)
        {
            if (item == null) return false;

            if (_currentSize + item.Count > _maxSize)
            {
                return false;
            }

            _items.Add(item);
            _currentSize += item.Count;
            return true;
        }

        public List<Item> RemoveItems(string name, int count)
        {
            List<Item> result = new List<Item>();
            int remaining = count;

            for (int i = _items.Count - 1; i >= 0 && remaining > 0; i--)
            {
                Item item = _items[i];
                
                if (item.Name != name) continue;

                if (item.Count <= remaining)
                {
                    result.Add(item);
                    remaining -= item.Count;
                    _currentSize -= item.Count;
                    _items.RemoveAt(i);
                }
                else
                {
                    int takenCount = remaining;
                    int leftCount = item.Count - takenCount;

                    Item takenItem = new Item(item.Name, takenCount);
                    Item leftItem = new Item(item.Name, leftCount);

                    _items[i] = leftItem;
                    result.Add(takenItem);
                    _currentSize -= takenCount;
                    remaining = 0;
                }
            }

            return result;
        }
    }
}