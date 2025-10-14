using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordGeneratorModel
    {
        private LevelDatas _levelDatas;
        [SerializeField]
        private WordViewObjectPool _wordViewObjectPool;
        public int currentCount { get; private set; }

        public class GenerateParam
        {
            public Vector3 wordPosition;
            public string wordName;
        }

        public WordGeneratorModel(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _wordViewObjectPool = new(_levelDatas.wordSetting, 10);
        }

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _wordViewObjectPool.Initialize(_levelDatas.wordSetting);
        }

        public async UniTask RepeatGenerate(int _generateAmount, int _generateIntervalMs = 0, CancellationToken _cancellationToken = new())
        {
            currentCount = 0;
            while (currentCount < _generateAmount)
            {
                currentCount++;

                GenerateParam generateParam = new()
                {
                    wordPosition = SelectWordPosition(),
                    wordName = SelectWordName()
                };

                Generate(generateParam);

                if (_generateIntervalMs != 0)
                    await UniTask.Delay(_generateIntervalMs, cancellationToken: _cancellationToken);
                else
                    continue;
            }
        }
        
        private Vector3 SelectWordPosition()
        {
            Vector3 _minPosition = _levelDatas.wordGeneratorSetting.generateMinPosition;
            Vector3 _maxPosition = _levelDatas.wordGeneratorSetting.generateMaxPosition;
            float _positionX = UnityEngine.Random.Range(_minPosition.x, _maxPosition.x);
            float _positionY = UnityEngine.Random.Range(_minPosition.y, _maxPosition.y);
            float _positionZ = UnityEngine.Random.Range(_minPosition.z, _maxPosition.z);

            return new Vector3(_positionX, _positionY, _positionZ);
        }

        private string SelectWordName()
        {
            string[] _words = _levelDatas.wordGeneratorSetting.wordNames;
            int _range = _words.Length;
            int _index = UnityEngine.Random.Range(0, _range);

            return _words[_index];
        }

        /// <summary>
        /// Generate Word and determine key and coordinates, then add to WordGeneratorData's wordDictionary, and finally return Word Class.
        /// </summary>
        public Word Generate(GenerateParam _param)
        {
            WordStat _stat = SelectWordStat();

            WordModel _wordModel = new WordMoveDown(_stat, _param.wordPosition, -6f);

            WordView _wordView = _wordViewObjectPool.Get();
            _wordView.SetTextName(_param.wordName);
            _wordView.SetPosition(_param.wordPosition);
            _wordView.SetGameObjectActive(true);

            Word _word = new(_wordModel, _stat, _wordView);

            _levelDatas.AddWord(_word);

            return _word;
        }

        private WordStat SelectWordStat()
        {
            WordStat[] _wordStats = _levelDatas.wordSetting.wordStats;
            int _range = _wordStats.Length;
            int _index = UnityEngine.Random.Range(0, _range);

            return _wordStats[_index];
        }
    }
}