using System;

namespace BtInjector
{
    static class Generator
    {
        public static IGenerator<T> Create<T>(IBuilder<T> builder, Lifecycle lifecycle)
        {
            switch (lifecycle)
            {
                case Lifecycle.Singleton:
                    return new SingletonGenerator<T>(builder);
                case Lifecycle.ThreadLocal:
                    return new ThreadLocalGenerator<T>(builder);
                case Lifecycle.Transient:
                    return new TransientGenerator<T>(builder);
                default:
                    throw new ArgumentException("Lifecycle is illegal");
            }
        }
    }
}