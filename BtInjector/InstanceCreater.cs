namespace BtInjector
{
    class InstanceCreater<T> : ICreater<T>
    {
        T _instance;

        DeclareMode _mode;
        public InstanceCreater(T instance, DeclareMode mode)
        {
            _instance = instance;
            _mode = mode;
        }

        public Lifecycle Lifecycle
        {
            get { return Lifecycle.Singleton; }
        }

        public System.Type ReturnType
        {
            get { return typeof(T); }
        }

        public DeclareMode DeclareMode
        {
            get { return _mode; }
        }

        public IBuilder<T> GetBuilder()
        {
            return new InstanceBuilder<T>(_instance);
        }
    }
}