using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    private const string SWORD = "Меч";
    private const string POTION = "Зелье";
    private const string SHIELD = "Щит";
    private const int MAX_SIZE = 10;

    private void Start()
    {
        Inventory inventory = new Inventory(new List<Item>(), MAX_SIZE);

        inventory.ItemAdded += item => Debug.Log($"Событие: Добавлен {item.Name} x{item.Count}");
        inventory.ItemRemoved += item => Debug.Log($"Событие: Изъят {item.Name} x{item.Count}");

        Item sword1 = new Item(SWORD, 2);
        Item sword2 = new Item(SWORD, 1);
        Item potion = new Item(POTION, 5);
        Item shield = new Item(SHIELD, 5);

        Debug.Log("--- Добавление предметов ---");
        inventory.TryAdd(sword1);
        inventory.TryAdd(sword2);
        inventory.TryAdd(potion);
        
        Debug.Log($"Текущий размер: {inventory.CurrentSize} / {inventory.MaxSize}");
        
        Debug.Log("--- Попытка добавить Щит (не хватит места) ---");
        inventory.TryAdd(shield); 

        Debug.Log("--- Забор предметов (Меч, 3 шт) ---");
        List<Item> takenSwords = inventory.RemoveItems(SWORD, 3);
        
        Debug.Log($"Забрано мечей: {takenSwords.Count} шт. (суммарно)");
        
        Debug.Log($"Текущий размер после забора: {inventory.CurrentSize} / {inventory.MaxSize}");
        
        inventory.Dispose();
    }
}