using System;

namespace BtInjector
{
    class SingletonGenerator<T> : IGenerator<T>
    {
        T _instance;
        bool _instanceCreated = false;
        object _lockobj = new object();

        IBuilder<T> _builder;
        Func<T> _getInstanceMethodFunc;
        public SingletonGenerator(IBuilder<T> builder)
        {
            _builder = builder;
            _getInstanceMethodFunc = () => GetInstance();
        }

        public T GetInstance()
        {
            if (!_instanceCreated)
            {
                lock (_lockobj)
                {
                    if (!_instanceCreated)
                    {
                        _instance = _builder.GetInstance();
                        _instanceCreated = true;
                    }
                }
            }
            return _instance;
        }

        public Func<T> GetInstanceMethod()
        {
            return _getInstanceMethodFunc;
        }

        object IGenerator.GetInstanceMethod()
        {
            return _getInstanceMethodFunc;
        }
    }
}