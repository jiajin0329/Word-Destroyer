using NUnit.Framework;

namespace Logy.WordDestroyer
{
    public class WordObjectPoolUnitTest
    {
        private WordObjectPool _objectPool;

        [Test]
        public void CheckCreate10()
        {
            BuildObjectPool(10);
            
            Assert.AreEqual(10, _objectPool.idleCount);
        }

        private void BuildObjectPool(ushort _startAmount)
        {
            _objectPool = new(_startAmount);
            _objectPool.Initialize(WordSetting.BuildTestSetting());
        }

        [Test]
        public void CheckGet10()
        {
            BuildObjectPool(1);

            byte _count = 0;
            while (_count < 10)
            {
                _count++;
                var _object = _objectPool.Get();

                Assert.IsNotNull(_object);
                Assert.AreEqual(0, _objectPool.idleCount);
            }
            
            Assert.AreEqual(10, _objectPool.usingCount);
        }

        [Test]
        public void CheckRelease10()
        {
            BuildObjectPool(0);

            byte _count = 0;
            while (_count < 10)
            {
                _count++;
                var _object = _objectPool.Get();
                _objectPool.Release(_object);

                Assert.AreEqual(0, _objectPool.usingCount);
            }

            Assert.AreEqual(1, _objectPool.idleCount);
        }
    }
}


