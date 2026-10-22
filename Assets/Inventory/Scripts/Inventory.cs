using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IDisposable
{
    private const string NullItemError = "Попытка добавить null вместо предмета.";
    private const string NotEnoughSpaceWarning = "Недостаточно места в инвентаре.";
    private const string InvalidInitialSizeError = "Начальное содержимое превышает максимальный размер инвентаря.";

    public event Action<Item> ItemAdded;
    public event Action<Item> ItemRemoved;

    private readonly List<Item> _items = new();

    public int MaxSize { get; }
    public int CurrentSize => _items.Sum(item => item.Count);
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
                if (CurrentSize + item.Count > MaxSize)
                    throw new ArgumentException(InvalidInitialSizeError, nameof(initialItems));

                _items.Add(item);
            }
        }
    }

    public bool TryAdd(Item item)
    {
        if (item == null)
        {
            Debug.LogError(NullItemError);
            return false;
        }

        if (CurrentSize + item.Count > MaxSize)
        {
            Debug.LogWarning(NotEnoughSpaceWarning);
            return false;
        }

        _items.Add(item);
        ItemAdded?.Invoke(item);
        return true;
    }

    public List<Item> RemoveItems(string name, int count)
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
                _items.RemoveAt(i);
                ItemRemoved?.Invoke(item);
            }
            else
            {
                int takenCount = remaining;
                int leftCount = item.Count - takenCount;

                _items[i] = new Item(item.Name, leftCount);
                result.Add(new Item(item.Name, takenCount));
                remaining = 0;
                
                ItemRemoved?.Invoke(new Item(item.Name, takenCount));
                ItemAdded?.Invoke(_items[i]);
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