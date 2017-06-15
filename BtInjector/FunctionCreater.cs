using System;

namespace BtInjector
{
    interface IFunctionCreater<TResult>
    {
        void SetFunction(Func<TResult> func);
        void SetFunction<T1>(Func<T1, TResult> func);
        void SetFunction<T1, T2>(Func<T1, T2, TResult> func);
        void SetFunction<T1, T2, T3>(Func<T1, T2, T3, TResult> func);
        void SetFunction<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> func);
        void SetFunction<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> func);
        void SetFunction<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> func);
        void SetFunction<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func);
        void SetFunction<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func);
        void SetFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func);
    }

    class FunctionCreater<T, TResult> : ICreater<T>, IFunctionCreater<TResult> where TResult : T
    {

        DeclareMode _declareMode;
        Lifecycle _lifecycle;
        Type[] _paramTypes;
        FunctionBuilderBase<T> _functionBuilder;

        Container _container;
        public FunctionCreater(DeclareMode delcareMode, Lifecycle lifecycle, Container container)
        {
            _declareMode = delcareMode;
            _lifecycle = lifecycle;
            _container = container;
        }

        public void SetFunction(Func<TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T, TResult>(func);
            _paramTypes = new Type[] { };
        }

        public void SetFunction<T1>(Func<T1, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T, TResult>(func);
            _paramTypes = new Type[] { typeof(T1) };
        }

        public void SetFunction<T1, T2>(Func<T1, T2, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2)
        };
        }

        public void SetFunction<T1, T2, T3>(Func<T1, T2, T3, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3)
        };
        }

        public void SetFunction<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4)
        };
        }

        public void SetFunction<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T5, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4),
            typeof(T5)
        };
        }

        public void SetFunction<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T5, T6, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4),
            typeof(T5),
            typeof(T6)
        };
        }

        public void SetFunction<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T5, T6, T7, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4),
            typeof(T5),
            typeof(T6),
            typeof(T7)
        };
        }

        public void SetFunction<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T5, T6, T7, T8, T, TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4),
            typeof(T5),
            typeof(T6),
            typeof(T7),
            typeof(T8)
        };
        }

        public void SetFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func)
        {
            _functionBuilder = new FunctionBuilder<T1, T2, T3, T4, T5, T6, T7, T8, T9, T,
            TResult>(func);
            _paramTypes = new Type[] {
            typeof(T1),
            typeof(T2),
            typeof(T3),
            typeof(T4),
            typeof(T5),
            typeof(T6),
            typeof(T7),
            typeof(T8),
            typeof(T9)
        };
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
            get { return typeof(T); }
        }

        public IBuilder<T> GetBuilder()
        {
            if (_functionBuilder == null)
                throw new InvalidOperationException("Functionが設定されていません");

            _functionBuilder.Container = _container;
            return _functionBuilder;
        }
    }
}