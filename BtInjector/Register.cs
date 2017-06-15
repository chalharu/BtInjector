using System;

namespace BtInjector
{
    public class Register<T> : IRegister<T>
    {
        Container _container;
        public Register(Container container)
        {
            _container = container;
        }

        public void As<TResult>(Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            ConstructorCreater<T, TResult> creater = new ConstructorCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            _container.AddCreater(creater);
        }

        public void As(T instance)
        {
            InstanceCreater<T> creater = new InstanceCreater<T>(instance, DeclareMode.Explicit);
            _container.AddCreater(creater);
        }

        public void As<TResult>(Func<TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, TResult>(Func<T1, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, TResult>(Func<T1, T2, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }

        public void As<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T
        {
            FunctionCreater<T, TResult> creater = new FunctionCreater<T, TResult>(DeclareMode.Explicit, lifecycle, _container);
            creater.SetFunction(func);
            _container.AddCreater(creater);
        }
    }
}
