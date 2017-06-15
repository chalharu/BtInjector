using System;
using System.Threading;

namespace BtInjector
{
    class ThreadLocalGenerator<T> : IGenerator<T>
    {

		ThreadLocal<T> _threadLocal;
		Func<T> _getInstanceMethodFunc;

        IBuilder<T> _builder;
        public ThreadLocalGenerator(IBuilder<T> builder)
        {
            _builder = builder;
            _getInstanceMethodFunc = () => GetInstance();
        }

        public T GetInstance()
        {
            if (_threadLocal == null)
                _threadLocal = new ThreadLocal<T>(_builder.GetInstanceMethod());

            return _threadLocal.Value;
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
