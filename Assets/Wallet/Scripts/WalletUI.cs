using System;
using UnityEngine;

namespace Wallet
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private CurrencyViewUI _currencyViewPrefab;
        [SerializeField] private Transform _container;

        [SerializeField] private Sprite _coinsIcon;
        [SerializeField] private Sprite _gemsIcon;
        [SerializeField] private Sprite _energyIcon;

        private WalletService _walletService;

        public void Initialize(WalletService walletService)
        {
            if (walletService == null)
            {
                Debug.LogError("WalletUI: передан null вместо WalletService.");
                enabled = false;
                return;
            }

            _walletService = walletService;
        }

        private void Start()
        {
            if (_walletService == null)
            {
                Debug.LogError("WalletUI: не передан WalletService.");
                enabled = false;
                return;
            }

            if (_currencyViewPrefab == null || _container == null)
            {
                Debug.LogError("WalletUI: не назначены _currencyViewPrefab или _container.");
                enabled = false;
                return;
            }

            CreateCurrencyViews();
        }

        private void CreateCurrencyViews()
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                IReadOnlyVariable<int> currency = _walletService.GetCurrency(type);
                if (currency == null)
                    continue;

                CurrencyViewUI view = Instantiate(_currencyViewPrefab, _container);
                view.Initialize(GetIcon(type), currency);
            }
        }

        private Sprite GetIcon(CurrencyType type)
        {
            switch (type)
            {
                case CurrencyType.Coins:
                    return _coinsIcon;
                case CurrencyType.Gems:
                    return _gemsIcon;
                case CurrencyType.Energy:
                    return _energyIcon;
                default:
                    Debug.LogError($"WalletUI: неизвестный тип валюты {type}.");
                    return null;
            }
        }
    }
}