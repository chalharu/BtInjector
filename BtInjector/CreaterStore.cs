using System;

namespace BtInjector
{
    class CreaterStore<T> : ICreaterStore
    {
        ICreater<T> _creater;
        public CreaterStore(ICreater<T> creater)
        {
            _creater = creater;
        }

        IBuilder<T> _builder;

        IGenerator ICreaterStore.GetGenerator()
		{
			return GetGenerator();
        }

        public IGenerator<T> GetGenerator()
        {
            if (_builder == null)
            {
                _builder = _creater.GetBuilder();
            }
            return Generator.Create(_builder, _creater.Lifecycle);
        }

        public ICreater Creater
        {
            get { return _creater; }
        }

        public ICreaterStore GetFuncCreater()
        {
            return new CreaterStore<Func<T>>(new FuncFromCreaterCreater<T>(this, DeclareMode.Implicit, Lifecycle.Singleton));
        }

        public ICreaterStore GetLazyCreater()
        {
            return new CreaterStore<Lazy<T>>(new LazyFromCreaterCreater<T>(this, DeclareMode.Implicit, _creater.Lifecycle));
        }
    }
}