using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordGenerator
    {
        private LevelDatas _levelDatas;
        private WordGeneratorModel _model = new();
        [field: SerializeField]
        public int currentCount { get; private set; }

        public class GenerateParam
        {
            public WordStat wordStat;
            public Vector3 wordPosition;
            public string wordName;
        }

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _model.Initialize(_levelDatas);
        }

        public async UniTask RepeatGenerate(int _generateAmount, int _generateIntervalMs = 0, CancellationToken _cancellationToken = new())
        {
            currentCount = 0;
            while (currentCount < _generateAmount)
            {
                currentCount++;

                _model.Generate(BuildGenerateParam());

                if (_generateIntervalMs != 0)
                    await UniTask.Delay(_generateIntervalMs, cancellationToken: _cancellationToken);
                else
                    continue;
            }
        }

        private WordGeneratorModel.GenerateParam BuildGenerateParam()
        {
            return new ()
            {
                wordStat = SelectWordStat(),
                wordPosition = SelectGeneratePosition(),
                wordName = SelectWordName()
            };
        }

        private WordStat SelectWordStat()
        {
            WordStat[] _wordStats = _levelDatas.wordSetting.wordStats;
            int _range = _wordStats.Length;
            int _index = UnityEngine.Random.Range(0, _range);

            return _wordStats[_index];
        }

        private Vector3 SelectGeneratePosition()
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
    }
}