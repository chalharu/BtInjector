#if __IOS__
#define FULLAOT
#endif
#if NETSTANDARD1_1
#define PCL
#endif

using System;
using System.Reflection;
#if FULLAOT
using System.Linq;
#elif PCL
using System.Linq;
using System.Linq.Expressions;
#else
using System.Reflection.Emit;
#endif

namespace BtInjector
{
    class ConstructorBuilder<T, TReal> : IBuilder<T> where TReal : T
    {
        ConstructorCreater<T, TReal> _creater;
        Container _container;
        public ConstructorBuilder(ConstructorCreater<T, TReal> creater, Container container)
        {
            _creater = creater;
            _container = container;
        }

        Func<T> _getInstanceDelegate;

        void InitGetInstanceDelegate()
        {
#if FULLAOT
            var consInfo = ConstructorInfoCache<TReal>.ConstructorInfos[0];
            switch (_creater.ParameterTypes.Length)
            {
                case 0:
                    // 引数が0個の場合はActivatorが速い
                    _getInstanceDelegate = () => Activator.CreateInstance<TReal>();
                    break;
                // 引数が0個以外の場合はConstructorInfo.Invokeが速い
                case 1:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1() });
                    }
                    break;
                case 2:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2() });
                    }
                    break;
                case 3:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3() });
                    }
                    break;
                case 4:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4() });
                    }
                    break;
                case 5:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        var gen5 = _container.GetDelegate(_creater.ParameterTypes[4]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4(), gen5() });
                    }
                    break;
                case 6:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        var gen5 = _container.GetDelegate(_creater.ParameterTypes[4]);
                        var gen6 = _container.GetDelegate(_creater.ParameterTypes[5]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4(), gen5(), gen6() });
                    }
                    break;
                case 7:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        var gen5 = _container.GetDelegate(_creater.ParameterTypes[4]);
                        var gen6 = _container.GetDelegate(_creater.ParameterTypes[5]);
                        var gen7 = _container.GetDelegate(_creater.ParameterTypes[6]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4(), gen5(), gen6(), gen7() });
                    }
                    break;
                case 8:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        var gen5 = _container.GetDelegate(_creater.ParameterTypes[4]);
                        var gen6 = _container.GetDelegate(_creater.ParameterTypes[5]);
                        var gen7 = _container.GetDelegate(_creater.ParameterTypes[6]);
                        var gen8 = _container.GetDelegate(_creater.ParameterTypes[7]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4(), gen5(), gen6(), gen7(), gen8() });
                    }
                    break;
                case 9:
                    {
                        var gen1 = _container.GetDelegate(_creater.ParameterTypes[0]);
                        var gen2 = _container.GetDelegate(_creater.ParameterTypes[1]);
                        var gen3 = _container.GetDelegate(_creater.ParameterTypes[2]);
                        var gen4 = _container.GetDelegate(_creater.ParameterTypes[3]);
                        var gen5 = _container.GetDelegate(_creater.ParameterTypes[4]);
                        var gen6 = _container.GetDelegate(_creater.ParameterTypes[5]);
                        var gen7 = _container.GetDelegate(_creater.ParameterTypes[6]);
                        var gen8 = _container.GetDelegate(_creater.ParameterTypes[7]);
                        var gen9 = _container.GetDelegate(_creater.ParameterTypes[8]);
                        _getInstanceDelegate = () => (T)consInfo.Invoke(new object[] { gen1(), gen2(), gen3(), gen4(), gen5(), gen6(), gen7(), gen8(), gen9() });
                    }
                    break;
                default:
                    {
                        var generators = _creater.ParameterTypes.Select(p => _container.GetDelegate(p)).ToList();
                        _getInstanceDelegate = () => (T)consInfo.Invoke(generators.Select(x => x()).ToArray());
                    }
                    break;
            }
#elif PCL // #if FULLAOT
            if (_creater.ParameterTypes.Length == 0)
            {
                _getInstanceDelegate = Expression.Lambda<Func<T>>(Expression.Convert(Expression.New(ConstructorInfoCache<TReal>.ConstructorInfos[0]), typeof(T))).Compile();
            }
            else
            {
                var paramGenerators =
                    _creater
                        .ParameterTypes
                        .Select(p =>
                        {
                            var funcType = typeof(Func<>).MakeGenericType(new Type[] { p });
#if NETSTANDARD1_1
                            var mi = funcType.GetTypeInfo().GetDeclaredMethod("Invoke");
#else
                            var mi = funcType.GetMethod("Invoke");
#endif
                            var funcCons = Expression.Constant(_container.GetDelegate(p), funcType);
                            return Expression.Call(funcCons, mi);
                        });
                _getInstanceDelegate = Expression.Lambda<Func<T>>(
                    Expression.Convert(
                        Expression.New(ConstructorInfoCache<TReal>.ConstructorInfos[0], paramGenerators),
                        typeof(T))).Compile();
            }
#else // #if FULLAOT
            var type = GeneratedTypeCache<T, TReal>.CreaterType;
            var instance = Activator.CreateInstance(type);

            if (_creater.ParameterTypes.Length == 0)
            {
                _getInstanceDelegate = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), GeneratedTypeCache<T, TReal>.ConstructorMethodInfo);
            }
            else
            {
                //var paramDelegates = _creater.ParameterTypes.Select(x => _container.GetDelegate(x)).ToArray();
                var paramTypes = _creater.ParameterTypes;
                var paramDelegates = new object[paramTypes.Length];
                for (int i = 0; i < paramDelegates.Length; ++i)
                    paramDelegates[i] = _container.GetDelegate(paramTypes[i]);

                GeneratedTypeCache<T, TReal>.SetParametersMethod(instance, paramDelegates);
                _getInstanceDelegate = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), instance, GeneratedTypeCache<T, TReal>.ConstructorMethodInfo);
            }
