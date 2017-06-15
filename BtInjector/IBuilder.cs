using System;

namespace BtInjector
{
    interface IBuilder<T>
    {
        T GetInstance();
        Func<T> GetInstanceMethod();
	}
}