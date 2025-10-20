using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class WordDestroyerUnitTest
    {
        private LevelDatas _levelDatas;
        private WordAttacker _wordDestroyer;

        [Test]
        public void CheckTick()
        {
            _levelDatas = LevelDatas.BuildTestDatas();
            _wordDestroyer = new();
            _wordDestroyer.Initialize(_levelDatas);

            _wordDestroyer.Tick();
        }
    }
}
