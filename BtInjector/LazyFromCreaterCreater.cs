using System;

namespace BtInjector
{
    class LazyFromCreaterCreater<T> : ICreater<Lazy<T>>
    {
        DeclareMode _declareMode;
        Lifecycle _lifecycle;

        CreaterStore<T> _creater;
        public LazyFromCreaterCreater(CreaterStore<T> creater, DeclareMode delcareMode, Lifecycle lifecycle)
        {
            _declareMode = delcareMode;
            _lifecycle = lifecycle;
            _creater = creater;
        }

        public DeclareMode DeclareMode
        {
            get { return _declareMode; }
        }

        public Lifecycle Lifecycle
        {
            get { return _lifecycle; }
        }

        public Type ReturnType
        {
            get { return typeof(Lazy<T>); }
        }

        public IBuilder<Lazy<T>> GetBuilder()
        {
            return new LazyFromCreaterBuilder<T>(_creater);
        }
    }
}
