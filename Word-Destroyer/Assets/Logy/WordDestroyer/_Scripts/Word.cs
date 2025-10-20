using System;
using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class Word
    {
        private WordModel _model;
        private WordView _view;

        public Word(WordModel _model, WordView _view)
        {
            this._model = _model;
            this._view = _view;
        }

        public WordStat GetStat() => _model.stat;

        public void SetStat(WordStat _set) => _model.stat = _set;

        public void SetPosition(Vector3 _set)
        {
            _model.position = _set;
            _view.SetPosition(_set);
        }

        public Vector3 GetViewPosition() => _view.GetPosition();

        public string GetViewTextName() => _view.GetTextName();

        public void SetTextName(string _set) => _view.SetTextName(_set);

        public void SetGameObjectActive(bool _set) => _view.SetGameObjectActive(_set);

        public void AddAttackListener(UnityAction _listener) => _model.attackAction += _listener;
        
        public void ClearAttackListener() => _model.attackAction = null;

        public void Tick()
        {
            _model.Tick();
            _view.SetPosition(_model.position);
        }

        public void Destory() => _view.Destory();

        public static Word BuildTestWord(string _wordName)
        {
            return new(WordModel.BuildTestWordModel(), WordView.BuildTestWordView(_wordName));
        }
    }
}