using System;
using Logy.GeneralCommonV01;
using TMPro;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordObjectPool
    {
        private ObjectPool<Word> _objectPool;
        public int idleCount => _objectPool.idleCount;
        public int usingCount => _objectPool.usingCount;
        private WordSetting _setting;
        [SerializeField]
        private ushort _startAmount = 10;
        [SerializeField]
        private TextMeshPro _wordPrefabClone;
        private bool _isFirstCreate;
        private Transform _objectPoolParent;

        public WordObjectPool(ushort _startAmount)
        {
            this._startAmount = _startAmount;
        }
        
        public void Initialize(WordSetting _setting)
        {
            this._setting = _setting;
            _isFirstCreate = _wordPrefabClone == null;
            _objectPool = new(CreateEvent, DestoryEvent, _startAmount);
        }

        private Word CreateEvent()
        {
            // New WordModel
            WordModel _wordModel = new WordModel(new Vector3(0f, 6f, 0f), -5f);

            // New WordView
            GameObject _gameObject;
            TextMeshPro _textMeshPro = null;
            if (_isFirstCreate)
            {
                // Create WordViewObjectPool parent gameObject.
                _objectPoolParent = new GameObject(nameof(WordObjectPool)).transform;

                _gameObject = UnityEngine.Object.Instantiate(_setting.wordPrefab, _objectPoolParent);
                _wordPrefabClone = _textMeshPro = _gameObject.GetComponent<TextMeshPro>();
                _isFirstCreate = false;
            }
            else
            {
                _textMeshPro = UnityEngine.Object.Instantiate(_wordPrefabClone, _objectPoolParent);
                _gameObject = _textMeshPro.gameObject;
            }
            _gameObject.SetActive(false);
            WordView _wordView = new(_gameObject, _textMeshPro);

            WordStat _wordStat = _setting.wordStats[0];
            return new(_wordModel, _wordView);
        }

        private void DestoryEvent(Word _object)
        {
            _object.Destory();
        }

        public Word Get() => _objectPool.Get();

        public void Release(Word _object)
        {
            _object.ClearAttackListener();
            _object.SetGameObjectActive(false);
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