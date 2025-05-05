using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockApp.Core.ServiceContracts.FinnhubService;
using StockApp.UI.Models;

namespace StockApp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class StocksController : Controller
    {
        private readonly IFinnhubStocksService _finnhubStocksService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        public StocksController(IFinnhubStocksService finnhubStocksService, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubStocksService = finnhubStocksService;
            _tradingOptions = tradingOptions;
        }

        [HttpGet]
        [Route("/")]
        [Route("[action]")]
        [Route("[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock)
        {
            List<Dictionary<string, string>>? stocksListDic = await _finnhubStocksService.GetStocks();
            if (stocksListDic == null)
            {
                return StatusCode(500, "Can't access finnhub servers");
            }
            List<Stock> stocks = stocksListDic.Select(x => new Stock() { StockSymbol = x["displaySymbol"], StockName = x["description"] }).ToList();
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
