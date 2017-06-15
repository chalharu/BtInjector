using System.Reflection;
#if NETSTANDARD1_1
using System.Linq;
#endif

namespace BtInjector
{
    static class ConstructorInfoCache<T>
    {
#pragma warning disable // Warns about static fields in generic types
#if NETSTANDARD1_1
        public readonly static ConstructorInfo[] ConstructorInfos = typeof(T).GetTypeInfo().DeclaredConstructors.ToArray();
#else
        public readonly static ConstructorInfo[] ConstructorInfos = typeof(T).GetConstructors();
#endif
#pragma warning restore // Warns about static fields in generic types
    }
}
