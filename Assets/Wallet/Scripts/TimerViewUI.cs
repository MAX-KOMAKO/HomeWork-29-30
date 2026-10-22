using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wallet
{
    public class TimerViewUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Image _fillImage;

        private ReactiveTimer _timer;

        public void Initialize(ReactiveTimer timer)
        {
            if (_timeText == null || _fillImage == null)
            {
                Debug.LogError("TimerViewUI: не назначены _timeText или _fillImage.");
                enabled = false;
                return;
            }

            _timer = timer;
            _timer.CurrentTime.Changed += OnTimeChanged;
            UpdateView(_timer.CurrentTime.Value, _timer.TimeLimit);
        }

        private void OnDestroy()
        {
            if (_timer != null)
                _timer.CurrentTime.Changed -= OnTimeChanged;
        }

        private void OnTimeChanged(float oldValue, float newValue)
        {
            UpdateView(newValue, _timer.TimeLimit);
        }

        private void UpdateView(float current, float max)
        {
            _timeText.text = current.ToString("0.0");
            
            if (max > 0)
                _fillImage.fillAmount = current / max;
            else
                _fillImage.fillAmount = 0;
        }
    }
}