using UnityEngine;

namespace Wallet
{
    public class WalletConsoleTest : MonoBehaviour
    {
        private const KeyCode ADD_COINS_KEY = KeyCode.Alpha1;
        private const KeyCode SUBTRACT_COINS_KEY = KeyCode.Alpha2;

        private const KeyCode ADD_GEMS_KEY = KeyCode.Alpha3;
        private const KeyCode SUBTRACT_GEMS_KEY = KeyCode.Alpha4;

        private const KeyCode ADD_ENERGY_KEY = KeyCode.Alpha5;
        private const KeyCode SUBTRACT_ENERGY_KEY = KeyCode.Alpha6;

        private const int DEFAULT_COINS_DELTA = 50;
        private const int DEFAULT_GEMS_DELTA = 10;
        private const int DEFAULT_ENERGY_DELTA = 20;

        [SerializeField] private WalletInstaller _walletInstaller;

        [SerializeField] private int _coinsDelta = DEFAULT_COINS_DELTA;
        [SerializeField] private int _gemsDelta = DEFAULT_GEMS_DELTA;
        [SerializeField] private int _energyDelta = DEFAULT_ENERGY_DELTA;

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
            if (Input.GetKeyDown(ADD_COINS_KEY))
                AddCurrency(CurrencyType.Coins, _coinsDelta);

            if (Input.GetKeyDown(SUBTRACT_COINS_KEY))
                SubtractCurrency(CurrencyType.Coins, _coinsDelta);

            if (Input.GetKeyDown(ADD_GEMS_KEY))
                AddCurrency(CurrencyType.Gems, _gemsDelta);

            if (Input.GetKeyDown(SUBTRACT_GEMS_KEY))
                SubtractCurrency(CurrencyType.Gems, _gemsDelta);

            if (Input.GetKeyDown(ADD_ENERGY_KEY))
                AddCurrency(CurrencyType.Energy, _energyDelta);

            if (Input.GetKeyDown(SUBTRACT_ENERGY_KEY))
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