using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
#if NETSTANDARD1_1
using System.Reflection;
#endif

namespace BtInjector
{
    public class Container
    {
        CreaterList _createrList = new CreaterList();
        ConcurrentDictionary<Type, ICreaterStore> _createrDictionary = new ConcurrentDictionary<Type, ICreaterStore>();
        ConcurrentDictionary<Type, object> _delegateDictionary = new ConcurrentDictionary<Type, object>();

        public void Register<TInterface, TService>(Lifecycle lifecycle = Lifecycle.Transient) where TService : TInterface
        {
            For<TInterface>().As<TService>(lifecycle);
        }

        internal void AddCreater<T>(ICreater<T> creater)
        {
            _createrList.Add(creater);
            _createrDictionary.AddOrUpdate(creater.ReturnType, _createrList[creater.ReturnType], _updateCreaterFunc);
        }

        public void Register<TInterface>(TInterface instance)
        {
            For<TInterface>().As(instance);
        }

        public IRegister<T> For<T>()
        {
            return new Register<T>(this);
        }

        ICreaterStore GetCreater(Type type)
        {
            return _createrDictionary.GetOrAdd(type, _getCreaterFunc);
        }

        Func<Type, object> _getDelegateFunc;
        Func<Type, ICreaterStore> _getCreaterFunc;
        Func<Type, ICreaterStore, ICreaterStore> _updateCreaterFunc;
        public Container()
        {
            _getDelegateFunc = x => GetCreater(x).GetGenerator().GetInstanceMethod();
            _getCreaterFunc = x =>
            {
#if NETSTANDARD1_1
                var ti = x.GetTypeInfo();
                
                // サブタイプが登録されているか検索
                var subtypes = _createrList.Keys.Where(y => y.GetTypeInfo().IsSubclassOf(x)).ToList();
#else
                var subtypes = _createrList.Keys.Where(y => y.IsSubclassOf(x)).ToList();
#endif
                if (subtypes.Count > 1)
                {
                    throw new ArgumentException(string.Format("型が一意に決まりません[{0}]", x));
                }
                if (subtypes.Count == 1)
                {
                    return _createrList[subtypes[0]];
                }

#if NETSTANDARD1_1
                if (ti.IsGenericType)
#else
                if (x.IsGenericType)
#endif
                {
                    var gd = x.GetGenericTypeDefinition();

                    // 欲しいインスタンスがFunc<T> である場合
                    if (gd == typeof(Func<>))
                    {
#if NETSTANDARD1_1
                        var ga = ti.GenericTypeArguments[0];
#else
                        var ga = x.GetGenericArguments()[0];
#endif
                        var baseCreater = GetCreater(ga);
                        var funcCreater = baseCreater.GetFuncCreater();
                        _createrList.Add(funcCreater);
                        return _createrList[x];
                    }

                    // 欲しいインスタンスがLazy<T> である場合
                    if (gd == typeof(Lazy<>))
                    {
#if NETSTANDARD1_1
                        var ga = ti.GenericTypeArguments[0];
#else
                        var ga = x.GetGenericArguments()[0];
#endif
                        var baseCreater = GetCreater(ga);
                        var lazyCreater = baseCreater.GetLazyCreater();
                        _createrList.Add(lazyCreater);
                        return _createrList[x];
                    }
                }

                // 抽象型・インターフェース・ジェネリック定義型でない場合
#if NETSTANDARD1_1
                if (!ti.IsAbstract && !ti.IsInterface && !ti.IsGenericTypeDefinition)
#else
                if (!x.IsAbstract && !x.IsInterface && !x.IsGenericTypeDefinition)
#endif
                {
                    var creater = ConstructorCreater.CreateStore(x, DeclareMode.Explicit, Lifecycle.Transient, this);
                    _createrList.Add(creater);
                    return _createrList[x];
                }
                    throw new KeyNotFoundException(String.Format("指定されたキーはディレクトリ内に存在しませんでした。[{0}]", x.FullName));
            };
            _updateCreaterFunc = (x, y) => _createrList[x];
        }

        internal Func<object> GetDelegate(Type type)
        {
            return _delegateDictionary.GetOrAdd(type, _getDelegateFunc) as Func<object>;
        }

        internal Func<T> GetDelegate<T>()
        {
            return _delegateDictionary.GetOrAdd(typeof(T), _getDelegateFunc) as Func<T>;
        }

        public object GetInstance(Type type)
        {
            return GetDelegate(type)();
        }

        public T GetInstance<T>()
        {
            return GetDelegate<T>()();
        }
    }
}