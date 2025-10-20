using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public class WordModel
    {
        public WordStat stat { get; set; }
        public Vector3 position { get; set; }
        private float _attackPosY;

        public UnityAction attackAction;

        public WordModel(Vector3 _position, float _attackPosY)
        {
            position = _position;
            this._attackPosY = _attackPosY;
        }

        public void Tick()
        {
            Vector3 _calculatePosition = position;
            _calculatePosition.y -= stat.moveSpeed * Time.deltaTime;
            position = _calculatePosition;

            IsAttackPos();
        }

        private void IsAttackPos()
        {
            if (position.y > _attackPosY)
                return;

            position = new Vector3(position.x, _attackPosY, position.z);

            attackAction?.Invoke();
        }

        public static WordModel BuildTestWordModel()
        {
            return new(new Vector3(0f, 6f, 0f), -6);
        }
    }
}