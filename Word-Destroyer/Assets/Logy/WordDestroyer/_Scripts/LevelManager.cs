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
        private WordGeneratorModel _wordGeneratorModel;

        public LevelManager(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _wordGeneratorModel = new(_levelDatas);
        }

        public void Initialize()
        {
            _wordGeneratorModel.Initialize(_levelDatas);
        }

        public async UniTaskVoid Start(CancellationToken _cancellationToken)
        {
            WordGeneratorSetting _wordGeneratorSetting = _levelDatas.wordGeneratorSetting;
            int _generateAmount = _wordGeneratorSetting.generateAmount;
            int _generateIntervalMs = _wordGeneratorSetting.generateIntervalMs;

            await _wordGeneratorModel.RepeatGenerate(_generateAmount, _generateIntervalMs, _cancellationToken);
        }

        public void Tick()
        {
            WordsTick();
        }

        private void WordsTick()
        {
            foreach(Word _word in _levelDatas.wordHashSet)
            {
                _word.Tick();
            }
        }
    }
}