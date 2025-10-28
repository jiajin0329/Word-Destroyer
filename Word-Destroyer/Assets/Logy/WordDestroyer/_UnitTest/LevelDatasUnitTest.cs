using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class LevelDatasUnitTest
    {
        private LevelDatas _levelDatas;
        private Word _word;

        [Test]
        public void CheckStart()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");
            _levelDatas.wordGeneratorDatas.AddWord(_word);

            _levelDatas.playerDatas.LoseHp(100);

            _levelDatas.ReSet();

            Assert.AreEqual(100, _levelDatas.playerDatas.GetHp());
            Assert.AreEqual(_levelDatas.wordGeneratorDatas.wordObjectPool.startAmount, _levelDatas.wordGeneratorDatas.wordObjectPool.idleCount);
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.wordObjectPool.usingCount);
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount("Test"));
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWaitRemoveWordListCount());
        }

        [Test]
        public void CheckAddWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");

            _levelDatas.wordGeneratorDatas.AddWord(_word);

            Assert.AreEqual(1 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsTrue(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(1 ,_levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckRemoveWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");

            _levelDatas.wordGeneratorDatas.AddWord(_word);
            _levelDatas.wordGeneratorDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }
        
        [Test]
        public void CheckRemoveRemovedWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");

            _levelDatas.wordGeneratorDatas.AddWord(_word);
            _levelDatas.wordGeneratorDatas.RemoveWord(_word.GetViewTextName());
            _levelDatas.wordGeneratorDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckRemoveUnexistWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");

            _levelDatas.wordGeneratorDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckRemoveWordByWordWillRemoveList()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _word = Word.BuildTestWord("Test");

            _levelDatas.wordGeneratorDatas.AddWord(_word);

            _levelDatas.wordGeneratorDatas.AddWaitRemoveWordList("Test");
            Assert.AreEqual(1, _levelDatas.wordGeneratorDatas.GetWaitRemoveWordListCount());

            _levelDatas.wordGeneratorDatas.RemoveWordByWordWillRemoveList();

            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.wordGeneratorDatas.WordHashSetContains(_word));
            Assert.AreEqual(0, _levelDatas.wordGeneratorDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckReset()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            _levelDatas.playerDatas.ReSet();

            Assert.AreEqual(PlayerDatas.startHp, _levelDatas.playerDatas.GetHp());
            Assert.AreEqual(LevelDatas.State.levelStart, _levelDatas.GetState());
        }

        [Test]
        public void CheckSetState()
        {
            _levelDatas = LevelDatas.BuildTestDatas();

            LevelDatas.State _state = LevelDatas.State.levelStart;

            _levelDatas.AddSetStateListener((LevelDatas.State _set) => _state = _set);
            _levelDatas.SetState(LevelDatas.State.levelFailed);

            Assert.AreEqual(LevelDatas.State.levelFailed, _levelDatas.GetState());
            Assert.AreEqual(LevelDatas.State.levelFailed, _state);
        }
    }
}
