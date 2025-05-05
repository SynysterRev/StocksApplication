using Microsoft.AspNetCore.Mvc;
using StockApp.Core.ServiceContracts.FinnhubService;
using System.Globalization;

namespace StockApp.UI.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceService;

        public SelectedStockViewComponent(IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService)
        {
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceService = finnhubStockPriceQuoteService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string stockSymbol)
        {
            Dictionary<string, object>? company = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stock = await _finnhubStockPriceService.GetStockPriceQuote(stockSymbol);

            if (company != null && stock != null)
            {              
                ViewBag.Image = company.ContainsKey("logo") ? company["logo"] : "";
                ViewBag.StockSymbol = stockSymbol;
                ViewBag.StockName = company.ContainsKey("name") ? company["name"] : "";
                ViewBag.Industry = company.ContainsKey("finnhubIndustry") ? company["finnhubIndustry"] : "";
                ViewBag.Exchange = company.ContainsKey("exchange") ? company["exchange"] : "";
                ViewBag.Price = Convert.ToDouble(stock["c"].ToString(), CultureInfo.InvariantCulture);
            }

            return View();
        }
    }
}
