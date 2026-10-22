using System;
using System.Collections;
using UnityEngine;

namespace Wallet
{
    public class ReactiveTimer : IDisposable
    {
        private readonly float _timeLimit;
        private readonly MonoBehaviour _coroutineRunner;

        private readonly ReactiveVariable<float> _currentTime = new();
        private Coroutine _process;
        private bool _isRunning;

        public IReadOnlyVariable<float> CurrentTime => _currentTime;
        public float TimeLimit => _timeLimit;
        public bool IsRunning => _isRunning;

        public ReactiveTimer(float timeLimit, MonoBehaviour coroutineRunner)
        {
            if (coroutineRunner == null)
                throw new ArgumentNullException(nameof(coroutineRunner));

            if (timeLimit <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeLimit), "Время таймера должно быть больше нуля.");

            _timeLimit = timeLimit;
            _coroutineRunner = coroutineRunner;
            _currentTime.Value = _timeLimit;
        }

        public void StartTimer()
        {
            if (_isRunning)
                StopTimer();

            _currentTime.Value = _timeLimit;
            _isRunning = true;
            _process = _coroutineRunner.StartCoroutine(Process());
        }

        public void StopTimer()
        {
            if (_process != null)
            {
                _coroutineRunner.StopCoroutine(_process);
                _process = null;
            }

            _isRunning = false;
        }

        private IEnumerator Process()
        {
            while (_currentTime.Value > 0)
            {
                float newValue = _currentTime.Value - Time.deltaTime;
                if (newValue < 0)
                    newValue = 0;

                _currentTime.Value = newValue;

                yield return null;
            }

            _isRunning = false;
            _process = null;
        }

        public void Dispose()
        {
            StopTimer();
        }
    }
}