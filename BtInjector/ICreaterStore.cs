namespace BtInjector
{
    interface ICreaterStore
    {
        IGenerator GetGenerator();
        ICreater Creater { get; }
        ICreaterStore GetFuncCreater();
        ICreaterStore GetLazyCreater();
    }
}