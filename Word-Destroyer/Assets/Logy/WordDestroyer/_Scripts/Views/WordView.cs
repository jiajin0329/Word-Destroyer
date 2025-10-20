using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    public class WordView
    {
        private GameObject _gameObject;
        private Transform _transform;
        private TextMeshPro _textMeshPro;

        private Vector3 _previousPosition;

        public WordView(GameObject _gameObject, TextMeshPro _textMeshPro)
        {
            this._gameObject = _gameObject;
            _transform = _gameObject.transform;
            this._textMeshPro = _textMeshPro;
        }

        public void SetGameObjectActive(bool _set) => _gameObject.SetActive(_set);

        public Vector3 GetPosition() => _transform.position;

        public void SetPosition(Vector3 _position)
        {
            if (_position != _previousPosition)
            {
                _transform.position = _position;
                _previousPosition = _position;
            }
        }

        public string GetTextName() => _textMeshPro.text;

        public void SetTextName(string _set) => _textMeshPro.text = _set;

        public void Destory() => Object.Destroy(_gameObject);

        public static WordView BuildTestWordView(string _textName)
        {
            GameObject _gameObject = new GameObject();
            TextMeshPro _textMeshPro = _gameObject.AddComponent<TextMeshPro>();
            WordView _wordView = new(new GameObject(), _textMeshPro);
            _wordView.SetTextName(_textName);
            return _wordView;
        }
    }
}