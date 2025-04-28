using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using StockApp.Filters.ActionFilter;
using StockApp.Models;
using System.Globalization;

namespace StockApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IStocksService _stocksService;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService,
            IStocksService stocksService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions;
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }
            string? token = _configuration["finnhubtoken"];
            if (string.IsNullOrEmpty(token))
            {
                return Content("The token is not set");
            }
            Dictionary<string, object>? companyProfileDic = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stockDic = await _finnhubService.GetStockPriceQuote(stockSymbol);

            if (companyProfileDic == null || stockDic == null)
            {
                return StatusCode(500, "Can't access finnhub servers");
            }

            if (companyProfileDic.ContainsKey("error"))
            {
                return StatusCode(500, Convert.ToString(companyProfileDic["error"]));
            }

            if (stockDic.ContainsKey("error"))
            {
                return StatusCode(500, Convert.ToString(stockDic["error"]));
            }

            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = stockSymbol,
                StockName = companyProfileDic["name"].ToString(),
                Price = Convert.ToDouble(stockDic["c"].ToString(), CultureInfo.InvariantCulture),
            };

            ViewBag.StockSymbol = stockSymbol;
            ViewBag.Token = token;
            ViewBag.DefaultQuantity = _tradingOptions.Value.DefaultOrderQuantity;
            ViewBag.CurrentPage = "Trade";

            return View(stockTrade);
        }

        [HttpPost]
        [Route("[action]")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(orderRequest);

            return RedirectToAction("Orders");
        }

        [HttpPost]
        [Route("[action]")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(orderRequest);

            return RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };
            ViewBag.CurrentPage = "Orders";

            return View(orders);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 20, 20, 20),
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
            };
        }
    }
}
