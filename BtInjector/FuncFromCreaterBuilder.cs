using System;

namespace BtInjector
{
    class FuncFromCreaterBuilder<T> : IBuilder<Func<T>>
    {
        CreaterStore<T> _creater;
        Func<T> _getInstance;

        public FuncFromCreaterBuilder(CreaterStore<T> creater)
        {
            _creater = creater;
        }

        void InitGetInstanceMethod()
        {
            _getInstance = _creater.GetGenerator().GetInstanceMethod();
        }

        public Func<T> GetInstance()
        {
            if(_getInstance == null)
                InitGetInstanceMethod();
            return _getInstance;
		}

		public Func<Func<T>> GetInstanceMethod()
		{
			if (_getInstance == null)
				InitGetInstanceMethod();
            return () => _getInstance;
		}
    }
}