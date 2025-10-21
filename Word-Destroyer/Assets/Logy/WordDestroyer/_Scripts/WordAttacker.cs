using System;
using System.Text;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordAttacker
    {
        private LevelDatas _levelDatas;
        private StringBuilder _inputString = new();

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
        }

        public void Tick()
        {
            _inputString.Clear();
            _inputString.Append(Input.inputString);
            Debug.Log(_inputString);

            Attack(_inputString);
        }
        
        private void Attack(StringBuilder _inputString)
        {
            if (_inputString.Length < 1) return;

            Word _word = _levelDatas.wordGeneratorDatas.RemoveWord(_inputString.ToString().ToUpper());

            if (_word == null) return;
            _levelDatas.wordGeneratorDatas.wordObjectPool.Release(_word);
        }
    }
}