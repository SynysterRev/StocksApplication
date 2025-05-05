using StockApp.Core.Domain.RepositoryContracts;
using StockApp.Core.Exceptions;
using StockApp.Core.ServiceContracts.FinnhubService;

namespace StockApp.Core.Services.FinnhubService
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                return await _finnhubRepository.GetStocks();

            }
            catch (Exception ex)
            {
                throw new FinnhubException("Error fetching stock price quote", ex);
            }
        }
    }
}
