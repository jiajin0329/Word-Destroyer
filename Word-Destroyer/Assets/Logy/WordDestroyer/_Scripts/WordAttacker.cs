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
        [SerializeField]
        private WordAttackerModel _model = new();

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _model.Initialize(_levelDatas);
        }

        public void Tick()
        {
            _inputString.Clear();
            _inputString.Append(Input.inputString);
            Debug.Log(_inputString);

            _model.Attack(_inputString);
        }
    }
}