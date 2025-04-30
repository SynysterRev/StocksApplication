using ServiceContracts;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using Exceptions;

namespace Services
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            try
            {
                return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Error fetching stock price quote", ex);
            }
        }
    }
}
