using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using StockApp.Models;
using System.Globalization;

namespace StockApp.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubService _finnhubService;

        public SelectedStockViewComponent(IFinnhubService finnhubService) => _finnhubService = finnhubService;

        public async Task<IViewComponentResult> InvokeAsync(string stockSymbol)
        {
            Dictionary<string, object>? company = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stock = await _finnhubService.GetStockPriceQuote(stockSymbol);

            if (company != null && stock != null)
            {
                ViewBag.Image = company["logo"];
                ViewBag.StockSymbol = stockSymbol;
                ViewBag.StockName = company["name"];
                ViewBag.Industry = company["finnhubIndustry"];
                ViewBag.Exchange = company["exchange"];
                ViewBag.Price = Convert.ToDouble(stock["c"].ToString(), CultureInfo.InvariantCulture);
            }
            
            return View();
        }
    }
}
