namespace Logy.GeneralCommonV01
{
    public class TestObjectPool 
    {
        private ObjectPool<Test> _objectPool;
        public int idleCount => _objectPool.idleCount;
        public int usingCount => _objectPool.usingCount;

        public TestObjectPool (ushort _startAmount)
        {
            _objectPool = new(Create, Destory, _startAmount);
        }

        private Test Create()
        {
            Test _object = new();
            _object.isCreate = true;

            return _object;
        }

        private void Destory(Test _object)
        {
            _object.isDestory = true;
        }

        public Test Get()
        {
            return _objectPool.Get();
        }

        public void Release(Test _object)
        {
            _objectPool.Release(_object);
        }

        public void Destory()
        {
            _objectPool.Destory();
        }
    }
}


