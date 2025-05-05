using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using StockApp.Core.Domain.RepositoryContracts;
using StockApp.Core.ServiceContracts.FinnhubService;
using StockApp.Core.Services.FinnhubService;
using StockApp.UI;
using StockApp.UI.Controllers;
using StockApp.UI.Models;

namespace StockApp.ControllerTests
{
    public class StocksControllerTest
    {
        private readonly IFinnhubStocksService _finnhubStocksService;
        private readonly Mock<IFinnhubRepository> _finnhubRepositoryMock;
        private readonly IFinnhubRepository _finnhubRepository;

        private readonly IFixture _fixture;

        public StocksControllerTest()
        {
            _finnhubRepositoryMock = new Mock<IFinnhubRepository>();
            _finnhubRepository = _finnhubRepositoryMock.Object;
            _finnhubStocksService = new FinnhubStocksService(_finnhubRepository);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Explore_ShouldReturnError505()
        {
            var tradingOptions = Options.Create(new TradingOptions
            {
                Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO",
                DefaultOrderQuantity = 100,
            });

            _finnhubRepositoryMock.Setup(temp => temp.GetStocks()).ReturnsAsync(null as List<Dictionary<string, string>>);
            StocksController stocksController = new StocksController(_finnhubStocksService, tradingOptions);

            IActionResult result = await stocksController.Explore(null);

            ObjectResult viewResult = result.Should().BeOfType<ObjectResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task Explore_ShouldReturnExploreViewWithStockList()
        {
            var tradingOptions = Options.Create(new TradingOptions
            {
                Top25PopularStocks = "AAPL,MSFT,GOOGL,",
                DefaultOrderQuantity = 100,
            });

            _finnhubRepositoryMock.Setup(temp => temp.GetStocks()).ReturnsAsync(new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "displaySymbol", "AAPL" }, { "description", "Apple Inc." } },
                new Dictionary<string, string> { { "displaySymbol", "MSFT" }, { "description", "Microsoft Corporation" } },
                new Dictionary<string, string> { { "displaySymbol", "GOOGL" }, { "description", "Alphabet Inc." } },
            });

            List<Stock> stocks = new List<Stock>
            {
                _fixture.Build<Stock>().With(x => x.StockSymbol, "AAPL").With(x => x.StockName, "Apple Inc.").Create(),
                _fixture.Build<Stock>().With(x => x.StockSymbol, "MSFT").With(x => x.StockName, "Microsoft Corporation").Create(),
                _fixture.Build<Stock>().With(x => x.StockSymbol, "GOOGL").With(x => x.StockName, "Alphabet Inc.").Create(),
            };
            StocksController stocksController = new StocksController(_finnhubStocksService, tradingOptions);

            IActionResult result = await stocksController.Explore(null);

            ViewResult viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.ViewData.Model.Should().BeAssignableTo<List<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(stocks);
            viewResult.ViewData["Stock"].Should().BeNull();
        }

        [Fact]
        public async Task Explore_ShouldReturnExploreViewWithStockListAndStockSymbol()
        {
            var tradingOptions = Options.Create(new TradingOptions
            {
                Top25PopularStocks = "AAPL,MSFT,GOOGL,",
                DefaultOrderQuantity = 100,
            });

            _finnhubRepositoryMock.Setup(temp => temp.GetStocks()).ReturnsAsync(new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "displaySymbol", "AAPL" }, { "description", "Apple Inc." } },
                new Dictionary<string, string> { { "displaySymbol", "MSFT" }, { "description", "Microsoft Corporation" } },
                new Dictionary<string, string> { { "displaySymbol", "GOOGL" }, { "description", "Alphabet Inc." } },
            });

            List<Stock> stocks = new List<Stock>
            {
                _fixture.Build<Stock>().With(x => x.StockSymbol, "AAPL").With(x => x.StockName, "Apple Inc.").Create(),
                _fixture.Build<Stock>().With(x => x.StockSymbol, "MSFT").With(x => x.StockName, "Microsoft Corporation").Create(),
                _fixture.Build<Stock>().With(x => x.StockSymbol, "GOOGL").With(x => x.StockName, "Alphabet Inc.").Create(),
            };
            StocksController stocksController = new StocksController(_finnhubStocksService, tradingOptions);

            IActionResult result = await stocksController.Explore("AAPL");

            ViewResult viewResult = result.Should().BeOfType<ViewResult>().Subject;

            viewResult.Should().NotBeNull();
            viewResult.ViewData.Model.Should().BeAssignableTo<List<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(stocks);
            viewResult.ViewData["Stock"].Should().Be("AAPL");
        }
    }
}
