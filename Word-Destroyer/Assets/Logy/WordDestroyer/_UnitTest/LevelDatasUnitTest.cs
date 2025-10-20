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
            _levelDatas.Initialize();

            _word = Word.BuildTestWord("Test");

            _levelDatas.AddWord(_word);

            Assert.AreEqual(1 ,_levelDatas.GetWordHashSetCount());
            Assert.IsTrue(_levelDatas.WordHashSetContains(_word));
            Assert.AreEqual(1 ,_levelDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckRemoveWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _levelDatas.Initialize();

            _word = Word.BuildTestWord("Test");

            _levelDatas.AddWord(_word);
            _levelDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.WordHashSetContains(_word));
            Assert.AreEqual(0, _levelDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }
        
        [Test]
        public void CheckRemoveRemovedWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _levelDatas.Initialize();

            _word = Word.BuildTestWord("Test");

            _levelDatas.AddWord(_word);
            _levelDatas.RemoveWord(_word.GetViewTextName());
            _levelDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.WordHashSetContains(_word));
            Assert.AreEqual(0 ,_levelDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }

        [Test]
        public void CheckRemoveUnexistWord()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _levelDatas.Initialize();

            _word = Word.BuildTestWord("Test");

            _levelDatas.RemoveWord(_word.GetViewTextName());

            Assert.AreEqual(0 ,_levelDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.WordHashSetContains(_word));
            Assert.AreEqual(0, _levelDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }
        
        [Test]
        public void CheckRemoveWordByWordWillRemoveList()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _levelDatas.Initialize();

            _word = Word.BuildTestWord("Test");

            _levelDatas.AddWord(_word);

            _levelDatas.AddWaitRemoveWordList(_word);
            Assert.AreEqual(1, _levelDatas.GetWaitRemoveWordListCount());

            _levelDatas.RemoveWordByWordWillRemoveList();

            Assert.AreEqual(0 ,_levelDatas.GetWordHashSetCount());
            Assert.IsFalse(_levelDatas.WordHashSetContains(_word));
            Assert.AreEqual(0 ,_levelDatas.GetWordDictionarWordQueueCount(_word.GetViewTextName()));
        }
    }
}
