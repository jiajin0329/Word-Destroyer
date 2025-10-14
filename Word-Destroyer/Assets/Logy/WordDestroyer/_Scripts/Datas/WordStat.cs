using System;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public struct WordStat
    {
        [field: SerializeField]
        public int index { get; private set; }
        [field: SerializeField]
        public float moveSpeed { get; private set; }
        [field: SerializeField]
        public int attack { get; private set; }

        public WordStat(int _index, float _moveSpeed, int _attack)
        {
            index = _index;
            moveSpeed = _moveSpeed;
            attack = _attack;
        }
    }
}