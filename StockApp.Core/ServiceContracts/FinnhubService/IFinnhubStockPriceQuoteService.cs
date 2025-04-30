namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubStockPriceQuoteService
    {
        /// <summary>
        /// Gets the stock price quote for a given stock symbol.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol to look for</param>
        /// <returns>A dictionary conttaining the API response</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
