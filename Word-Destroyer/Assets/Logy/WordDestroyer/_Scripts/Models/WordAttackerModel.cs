using System.Text;
using UnityEngine.Events;

namespace Logy.WordDestroyer
{
    public class WordAttackerModel
    {
        private LevelDatas _levelDatas;

        public void Initialize(LevelDatas _levelDatas)
        {
            this._levelDatas = _levelDatas;
        }

        public void Reset()
        {
            
        }

        public void Attack(StringBuilder _inputString)
        {
            if (_levelDatas.GetState() == LevelDatas.State.levelFailed)
                return;
            if (_inputString.Length < 1)
                return;

            Word _word = _levelDatas.wordGeneratorDatas.RemoveWord(_inputString.ToString().ToUpper());

            if (_word == null) return;

            _levelDatas.playerDatas.AddHp(1);
        }
    }
}