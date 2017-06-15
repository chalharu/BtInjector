using System;

namespace BtInjector
{
    interface ICreater
    {
        Lifecycle Lifecycle { get; }
        Type ReturnType { get; }
        DeclareMode DeclareMode { get; }
    }

    interface ICreater<T> : ICreater
    {
        IBuilder<T> GetBuilder();
    }
}