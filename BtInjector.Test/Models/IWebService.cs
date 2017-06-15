
namespace BtInjector.Test.Models
{
    public interface IWebService
    {
        IAuthenticator Authenticator { get; }
        IStockQuote StockQuote { get; }
        void Execute();
    }
}