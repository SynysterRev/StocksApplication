﻿using StockApp.Core.Domain.RepositoryContracts;
using StockApp.Core.Exceptions;
using StockApp.Core.ServiceContracts.FinnhubService;

namespace StockApp.Core.Services.FinnhubService
{
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        private readonly IFinnhubRepository _finnhubRepository;
        public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            try
            {
                return await _finnhubRepository.GetCompanyProfile(stockSymbol);
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Error fetching company profile", ex);
            }
        }
    }
}
