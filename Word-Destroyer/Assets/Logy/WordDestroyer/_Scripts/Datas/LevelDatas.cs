using System;
using UnityEngine;

namespace Logy.WordDestroyer
{
    /// <summary>
    /// Aggregate all dynamic and static data from Level.
    /// </summary>
    [Serializable]
    public class LevelDatas
    {
        // Setting
        [field: SerializeField]
        public WordSetting wordSetting { get; private set; }
        [field: SerializeField]
        public WordGeneratorSetting wordGeneratorSetting { get; private set; }

        // Data
        [field: SerializeField]
        public WordGeneratorDatas wordGeneratorDatas { get; private set; } = new();
        [field: SerializeField]
        public PlayerDatas playerDatas { get; private set; } = new();

        public LevelDatas() {}

        public LevelDatas(WordSetting _wordSetting, WordGeneratorSetting _wordGeneratorSetting)
        {
            wordSetting = _wordSetting;
            wordGeneratorSetting = _wordGeneratorSetting;
        }

        public void Initialize()
        {
            wordGeneratorDatas.wordObjectPool.Initialize(wordSetting);
        }

        public static LevelDatas BuildTestDatas()
        {
            WordGeneratorSetting _wordGeneratorSetting = new();
            LevelDatas _levelDatas = new(WordSetting.BuildTestSetting(), _wordGeneratorSetting);
            _levelDatas.Initialize();
            return _levelDatas;
        }
    }
}
