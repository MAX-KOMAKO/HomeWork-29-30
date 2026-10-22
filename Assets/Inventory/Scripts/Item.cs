using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string Name { get; private set; }
    public int Count { get; private set; }

    public Item(string name, int count)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Имя предмета не может быть пустым.", nameof(name));
            
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество должно быть больше нуля.");

        Name = name;
        Count = count;
    }

    public bool TryReduceCount(int amount, out int reducedAmount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"Некорректное количество для списания: {amount}. Должно быть больше нуля.");
            reducedAmount = 0;
            return false;
        }

        reducedAmount = Mathf.Min(amount, Count);
        Count -= reducedAmount;
        return true;
    }
}