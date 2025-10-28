using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public class WordAttacker
    {
        private StringBuilder _inputString = new();
        private WordAttackerModel _model;

        public WordAttacker(WordAttackerModel _model)
        {
            this._model = _model;
        }

        public void Initialize(LevelDatas _levelDatas)
        {
            _model.Initialize(_levelDatas);
        }

        public void Reset()
        {
            _model.Reset();
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