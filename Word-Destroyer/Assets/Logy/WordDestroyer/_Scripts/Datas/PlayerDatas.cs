using System;
using UnityEngine.Events;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class PlayerDatas
    {
        [SerializeField]
        private int _hp = 100;
        public event UnityAction<int> addHpEvent;
        public event UnityAction<int> loseHpEvent;

        public void Reset()
        {
            _hp = 100;
        }

        public int GetHp() => _hp;

        public void AddHp(int _add)
        {
            _hp += _add;

            if (_hp > 120)
                _hp = 120;
                
            addHpEvent?.Invoke(_hp);
        }

        public void LoseHp(int _lose)
        {
            _hp -= _lose;
            loseHpEvent?.Invoke(_hp);
        }
    }
}
