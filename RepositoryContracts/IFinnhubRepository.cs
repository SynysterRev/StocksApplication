namespace RepositoryContracts
{
    public interface IFinnhubRepository
    {
        /// <summary>
        /// Gets the company profile for a given stock symbol.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol to look for</param>
        /// <returns>A dictionary containing the APi response</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Gets the stock price quote for a given stock symbol.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol to look for</param>
        /// <returns>A dictionary containing the API response</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

        /// <summary>
        /// Gets a list of stocks.
        /// </summary>
        /// <returns>A list of dictionaries containing the stocks</returns>
        Task<List<Dictionary<string, string>>?> GetStocks();

        /// <summary>
        /// Searches for stocks based on a given stock symbol.
        /// </summary>
        /// <param name="stockSymbolToSearch">The stock symbol to look for</param>
        /// <returns>A dictionary containing the stock data</returns>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
