using System;

namespace BtInjector
{
    class TransientGenerator<T> : IGenerator<T>
    {
        IBuilder<T> _builder;
        Func<T> _getInstanceMethod;

        public TransientGenerator(IBuilder<T> builder)
        {
            _builder = builder;
        }

        public Func<T> GetInstanceMethod()
		{
			if (_getInstanceMethod == null)
				_getInstanceMethod = _builder.GetInstanceMethod();

			return _getInstanceMethod;
		}

		object IGenerator.GetInstanceMethod()
		{
			return GetInstanceMethod();
		}
    }
}