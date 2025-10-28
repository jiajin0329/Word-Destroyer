using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class PlayerDatasUnitTest
    {
        private PlayerDatas _playerDatas;

        [Test]
        public void CheckAddHp()
        {
            _playerDatas = new();

            int _previousHp = _playerDatas.GetHp();
            int _callBackGetHp = 0;
            _playerDatas.AddAddHpListener((int _hp) => _callBackGetHp = _hp);
            _playerDatas.AddHp(10);

            int _expectedHp = _previousHp + 10;
            Assert.AreEqual(_expectedHp, _playerDatas.GetHp());
            Assert.AreEqual(_expectedHp, _callBackGetHp);
        }

        [Test]
        public void CheckLoseHp()
        {
            _playerDatas = new();

            int _previousHp = _playerDatas.GetHp();
            int _callBackGetHp = 0;
            _playerDatas.AddLoseHpEventListener((int _hp) => _callBackGetHp = _hp);
            _playerDatas.LoseHp(10);

            int _expectedHp = _previousHp - 10;
            Assert.AreEqual(_expectedHp, _playerDatas.GetHp());
            Assert.AreEqual(_expectedHp, _callBackGetHp);
        }

        [Test]
        public void CheckReset()
        {
            _playerDatas = new();

            _playerDatas.LoseHp(10);

            _playerDatas.ReSet();
            
            Assert.AreEqual(100, _playerDatas.GetHp());
        }
    }
}
