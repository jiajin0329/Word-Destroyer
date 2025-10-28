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
        [field: SerializeField]
        public ushort startAmount { get; private set; } = 10;
        [SerializeField]
        private TextMeshPro _wordPrefabClone;
        private bool _isFirstCreate;
        private Transform _objectPoolParent;

        public WordObjectPool(ushort _startAmount)
        {
            this.startAmount = _startAmount;
        }

        public void Initialize(WordSetting _setting)
        {
            this._setting = _setting;
            _isFirstCreate = _wordPrefabClone == null;
            _objectPool = new(CreateObject, ReleaseObject, DestoryObject, startAmount);
        }
        
        public void ReSet()
        {
            _objectPool.ReleaseAll();
        }

        private Word CreateObject()
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

        private void ReleaseObject(Word _object)
        {
            _object.ClearAttackListener();
            _object.SetGameObjectActive(false);
        }

        private void DestoryObject(Word _object) => _object.Destory();

        public Word Get() => _objectPool.Get();

        public void Release(Word _object) => _objectPool.Release(_object);

        public void ReleaseAll() => _objectPool.ReleaseAll();

        public void Destory()
        {
            _objectPool.Destory();
            UnityEngine.Object.Destroy(_objectPoolParent.gameObject);
            _isFirstCreate = true;
        }
    }
}