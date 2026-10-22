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

            if (_currencyViewPrefab == null || _container == null)
            {
                Debug.LogError("WalletUI: не назначены _currencyViewPrefab или _container.");
                enabled = false;
                return;
            }

            if (HasAllIcons() == false)
            {
                enabled = false;
                return;
            }

            _walletService = walletService;
            CreateCurrencyViews();
        }

        private void CreateCurrencyViews()
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                CurrencyViewUI view = Instantiate(_currencyViewPrefab, _container);
                view.Initialize(GetIcon(type), _walletService.GetCurrency(type));
            }
        }

        private bool HasAllIcons()
        {
            if (_coinsIcon == null)
            {
                Debug.LogError("WalletUI: не назначена _coinsIcon.");
                return false;
            }

            if (_gemsIcon == null)
            {
                Debug.LogError("WalletUI: не назначена _gemsIcon.");
                return false;
            }

            if (_energyIcon == null)
            {
                Debug.LogError("WalletUI: не назначена _energyIcon.");
                return false;
            }

            return true;
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
                    throw new ArgumentException($"WalletUI: неизвестный тип валюты {type}.", nameof(type));
            }
        }
    }
}