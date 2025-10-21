using System.Text;
using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class WordAttackerUnitTest
    {
        private LevelDatas _levelDatas;
        private WordAttacker _wordAttacker;

        [Test]
        public void CheckTick()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordAttacker = new();
            _wordAttacker.Initialize(_levelDatas);

            _wordAttacker.Tick();
        }

        [Test]
        public void CheckAddHpOnSuccessfulAttack()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            WordAttackerModel _wordAttackerModel = new();
            _wordAttackerModel.Initialize(_levelDatas);

            _levelDatas.wordGeneratorDatas.AddWord(Word.BuildTestWord("T"));

            StringBuilder _stringBuilder = new("T");
            _wordAttackerModel.Attack(_stringBuilder);

            Assert.AreEqual(101, _levelDatas.playerDatas.GetHp());
        }
    }
}
