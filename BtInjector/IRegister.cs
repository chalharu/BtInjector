using System;

namespace BtInjector
{
    public interface IRegister<T>
    {
        void As<TResult>(Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As(T instance);
        void As<TResult>(Func<TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, TResult>(Func<T1, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, TResult>(Func<T1, T2, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
        void As<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, Lifecycle lifecycle = Lifecycle.Transient) where TResult : T;
    }
}