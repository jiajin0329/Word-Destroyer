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
            _wordAttacker = new(new());
            _wordAttacker.Initialize(_levelDatas);

            _wordAttacker.Tick();
        }

        [Test]
        public void CheckAddHpOnSuccessfulAttack()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            WordAttackerModel _wordAttackerModel = new();
            _wordAttackerModel.Initialize(_levelDatas);

            Word _word = Word.BuildTestWord("T");
            _levelDatas.wordGeneratorDatas.AddWord(_word);

            StringBuilder _stringBuilder = new("T");
            _wordAttackerModel.Attack(_stringBuilder);

            Assert.AreEqual(101, _levelDatas.playerDatas.GetHp());
            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckCantAttackOnGameOver()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            WordAttackerModel _wordAttackerModel = new();
            _wordAttackerModel.Initialize(_levelDatas);

            Word _word = Word.BuildTestWord("T");
            _levelDatas.wordGeneratorDatas.AddWord(_word);

            _levelDatas.SetState(LevelDatas.State.levelFailed);

            StringBuilder _stringBuilder = new("T");
            _wordAttackerModel.Attack(_stringBuilder);
            
            Assert.AreEqual(100, _levelDatas.playerDatas.GetHp());
            Assert.AreEqual(1 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsTrue(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(1 ,_levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }
    }
}
