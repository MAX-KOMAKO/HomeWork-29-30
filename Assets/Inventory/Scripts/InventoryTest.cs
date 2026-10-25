using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryTest : MonoBehaviour
    {
        private void Start()
        {
            Inventory inventory = new Inventory(10);

            Debug.Log("--- Добавление предметов ---");
            
            Item sword1 = new Item("Меч", 2);
            Item sword2 = new Item("Меч", 1);
            Item potion = new Item("Зелье", 5);
            Item shield = new Item("Щит", 5);

            Debug.Log($"Добавлен Меч (2 шт): {inventory.TryAdd(sword1)}");
            Debug.Log($"Добавлен Меч (1 шт): {inventory.TryAdd(sword2)}");
            Debug.Log($"Добавлено Зелье (5 шт): {inventory.TryAdd(potion)}");
            
            Debug.Log($"Текущий размер: {inventory.CurrentSize} / {inventory.MaxSize}");
            
            Debug.Log("--- Попытка добавить Щит (не хватит места) ---");
            Debug.Log($"Добавлен Щит (5 шт): {inventory.TryAdd(shield)}"); 

            Debug.Log("--- Забор предметов (Меч, 3 шт) ---");
            List<Item> takenSwords = inventory.RemoveItems("Меч", 3);
            
            Debug.Log($"Забрано мечей: {takenSwords.Count} объектов (суммарно).");
            
            Debug.Log($"Текущий размер после забора: {inventory.CurrentSize} / {inventory.MaxSize}");
        }
    }
}