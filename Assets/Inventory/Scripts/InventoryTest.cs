using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryTest : MonoBehaviour
    {
        private const string SWORD = "Меч";
        private const string POTION = "Зелье";
        private const string SHIELD = "Щит";
        private const int MAX_SIZE = 10;

        private Inventory _inventory;

        private void Start()
        {
            _inventory = new Inventory(new List<Item>(), MAX_SIZE);

            _inventory.ItemAdded += OnItemAdded;
            _inventory.ItemRemoved += OnItemRemoved;

            Item sword1 = new Item(SWORD, 2);
            Item sword2 = new Item(SWORD, 1);
            Item potion = new Item(POTION, 5);
            Item shield = new Item(SHIELD, 5);

            Debug.Log("--- Добавление предметов ---");
            _inventory.TryAdd(sword1);
            _inventory.TryAdd(sword2);
            _inventory.TryAdd(potion);
            
            Debug.Log($"Текущий размер: {_inventory.CurrentSize} / {_inventory.MaxSize}");
            
            Debug.Log("--- Попытка добавить Щит (не хватит места) ---");
            _inventory.TryAdd(shield); 

            Debug.Log("--- Забор предметов (Меч, 3 шт) ---");
            IReadOnlyList<Item> takenSwords = _inventory.RemoveItems(SWORD, 3);
            
            Debug.Log($"Забрано мечей: {takenSwords.Count} шт. (суммарно)");
            
            Debug.Log($"Текущий размер после забора: {_inventory.CurrentSize} / {_inventory.MaxSize}");
            
            _inventory.Dispose();
        }

        private void OnDestroy()
        {
            if (_inventory != null)
            {
                _inventory.ItemAdded -= OnItemAdded;
                _inventory.ItemRemoved -= OnItemRemoved;
            }
        }

        private void OnItemAdded(Item item)
        {
            Debug.Log($"Событие: Добавлен {item.Name} x{item.Count}");
        }

        private void OnItemRemoved(Item item)
        {
            Debug.Log($"Событие: Изъят {item.Name} x{item.Count}");
        }
    }
}