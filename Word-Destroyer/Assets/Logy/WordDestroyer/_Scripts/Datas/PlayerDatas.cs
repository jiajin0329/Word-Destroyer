using System;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class PlayerDatas
    {
        [SerializeField]
        private int _currentHp = startHp;
        public const int startHp = 100;
        [SerializeField]
        private TextMeshProUGUI _hpUi;

        private UnityAction<int> _addHpEvent;
        private UnityAction<int> _loseHpEvent;

        public void Initialize()
        {
            _currentHp = startHp;
            SetHpUi(_currentHp);
        }

        public void ReSet()
        {
            _currentHp = startHp;
            _addHpEvent = null;
            _loseHpEvent = null;
            SetHpUi(_currentHp);
        }

        public int GetHp() => _currentHp;

        public void AddHp(int _add)
        {
            _currentHp += _add;

            if (_currentHp > 120)
                _currentHp = 120;

            SetHpUi(_currentHp);

            _addHpEvent?.Invoke(_currentHp);
        }
        
        private void SetHpUi(int _hp)
        {
            if (_hpUi != null)
                _hpUi.text = $"HP : {_hp}";
        }

        public void LoseHp(int _lose)
        {
            _currentHp -= _lose;

            SetHpUi(_currentHp);

            _loseHpEvent?.Invoke(_currentHp);
        }

        public void AddAddHpListener(UnityAction<int> _listener) => _addHpEvent += _listener;

        public void RemoveAddHpListener(UnityAction<int> _listener) => _addHpEvent -= _listener;

        public void AddLoseHpEventListener(UnityAction<int> _listener) => _loseHpEvent += _listener;

        public void RemoveLoseHpEventListener(UnityAction<int> _listener) => _addHpEvent -= _listener;
    }
}
