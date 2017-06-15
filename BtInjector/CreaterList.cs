using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace BtInjector
{
    class CreaterList
    {
        ConcurrentDictionary<Type, ICreaterStore> _createrList = new ConcurrentDictionary<Type, ICreaterStore>();

        public void Add<T>(ICreater<T> creater)
        {
            var retType = creater.ReturnType;
            _createrList.AddOrUpdate(retType, x => new CreaterStore<T>(creater), (x, y) =>
            {
                if (y.Creater.DeclareMode == DeclareMode.Implicit)
                {
                    return new CreaterStore<T>(creater);
                }
                throw new ArgumentException(string.Format("既に同じ型が登録されています[{0}]", retType));
            });
        }

        public void Add(ICreaterStore creater)
        {
            var retType = creater.Creater.ReturnType;
            _createrList.AddOrUpdate(retType, creater, (x, y) =>
            {
                if (y.Creater.DeclareMode == DeclareMode.Implicit)
                {
                    return creater;
                }
                throw new ArgumentException(string.Format("既に同じ型が登録されています[{0}]", retType));
            });
        }

        public bool ContainsKey(Type type)
        {
            return _createrList.ContainsKey(type);
        }

        public ICollection<Type> Keys
        {
            get { return _createrList.Keys; }
        }

        public ICreaterStore this[Type type]
        {
            get { return _createrList[type]; }
        }
    }
}