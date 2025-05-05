namespace StockApp.Core.ServiceContracts.FinnhubService
{
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
        /// Gets the company profile for a given stock symbol.
        /// </summary>
        /// <param name="stockSymbol">The stock symbol to look for</param>
        /// <returns>A dictionary containing the APi response</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}
