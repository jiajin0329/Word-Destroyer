using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class LevelManager
    {
        [SerializeField]
        private LevelDatas _levelDatas;
        [SerializeField]
        private WordGenerator _wordGenerator = new();
        [SerializeField]
        private WordAttacker _wordDestroyer = new();

        public LevelManager(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
        }

        public void Initialize()
        {
            _levelDatas.Initialize();
            _wordGenerator.Initialize(_levelDatas);
            _wordDestroyer.Initialize(_levelDatas);
        }

        public async UniTaskVoid Start(CancellationToken _cancellationToken)
        {
            WordGeneratorSetting _wordGeneratorSetting = _levelDatas.wordGeneratorSetting;
            int _generateAmount = _wordGeneratorSetting.generateAmount;
            int _generateIntervalMs = _wordGeneratorSetting.generateIntervalMs;

            await _wordGenerator.RepeatGenerate(_generateAmount, _generateIntervalMs, _cancellationToken);
        }

        public void Tick()
        {
            _levelDatas.wordGeneratorDatas.TickWordHashSet();
            _wordDestroyer.Tick();
            _levelDatas.wordGeneratorDatas.RemoveWordByWordWillRemoveList();
        }
    }
}