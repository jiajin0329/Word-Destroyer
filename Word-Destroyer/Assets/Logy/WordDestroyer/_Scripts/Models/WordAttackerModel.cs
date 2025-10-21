using System;
using System.Text;

namespace Logy.WordDestroyer
{
    [Serializable]
    public class WordAttackerModel
    {
        private LevelDatas _levelDatas;

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
        }
        
        public void Attack(StringBuilder _inputString)
        {
            if (_inputString.Length < 1) return;

            Word _word = _levelDatas.wordGeneratorDatas.RemoveWord(_inputString.ToString().ToUpper());

            if (_word == null) return;

            _levelDatas.playerDatas.AddHp(1);
            _levelDatas.wordGeneratorDatas.wordObjectPool.Release(_word);
        }
    }
}