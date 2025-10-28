using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public struct LevelStartUI
    {
        [SerializeField]
        private GameObject _ui;
        [SerializeField]
        private TextMeshProUGUI _countDownText;
        [SerializeField]
        private int _waitTime;

        public async UniTask Start(CancellationToken _cancellationToken)
        {
            if(_waitTime < 1)
            {
                _ui.SetActive(false);
                return;
            }

            _ui.SetActive(true);

            int _timer = _waitTime;
            while (_timer > 0)
            {
                _countDownText.text = _timer.ToString();
                await UniTask.Delay(1000, cancellationToken: _cancellationToken);
                _timer--;
            }

            _ui.SetActive(false);
        }

        public static LevelStartUI BuildTestCountDown()
        {
            GameObject gameObject = new();

            LevelStartUI _countDown = new()
            {
                _ui = gameObject,
                _countDownText = gameObject.AddComponent<TextMeshProUGUI>(),
                _waitTime = 0
            };

            return _countDown;
        }
    }
}