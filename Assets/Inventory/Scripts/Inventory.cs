using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private readonly List<Item> _items;

    public int MaxSize { get; }
    public int CurrentSize => _items.Sum(item => item.Count);
    public IReadOnlyList<Item> Items => _items;

    public Inventory(IEnumerable<Item> initialItems, int maxSize)
    {
        if (maxSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxSize), "Максимальный размер должен быть больше нуля.");

        _items = initialItems != null ? new List<Item>(initialItems) : new List<Item>();
        MaxSize = maxSize;

        if (CurrentSize > MaxSize)
            Debug.LogWarning("Начальное содержимое превышает максимальный размер инвентаря.");
    }

    public bool TryAdd(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Попытка добавить null вместо предмета.");
            return false;
        }

        if (CurrentSize + item.Count > MaxSize)
        {
            Debug.LogWarning($"Недостаточно места. Текущий размер: {CurrentSize}, добавляем: {item.Count}, максимум: {MaxSize}.");
            return false;
        }

        _items.Add(item);
        return true;
    }

    public List<Item> GetItemsBy(string name, int count)
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
                result.Add(new Item(item.Name, item.Count));
                remaining -= item.Count;
                _items.RemoveAt(i);
            }
            else
            {
                if (item.TryReduceCount(remaining, out int taken))
                {
                    result.Add(new Item(item.Name, taken));
                    remaining -= taken;
                }
                break;
            }
        }

        if (remaining > 0)
            Debug.LogWarning($"Запрошено {count} предметов '{name}', но найдено только {count - remaining}.");

        return result;
    }
}