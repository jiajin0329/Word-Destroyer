using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Record Word Object.
        /// </summary>
        public HashSet<Word> wordHashSet { get; private set; }

        public LevelDatas()
        {
            wordHashSet = new();
        }

        public LevelDatas(WordSetting _wordSetting, WordGeneratorSetting _wordGeneratorSetting)
        {
            wordSetting = _wordSetting;
            wordGeneratorSetting = _wordGeneratorSetting;
            wordHashSet = new();
        }
        
        /// <summary>
        /// Add to WordGeneratorData's wordDictionary.
        /// </summary>
        public void AddWord(Word _word)
        {
            wordHashSet.Add(_word);
        }

        public static LevelDatas BuildTestDatas()
        {
            WordGeneratorSetting _wordGeneratorSetting = new();

            return new LevelDatas(WordSetting.BuildTestSetting(), _wordGeneratorSetting);
        }
    }
}
