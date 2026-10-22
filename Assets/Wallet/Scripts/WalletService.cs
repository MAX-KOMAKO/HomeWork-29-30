using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wallet
{
    public class WalletService
    {
        private const int DEFAULT_COINS = 100;
        private const int DEFAULT_GEMS = 100;
        private const int DEFAULT_ENERGY = 100;

        private readonly Dictionary<CurrencyType, ReactiveVariable<int>> _currencies = new();

        public WalletService()
        {
            _currencies[CurrencyType.Coins] = new ReactiveVariable<int>(DEFAULT_COINS);
            _currencies[CurrencyType.Gems] = new ReactiveVariable<int>(DEFAULT_GEMS);
            _currencies[CurrencyType.Energy] = new ReactiveVariable<int>(DEFAULT_ENERGY);
        }

        public IReadOnlyVariable<int> GetCurrency(CurrencyType type)
        {
            if (_currencies.TryGetValue(type, out ReactiveVariable<int> variable) == false)
            {
                Debug.LogError($"Валюта {type} отсутствует в кошельке.");
                return null;
            }

            return variable;
        }

        public int GetBalance(CurrencyType type)
        {
            if (_currencies.TryGetValue(type, out ReactiveVariable<int> variable) == false)
            {
                Debug.LogError($"Валюта {type} отсутствует в кошельке.");
                return 0;
            }

            return variable.Value;
        }

        public void Add(CurrencyType type, int amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Некорректная сумма пополнения: {amount}. Сумма должна быть больше нуля.");
                return;
            }

            if (_currencies.TryGetValue(type, out ReactiveVariable<int> variable) == false)
            {
                Debug.LogError($"Валюта {type} отсутствует в кошельке.");
                return;
            }

            int newBalance;
            try
            {
                newBalance = checked(variable.Value + amount);
            }
            catch (OverflowException)
            {
                Debug.LogError($"Переполнение баланса валюты {type}.");
                return;
            }

            variable.Value = newBalance;
        }

        public void Subtract(CurrencyType type, int amount)
        {
            if (amount <= 0)
            {
                Debug.LogError($"Некорректная сумма списания: {amount}. Сумма должна быть больше нуля.");
                return;
            }

            if (_currencies.TryGetValue(type, out ReactiveVariable<int> variable) == false)
            {
                Debug.LogError($"Валюта {type} отсутствует в кошельке.");
                return;
            }

            if (variable.Value < amount)
            {
                Debug.LogWarning($"Не хватает средств. Валюта: {type}, требуется: {amount}, доступно: {variable.Value}.");
                return;
            }

            variable.Value -= amount;
        }
    }
}