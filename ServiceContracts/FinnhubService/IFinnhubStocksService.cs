namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubStocksService
    {
        /// <summary>
        /// Gets a list of stocks.
        /// </summary>
        /// <returns>A list of dictionaries containing the stocks</returns>
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}
