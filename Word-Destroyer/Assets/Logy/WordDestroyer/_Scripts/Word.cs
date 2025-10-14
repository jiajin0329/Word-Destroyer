using System;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class Word
    {
        private WordModel _model;
        private WordView _view;
        public WordStat stat { get; private set; }

        public Word(WordModel _model, WordStat _stat, WordView _view)
        {
            this._model = _model;
            stat = _stat;
            this._view = _view;
        }

        public void Tick()
        {
            _model.Tick();
            _view.SetPosition(_model.position);
        }

        public Vector3 GetViewPosition()
        {
            return _view.GetPosition();
        }

        public string GetViewTextName()
        {
            return _view.GetTextName();
        }
    }
}