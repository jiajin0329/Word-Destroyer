using NUnit.Framework;

namespace Logy.GeneralCommonV01
{
    public class ObjectPoolUnitTest
    {
        private TestObjectPool _objectPool;

        [Test]
        public void CheckCreate10()
        {
            _objectPool = new(10);

            Assert.AreEqual(10, _objectPool.idleCount);
            Assert.AreEqual(0, _objectPool.usingCount);
            Assert.IsTrue(_objectPool.Get().isCreate);
        }

        [Test]
        public void CheckGet10()
        {
            _objectPool = new(1);

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
            _objectPool = new(0);

            byte _count = 0;
            while (_count < 10)
            {
                _count++;
                var _object = _objectPool.Get();
                _object.isUse = true;
                _objectPool.Release(_object);

                Assert.AreEqual(0, _objectPool.usingCount);
                Assert.AreEqual(false, _object.isUse);
            }

            Assert.AreEqual(1, _objectPool.idleCount);
        }

        [Test]
        public void CheckReleaseAll()
        {
            _objectPool = new(0);

            byte _count = 0;
            while (_count < 10)
            {
                _count++;
                _objectPool.Get();
            }

            _objectPool.ReleaseAll();

            Assert.AreEqual(10, _objectPool.idleCount);
            Assert.AreEqual(0, _objectPool.usingCount);
        }

        [Test]
        public void CheckDestory()
        {
            _objectPool = new(10);

            Test _object = null;
            byte _count = 0;
            while (_count < 5)
            {
                _count++;
                _object = _objectPool.Get();
            }

            _objectPool.Destory();

            Assert.AreEqual(0, _objectPool.idleCount);
            Assert.AreEqual(0, _objectPool.usingCount);
            Assert.AreEqual(true, _object.isDestory);
        }
    }
}


