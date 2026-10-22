using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    private void Start()
    {
        Inventory inventory = new Inventory(new List<Item>(), 10);

        Item sword1 = new Item("Меч", 2);
        Item sword2 = new Item("Меч", 1);
        Item potion = new Item("Зелье", 5);
        Item shield = new Item("Щит", 5);

        Debug.Log("--- Добавление предметов ---");
        inventory.TryAdd(sword1);
        inventory.TryAdd(sword2);
        inventory.TryAdd(potion);
        
        Debug.Log($"Текущий размер: {inventory.CurrentSize} / {inventory.MaxSize}");
        PrintInventory(inventory);

        Debug.Log("--- Попытка добавить Щит (не хватит места) ---");
        inventory.TryAdd(shield); 

        Debug.Log("--- Забор предметов (Меч, 3 шт) ---");
        List<Item> takenSwords = inventory.GetItemsBy("Меч", 3);
        
        Debug.Log($"Забрано мечей: {takenSwords.Count} шт. (суммарно)");
        foreach (Item item in takenSwords)
        {
            Debug.Log($"Забран предмет: {item.Name} x{item.Count}");
        }
        
        Debug.Log($"Текущий размер после забора: {inventory.CurrentSize} / {inventory.MaxSize}");
        PrintInventory(inventory);
    }

    private void PrintInventory(Inventory inventory)
    {
        Debug.Log("Содержимое инвентаря:");
        foreach (Item item in inventory.Items)
        {
            Debug.Log($"- {item.Name} x{item.Count}");
        }
    }
}