using System;

namespace BtInjector
{
    abstract class FunctionBuilderBase<T> : IBuilder<T>
    {
        public Func<T> GetInstanceMethod()
        {
            if (_retFunc == null)
                InitRetFunc();
            return _retFunc;
        }

        public T GetInstance()
        {
            if (_retFunc == null)
                InitRetFunc();
            return _retFunc();
        }

        public Container Container { get; set; }

        protected abstract void InitRetFunc();
        protected Func<T> _retFunc;
    }

    class FunctionBuilder<T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        public FunctionBuilder(Func<TResult> func)
        {
            _retFunc = () => func();
        }

        protected override void InitRetFunc()
        {
        }
    }

    class FunctionBuilder<TArg1, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TResult> _func;
        public FunctionBuilder(Func<TArg1, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            _retFunc =  () => _func(gen1());
        }
    }

    class FunctionBuilder<TArg1, TArg2, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            _retFunc = () => _func(gen1(), gen2());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, TArg5, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            var gen5 = Container.GetDelegate<TArg5>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4(),
                                   gen5());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            var gen5 = Container.GetDelegate<TArg5>();
            var gen6 = Container.GetDelegate<TArg6>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4(),
                                   gen5(),
                                   gen6());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            var gen5 = Container.GetDelegate<TArg5>();
            var gen6 = Container.GetDelegate<TArg6>();
            var gen7 = Container.GetDelegate<TArg7>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4(),
                                   gen5(),
                                   gen6(),
                                   gen7());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            var gen5 = Container.GetDelegate<TArg5>();
            var gen6 = Container.GetDelegate<TArg6>();
            var gen7 = Container.GetDelegate<TArg7>();
            var gen8 = Container.GetDelegate<TArg8>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4(),
                                   gen5(),
                                   gen6(),
                                   gen7(),
                                   gen8());
        }
    }

    class FunctionBuilder<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, T, TResult> : FunctionBuilderBase<T> where TResult : T
    {
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> _func;
        public FunctionBuilder(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> func)
        {
            _func = func;
        }

        protected override void InitRetFunc()
        {
            var gen1 = Container.GetDelegate<TArg1>();
            var gen2 = Container.GetDelegate<TArg2>();
            var gen3 = Container.GetDelegate<TArg3>();
            var gen4 = Container.GetDelegate<TArg4>();
            var gen5 = Container.GetDelegate<TArg5>();
            var gen6 = Container.GetDelegate<TArg6>();
            var gen7 = Container.GetDelegate<TArg7>();
            var gen8 = Container.GetDelegate<TArg8>();
            var gen9 = Container.GetDelegate<TArg9>();
            _retFunc = () => _func(gen1(),
                                   gen2(),
                                   gen3(),
                                   gen4(),
                                   gen5(),
                                   gen6(),
                                   gen7(),
                                   gen8(),
                                   gen9());
        }
    }
}