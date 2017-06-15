using System;

namespace BtInjector
{
    class LazyFromCreaterBuilder<T> : IBuilder<Lazy<T>>
    {
        CreaterStore<T> _creater;
        Func<T> _getInstance;

        public LazyFromCreaterBuilder(CreaterStore<T> creater)
        {
            _creater = creater;
        }
        
        void InitGetInstanceMethod()
        {
            _getInstance = _creater.GetGenerator().GetInstanceMethod();
        }

        public Lazy<T> GetInstance()
        {
            if(_getInstance == null)
                InitGetInstanceMethod();
            return new Lazy<T>(_getInstance);
		}

		public Func<Lazy<T>> GetInstanceMethod()
		{
            if(_getInstance == null)
                InitGetInstanceMethod();
		    return () => new Lazy<T>(_getInstance);
		}
    }
}
