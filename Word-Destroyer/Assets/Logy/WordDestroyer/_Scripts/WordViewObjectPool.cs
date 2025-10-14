using System;
using Logy.GeneralCommonV01;
using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordViewObjectPool
    {
        private ObjectPool<WordView> _objectPool;
        public int idleCount => _objectPool.idleCount;
        public int usingCount => _objectPool.usingCount;
        private WordSetting _setting;
        [SerializeField]
        private ushort _startAmount = 10;
        [SerializeField]
        private TextMeshPro _wordClone;
        private bool _isFirstCreate;
        private Transform _objectPoolParent;

        public WordViewObjectPool(WordSetting _setting, ushort _startAmount)
        {
            this._setting = _setting;
            this._startAmount = _startAmount;            
        }
        
        public void Initialize(WordSetting _setting = null)
        {
            if (_setting != null)
                this._setting = _setting;
            _isFirstCreate = _wordClone == null;
            _objectPool = new(CreateEvent, DestoryEvent, _startAmount);
        }

        private WordView CreateEvent()
        {
            Vector3 _wordPosition = new(0f, 20f, 0f);

            GameObject _gameObject;
            TextMeshPro _textMeshPro = null;
            if (_isFirstCreate)
            {
                // Create WordViewObjectPool parent gameObject.
                _objectPoolParent = new GameObject(nameof(WordViewObjectPool)).transform;

                _gameObject = UnityEngine.Object.Instantiate(_setting.wordPrefabs, _objectPoolParent);
                _wordClone = _textMeshPro = _gameObject.GetComponent<TextMeshPro>();
                _isFirstCreate = false;
            }
            else
            {
                _textMeshPro = UnityEngine.Object.Instantiate(_wordClone, _objectPoolParent);
                _gameObject = _textMeshPro.gameObject;
            }

            _gameObject.SetActive(false);

            return new(_gameObject, _textMeshPro);
        }

        private void DestoryEvent(WordView _object)
        {
            _object.Destory();
        }

        public WordView Get()
        {
            return _objectPool.Get();
        }

        public void Release(WordView _object)
        {
            _objectPool.Release(_object);
        }

        public void Destory()
        {
            _objectPool.Destory();
            UnityEngine.Object.Destroy(_objectPoolParent.gameObject);
            _isFirstCreate = true;
        }
    }
}