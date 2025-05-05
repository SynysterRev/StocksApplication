namespace StockApp.Core.ServiceContracts.FinnhubService
{
    public interface IFinnhubSearchStocksService
    {
        /// <summary>
        /// Searches for stocks based on a given stock symbol.
        /// </summary>
        /// <param name="stockSymbolToSearch">The stock symbol to look for</param>
        /// <returns>A dictionary containing the stock data</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
