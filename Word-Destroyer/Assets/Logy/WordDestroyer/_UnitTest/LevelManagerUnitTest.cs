using Moq;
using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class LevelManagerUnitTest
    {
        private LevelDatas _levelDatas;
        private LevelManager _levelManager;
        private WordView _wordView;

        [Test]
        public void CheckInitialize()
        {
            BuildLevelManager();
        }

        private void BuildLevelManager()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _levelManager = new(_levelDatas);
            _levelManager.Initialize();
        }

        [Test]
        public void CheckWordsTick()
        {
            BuildLevelManager();;
            int _count = 0;

            while (_count < 10)
            {
                _count++;
                Word _word = Word.BuildTestWord("Test");

                _levelDatas.AddWord(_word);
            }

            _levelManager.Tick();
        }
    }
}


