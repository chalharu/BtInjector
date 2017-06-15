using System;
using System.Reflection;

namespace BtInjector
{
    class ConstructorCreater<T, TReal> : ICreater<T> where TReal : T
    {
        DeclareMode _declareMode;
        Lifecycle _lifecycle;
        Type[] _paramTypes;

        Container _container;
        public ConstructorCreater(DeclareMode delcareMode, Lifecycle lifecycle, Container container)
        {
            _declareMode = delcareMode;
            _lifecycle = lifecycle;
            _container = container;

            var cons = ConstructorInfoCache<TReal>.ConstructorInfos;
            if (cons.Length != 1)
            {
                throw new ArgumentException(string.Format("コンストラクタが定まりません[{0}]", typeof(T)));
            }
            var consInfo = cons[0];

			// _paramTypes = consInfo.GetParameters().Select(x => x.ParameterType).ToArray();
			var parameters = consInfo.GetParameters();
            var paramTypes = new Type[parameters.Length];
            for (int i = 0; i < paramTypes.Length; ++i)
                paramTypes[i] = parameters[i].ParameterType;
            _paramTypes = paramTypes;
        }

        public DeclareMode DeclareMode
        {
            get { return _declareMode; }
        }

        public Lifecycle Lifecycle
        {
            get { return _lifecycle; }
        }

        public Type[] ParameterTypes
        {
            get { return _paramTypes; }
        }

        public Type ReturnType
        {
            get { return typeof(T); }
        }

        public IBuilder<T> GetBuilder()
        {
            return new ConstructorBuilder<T, TReal>(this, _container);
        }
    }

    static class ConstructorCreater
    {
        static ConstructorCreater()
        {
#if NETSTANDARD1_1
            createStoreMethod = typeof(ConstructorCreater).GetTypeInfo().GetDeclaredMethod("CreateStoreGeneric");
#else
            createStoreMethod = typeof(ConstructorCreater).GetMethod("CreateStoreGeneric", BindingFlags.Static | BindingFlags.Public);
#endif
        }

        static MethodInfo createStoreMethod;
        public static ICreaterStore CreateStore(Type type, DeclareMode delcareMode, Lifecycle lifecycle, Container container)
        {
            return (ICreaterStore)createStoreMethod.MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] {
                delcareMode,
                lifecycle,
                container
            });
        }

	[Preserve]
        public static ICreaterStore CreateStoreGeneric<T>(DeclareMode delcareMode, Lifecycle lifecycle, Container container)
        {
            var creater = new ConstructorCreater<T, T>(delcareMode, lifecycle, container);
            return new CreaterStore<T>(creater);
        }
    }
}
