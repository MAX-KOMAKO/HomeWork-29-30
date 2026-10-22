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
            return GetVariable(type);
        }

        public int GetBalance(CurrencyType type)
        {
            return GetVariable(type).Value;
        }

        public void Add(CurrencyType type, int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма пополнения должна быть больше нуля.");

            ReactiveVariable<int> variable = GetVariable(type);

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
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма списания должна быть больше нуля.");

            ReactiveVariable<int> variable = GetVariable(type);

            if (variable.Value < amount)
            {
                Debug.LogWarning($"Не хватает средств. Валюта: {type}, требуется: {amount}, доступно: {variable.Value}.");
                return;
            }

            variable.Value -= amount;
        }

        private ReactiveVariable<int> GetVariable(CurrencyType type)
        {
            if (_currencies.TryGetValue(type, out ReactiveVariable<int> variable) == false)
                throw new ArgumentException($"Валюта {type} отсутствует в кошельке.", nameof(type));

            return variable;
        }
    }
}