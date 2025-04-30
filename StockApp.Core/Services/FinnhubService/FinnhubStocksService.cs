using ServiceContracts;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using Exceptions;

namespace Services
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
