using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string Name { get; }
    public int Count { get; }

    public Item(string name, int count)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Имя предмета не может быть пустым.", nameof(name));
            
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество должно быть больше нуля.");

        Name = name;
        Count = count;
    }
}