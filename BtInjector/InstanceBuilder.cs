using System;

namespace BtInjector
{
    class InstanceBuilder<T> : IBuilder<T>
    {
        T _instance;
        public InstanceBuilder(T instance)
        {
            _instance = instance;
        }

        public T GetInstance()
        {
            return _instance;
        }

        public Func<T> GetInstanceMethod()
        {
            return () => _instance;
        }
    }
}