using System;

namespace BtInjector
{
    interface IGenerator
    {
        object GetInstanceMethod();
    }

    interface IGenerator<T> : IGenerator
    {
        new Func<T> GetInstanceMethod();
    }
}