using System.Text;
using System.Threading.Tasks;
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
            _levelManager = new(_levelDatas, new(new()), new Mock<ILevelMenu>().Object, LevelStartUI.BuildTestCountDown());
            _levelManager.Initialize();
        }

        [Test]
        public void CheckWordsTick()
        {
            BuildLevelManager();
            int _count = 0;

            while (_count < 10)
            {
                _count++;
                Word _word = Word.BuildTestWord("Test");

                _levelDatas.wordGeneratorDatas.AddWord(_word);
            }

            _levelManager.Tick();
        }

        [Test]
        public void CheckPlayerDie()
        {
            BuildLevelManager();

            _levelDatas.playerDatas.LoseHp(PlayerDatas.startHp);

            Assert.AreEqual(LevelDatas.State.levelFailed, _levelDatas.GetState());
        }

        [Test]
        public async Task CheckPlayerWin()
        {
            WordGeneratorSetting _wordGeneratorSetting = new(1, 0);
            LevelDatas _levelDatas = new(WordSetting.BuildTestSetting(), _wordGeneratorSetting);

            WordAttackerModel _wordAttackerModel = new();
            WordAttacker _wordAttacker = new(_wordAttackerModel);

            _levelManager = new(_levelDatas, _wordAttacker, new Mock<ILevelMenu>().Object, LevelStartUI.BuildTestCountDown());
            _levelManager.Initialize();

            await _levelManager.Start();
            
            _wordAttackerModel.Initialize(_levelDatas);
            StringBuilder _stringBuilder = new("Test");
            _wordAttackerModel.Attack(_stringBuilder);

            Assert.AreEqual(LevelDatas.State.levelWin, _levelDatas.GetState());
        }
    }
}


