using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wallet
{
    public class CurrencyViewUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;

        private IReadOnlyVariable<int> _currency;

        public void Initialize(Sprite icon, IReadOnlyVariable<int> currency)
        {
            if (_icon == null || _amountText == null)
            {
                Debug.LogError("CurrencyViewUI: не назначены _icon или _amountText.");
                enabled = false;
                return;
            }

            _icon.sprite = icon;
            _currency = currency;

            if (_currency != null)
            {
                _currency.Changed += OnCurrencyChanged;
                SetValue(_currency.Value);
            }
        }

        private void OnDestroy()
        {
            if (_currency != null)
                _currency.Changed -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int oldValue, int newValue)
        {
            SetValue(newValue);
        }

        private void SetValue(int amount)
        {
            if (_amountText != null)
                _amountText.text = amount.ToString();
        }
    }
}