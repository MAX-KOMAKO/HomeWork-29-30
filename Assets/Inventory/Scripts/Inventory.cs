using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : IDisposable
    {
        private const string NULL_ITEM_ERROR = "Попытка добавить null вместо предмета.";
        private const string NOT_ENOUGH_SPACE_WARNING = "Недостаточно места в инвентаре.";
        private const string INVALID_INITIAL_SIZE_ERROR = "Начальное содержимое превышает максимальный размер инвентаря.";
        private const string OVERFLOW_ERROR = "Переполнение размера инвентаря.";

        public event Action<Item> ItemAdded;
        public event Action<Item> ItemRemoved;

        private readonly List<Item> _items = new();
        private int _currentSize;

        public int MaxSize { get; }
        public int CurrentSize => _currentSize;
        public IReadOnlyList<Item> Items => _items;

        public Inventory(IEnumerable<Item> initialItems, int maxSize)
        {
            if (maxSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxSize), "Максимальный размер должен быть больше нуля.");

            MaxSize = maxSize;

            if (initialItems != null)
            {
                foreach (Item item in initialItems)
                {
                    if (item == null)
                        throw new ArgumentException(NULL_ITEM_ERROR, nameof(initialItems));

                    if (checked(_currentSize + item.Count) > MaxSize)
                        throw new ArgumentException(INVALID_INITIAL_SIZE_ERROR, nameof(initialItems));

                    _items.Add(item);
                    _currentSize += item.Count;
                }
            }
        }

        public bool TryAdd(Item item)
        {
            if (item == null)
            {
                Debug.LogError(NULL_ITEM_ERROR);
                return false;
            }

            int newSize;
            try
            {
                newSize = checked(_currentSize + item.Count);
            }
            catch (OverflowException)
            {
                Debug.LogError(OVERFLOW_ERROR);
                return false;
            }

            if (newSize > MaxSize)
            {
                Debug.LogWarning(NOT_ENOUGH_SPACE_WARNING);
                return false;
            }

            _items.Add(item);
            _currentSize = newSize;
            ItemAdded?.Invoke(item);
            return true;
        }

        public IReadOnlyList<Item> RemoveItems(string name, int count)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя предмета не может быть пустым.", nameof(name));
                
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Количество должно быть больше нуля.");

            List<Item> result = new List<Item>();
            int remaining = count;

            for (int i = _items.Count - 1; i >= 0 && remaining > 0; i--)
            {
                Item item = _items[i];
                
                if (item.Name != name)
                    continue;

                if (item.Count <= remaining)
                {
                    result.Add(item);
                    remaining -= item.Count;
                    _currentSize -= item.Count;
                    _items.RemoveAt(i);
                    ItemRemoved?.Invoke(item);
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
                    
                    ItemRemoved?.Invoke(takenItem);
                }
            }

            return result;
        }

        public void Dispose()
        {
            ItemAdded = null;
            ItemRemoved = null;
        }
    }
}