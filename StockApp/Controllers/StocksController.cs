using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockApp.Models;
using System.Linq;

namespace StockApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
        }

        [HttpGet]
        [Route("/")]
        [Route("[action]")]
        [Route("[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock)
        {
            List<Dictionary<string, string>>? stocksListDic = await _finnhubService.GetStocks();
            if (stocksListDic == null)
            {
                return StatusCode(500, "Can't access finnhub servers");
            }
            List<Stock> stocks = new List<Stock>();
            stocks = stocksListDic.Select(x => new Stock() { StockSymbol = x["displaySymbol"], StockName = x["description"] }).ToList();
            if (!string.IsNullOrEmpty(_tradingOptions.Value.Top25PopularStocks))
            {
                string[] defaultStockSymbol = _tradingOptions.Value.Top25PopularStocks.Split(',');
                stocks = stocks.Select(x => x).Where(x => defaultStockSymbol.Contains(x.StockSymbol)).ToList();
            }
            ViewBag.Stock = stock;
            ViewBag.CurrentPage = "Explore";
            return View(stocks);
        }
    }
}
