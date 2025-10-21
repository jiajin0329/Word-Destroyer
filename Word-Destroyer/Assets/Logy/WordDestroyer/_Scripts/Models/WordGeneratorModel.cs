using UnityEngine;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public class WordGeneratorModel
    {
        private LevelDatas _levelDatas;

        public struct GenerateParam
        {
            public WordStat wordStat;
            public Vector3 wordPosition;
            public string wordName;
        }

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
        }

        /// <summary>
        /// Generate Word and determine key and coordinates, then add to WordGeneratorData's wordDictionary, and finally return Word Class.
        /// </summary>
        public Word Generate(GenerateParam _param)
        {
            Word _word = _levelDatas.wordGeneratorDatas.wordObjectPool.Get();

            _word.SetStat(_param.wordStat);
            _word.SetPosition(_param.wordPosition);
            _word.SetTextName(_param.wordName);
            _word.SetGameObjectActive(true);

            UnityAction _attackListener = () =>
            {
                _levelDatas.wordGeneratorDatas.AddWaitRemoveWordList(_word);
                Debug.Log($"{_word.GetViewTextName()} attack");
            };

            _word.AddAttackListener(_attackListener);

            _levelDatas.wordGeneratorDatas.AddWord(_word);

            return _word;
        }
    }
}