#endif // #if FULLAOT
        }

        public T GetInstance()
        {
            if (_getInstanceDelegate == null)
                InitGetInstanceDelegate();

            return _getInstanceDelegate();
        }

        public Func<T> GetInstanceMethod()
        {
            if (_getInstanceDelegate == null)
                InitGetInstanceDelegate();

            return _getInstanceDelegate;
        }
    }

#if !FULLAOT && !PCL
    static class GeneratedTypeCache<T, TReal>
    {
#pragma warning disable
        // #pragma warning disable RECS0108 // Warns about static fields in generic types
        public static Type CreaterType { get; private set; }
        public static Action<object, object[]> SetParametersMethod { get; private set; }
        public static MethodInfo ConstructorMethodInfo { get; private set; }
        // #pragma warning restore RECS0108 // Warns about static fields in generic types
#pragma warning restore

        static GeneratedTypeCache()
        {
            var consInfo = ConstructorInfoCache<TReal>.ConstructorInfos[0];

            var typeBuilder = GeneratedModuleCache.ModuleBuilder.DefineType("$$DynamicMethods$$__" + typeof(T).FullName + "__" + typeof(TReal).FullName, TypeAttributes.Public, typeof(object));

            //var funcParams = consInfo.GetParameters().Select(x => typeof(Func<>).MakeGenericType(new Type[] { x.ParameterType })).ToArray();
            var consParams = consInfo.GetParameters();
            var funcParams = new Type[consParams.Length];
            for (int i = 0; i < funcParams.Length; ++i)
                funcParams[i] = typeof(Func<>).MakeGenericType(consParams[i].ParameterType);

            var isStatic = funcParams.Length == 0;

            var methodBuilder = typeBuilder.DefineMethod("Constructor", isStatic ? MethodAttributes.Static | MethodAttributes.Public : MethodAttributes.Public, typeof(T), Type.EmptyTypes);
            var il = methodBuilder.GetILGenerator();

            var setParamMethodBuilder = typeBuilder.DefineMethod("SetParameters", MethodAttributes.Static | MethodAttributes.Public, null, new Type[] { typeof(object), typeof(object[]) });
            var setParamIl = setParamMethodBuilder.GetILGenerator();

            for (int i = 0; i < funcParams.Length; ++i)
            {
                var pName = "Param" + i.ToString();
                var mi = funcParams[i].GetMethod("Invoke");
                var field = typeBuilder.DefineField(pName, funcParams[i], FieldAttributes.Private);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, field);
                il.Emit(OpCodes.Callvirt, mi);

                setParamIl.Emit(OpCodes.Ldarg_0);
                setParamIl.Emit(OpCodes.Ldarg_1);
                switch (i)
                {
                    case 0:
                        setParamIl.Emit(OpCodes.Ldc_I4_0);
                        break;
                    case 1:
                        setParamIl.Emit(OpCodes.Ldc_I4_1);
                        break;
                    case 2:
                        setParamIl.Emit(OpCodes.Ldc_I4_2);
                        break;
                    case 3:
                        setParamIl.Emit(OpCodes.Ldc_I4_3);
                        break;
                    case 4:
                        setParamIl.Emit(OpCodes.Ldc_I4_4);
                        break;
                    case 5:
                        setParamIl.Emit(OpCodes.Ldc_I4_5);
                        break;
                    case 6:
                        setParamIl.Emit(OpCodes.Ldc_I4_6);
                        break;
                    case 7:
                        setParamIl.Emit(OpCodes.Ldc_I4_7);
                        break;
                    case 8:
                        setParamIl.Emit(OpCodes.Ldc_I4_8);
                        break;
                    default:
                        setParamIl.Emit(OpCodes.Ldc_I4_S, (byte)i);
                        break;
                }
                setParamIl.Emit(OpCodes.Ldelem, funcParams[i]);
                setParamIl.Emit(OpCodes.Stfld, field);
            }

            il.Emit(OpCodes.Newobj, consInfo);
            il.Emit(OpCodes.Ret);

            setParamIl.Emit(OpCodes.Ret);

            CreaterType = typeBuilder.CreateType();
            SetParametersMethod = (Action<object, object[]>)Delegate.CreateDelegate(typeof(Action<object, object[]>),
                CreaterType.GetMethod("SetParameters", BindingFlags.Static | BindingFlags.Public));
            ConstructorMethodInfo = CreaterType.GetMethod("Constructor", (isStatic ? BindingFlags.Static : BindingFlags.Instance) | BindingFlags.Public);
        }
    }

    static class GeneratedModuleCache
    {
        static Cache _cache = new Cache();
        public static ModuleBuilder ModuleBuilder
        {
            get
            {
                return _cache.InnerModuleBuilder;
            }
        }

        class Cache
        {
            const string AssemblyFileName = "Debug.dll";
            AssemblyBuilder AssemblyBuilder;
            public ModuleBuilder InnerModuleBuilder { get; private set; }

            public Cache()
            {
#if DEBUG
                AssemblyBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName("DynamicTypes"), AssemblyBuilderAccess.RunAndSave);
                InnerModuleBuilder = AssemblyBuilder.DefineDynamicModule(AssemblyFileName, AssemblyFileName);
#else // #if DEBUG
                AssemblyBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName("DynamicTypes"), AssemblyBuilderAccess.Run);
                InnerModuleBuilder = AssemblyBuilder.DefineDynamicModule(AssemblyFileName);
#endif // #if DEBUG
            }

#if DEBUG
            ~Cache()
            {
                AssemblyBuilder.Save(AssemblyFileName);
            }
#endif // #if DEBUG
        }
    }
#endif // #if !FULLAOT && !PCL
}
