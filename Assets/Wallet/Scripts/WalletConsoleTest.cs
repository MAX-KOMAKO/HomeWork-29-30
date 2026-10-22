using UnityEngine;

namespace Wallet
{
    public class WalletConsoleTest : MonoBehaviour
    {
        private const KeyCode AddCoinsKey = KeyCode.Alpha1;
        private const KeyCode SubtractCoinsKey = KeyCode.Alpha2;
        
        private const KeyCode AddGemsKey = KeyCode.Alpha3;
        private const KeyCode SubtractGemsKey = KeyCode.Alpha4;
        
        private const KeyCode AddEnergyKey = KeyCode.Alpha5;
        private const KeyCode SubtractEnergyKey = KeyCode.Alpha6;

        private const int DefaultCoinsDelta = 50;
        private const int DefaultGemsDelta = 10;
        private const int DefaultEnergyDelta = 20;

        [SerializeField] private WalletInstaller _walletInstaller;

        [SerializeField] private int _coinsDelta = DefaultCoinsDelta;
        [SerializeField] private int _gemsDelta = DefaultGemsDelta;
        [SerializeField] private int _energyDelta = DefaultEnergyDelta;

        private WalletService _walletService;

        private void Start()
        {
            if (_walletInstaller == null)
            {
                Debug.LogError("WalletConsoleTest: не назначен _walletInstaller.");
                enabled = false;
                return;
            }

            _walletService = _walletInstaller.WalletService;

            if (_walletService == null)
            {
                Debug.LogError("WalletConsoleTest: WalletService недоступен.");
                enabled = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(AddCoinsKey))
                AddCurrency(CurrencyType.Coins, _coinsDelta);

            if (Input.GetKeyDown(SubtractCoinsKey))
                SubtractCurrency(CurrencyType.Coins, _coinsDelta);

            if (Input.GetKeyDown(AddGemsKey))
                AddCurrency(CurrencyType.Gems, _gemsDelta);

            if (Input.GetKeyDown(SubtractGemsKey))
                SubtractCurrency(CurrencyType.Gems, _gemsDelta);

            if (Input.GetKeyDown(AddEnergyKey))
                AddCurrency(CurrencyType.Energy, _energyDelta);

            if (Input.GetKeyDown(SubtractEnergyKey))
                SubtractCurrency(CurrencyType.Energy, _energyDelta);
        }

        private void AddCurrency(CurrencyType type, int amount)
        {
            int balanceBefore = _walletService.GetBalance(type);
            _walletService.Add(type, amount);
            int balanceAfter = _walletService.GetBalance(type);

            if (balanceAfter > balanceBefore)
                Debug.Log($"Добавлено {amount} {type}. Баланс: {balanceAfter}");
            else
                Debug.Log($"Пополнение {amount} {type} не выполнено. Баланс: {balanceAfter}");
        }

        private void SubtractCurrency(CurrencyType type, int amount)
        {
            int balanceBefore = _walletService.GetBalance(type);
            _walletService.Subtract(type, amount);
            int balanceAfter = _walletService.GetBalance(type);

            if (balanceAfter < balanceBefore)
                Debug.Log($"Списано {amount} {type}. Баланс: {balanceAfter}");
            else
                Debug.Log($"Списание {amount} {type} не выполнено. Баланс: {balanceAfter}");
        }
    }
}