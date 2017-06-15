using System;

namespace BtInjector
{
    class FuncFromCreaterCreater<T> : ICreater<Func<T>>
    {

        DeclareMode _declareMode;
        Lifecycle _lifecycle;

        CreaterStore<T> _creater;
        public FuncFromCreaterCreater(CreaterStore<T> creater, DeclareMode delcareMode, Lifecycle lifecycle)
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
            get { return typeof(Func<T>); }
        }

        public IBuilder<Func<T>> GetBuilder()
        {
            return new FuncFromCreaterBuilder<T>(_creater);
        }
    }
}
