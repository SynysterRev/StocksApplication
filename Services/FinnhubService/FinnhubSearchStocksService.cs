using ServiceContracts;
using RepositoryContracts;
using Exceptions;
using ServiceContracts.FinnhubService;

namespace Services
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            try
            {
                return await _finnhubRepository.SearchStocks(stockSymbolToSearch);

            }
            catch (Exception ex)
            {
                throw new FinnhubException("Error fetching stock price quote", ex);
            }
        }
    }
}
