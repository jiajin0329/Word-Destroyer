using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class LevelDatasUnitTest
    {
        private LevelDatas _levelDatas;
        private Word _word;

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

            _levelDatas.wordGeneratorDatas.AddWaitRemoveWordList(_word);
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

            _levelDatas.playerDatas.Reset();

            Assert.AreEqual(100, _levelDatas.playerDatas.GetHp());
        }
    }
}
