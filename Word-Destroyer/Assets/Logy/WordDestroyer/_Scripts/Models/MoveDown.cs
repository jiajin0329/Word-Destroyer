using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public class WordMoveDown : WordModel
    {
        public WordStat stat { get; private set; }
        public Vector3 position { get; private set; }
        public Queue<string> attackableWord { get; private set; }
        private float _attackPosY;

        /// <summary>
        /// Execute after handle position.
        /// </summary>
        public event UnityAction tickEvent;
        public event UnityAction attackAction;

        public WordMoveDown(WordStat _stat, Vector3 _position, float _attackPosY)
        {
            stat = _stat;
            position = _position;
            this._attackPosY = _attackPosY;
        }

        public void Tick()
        {
            Vector3 _calculatePosition = position;
            _calculatePosition.y -= stat.moveSpeed * Time.deltaTime;
            position = _calculatePosition;

            tickEvent?.Invoke();

            IsAttackPos();
        }

        private void IsAttackPos()
        {
            if (position.y > _attackPosY)
                return;

            position = new Vector3(position.x, _attackPosY, position.z);
            attackAction?.Invoke();
        }
    }
}