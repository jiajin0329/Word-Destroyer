using System;
using UnityEngine;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordGeneratorModel
    {
        private LevelDatas _levelDatas;
        [SerializeField]
        private WordViewObjectPool _wordViewObjectPool;

        public struct GenerateParam
        {
            public WordStat wordStat;
            public Vector3 wordPosition;
            public string wordName;
        }

        public WordGeneratorModel(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
            _wordViewObjectPool = new(_levelDatas.wordSetting, 10);
        }

        public void Initialize(LevelDatas _levelDatas = null)
        {
            if(_levelDatas != null)
                this._levelDatas = _levelDatas;
            _wordViewObjectPool.Initialize(_levelDatas.wordSetting);
        }

        /// <summary>
        /// Generate Word and determine key and coordinates, then add to WordGeneratorData's wordDictionary, and finally return Word Class.
        /// </summary>
        public Word Generate(GenerateParam _param)
        {
            WordModel _wordModel = new WordMoveDown(_param.wordStat, _param.wordPosition, -6f);

            WordView _wordView = _wordViewObjectPool.Get();
            _wordView.SetTextName(_param.wordName);
            _wordView.SetPosition(_param.wordPosition);
            _wordView.SetGameObjectActive(true);

            Word _word = new(_wordModel, _param.wordStat, _wordView);

            _levelDatas.AddWord(_word);

            return _word;
        }
    }
}