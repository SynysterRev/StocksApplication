using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;

namespace StockAppTests
{
    public class StocksServiceTest
    {
        //private readonly IStocksService _stocksService;
        //private readonly ITestOutputHelper _testOutputHelper;

        //public StocksServiceTest(ITestOutputHelper testOutputHelper)
        //{
        //    _stocksService = new StocksService();
        //    _testOutputHelper = testOutputHelper;
        //}

        //private BuyOrderRequest GetBuyOrderRequest()
        //{
        //    return new BuyOrderRequest()
        //    {
        //        StockSymbol = "AAPL",
        //        StockName = "Apple Inc.",
        //        DateTimeOrder = DateTime.Now,
        //        Quantity = 10,
        //        Price = 150.00
        //    };
        //}

        //private SellOrderRequest GetSellOrderRequest()
        //{
        //    return new SellOrderRequest()
        //    {
        //        StockSymbol = "AAPL",
        //        StockName = "Apple Inc.",
        //        DateTimeOrder = DateTime.Now,
        //        Quantity = 10,
        //        Price = 150.00
        //    };
        //}

        //private List<BuyOrderRequest> GetMultipleBuyOrderRequest()
        //{
        //    return new List<BuyOrderRequest>()
        //    {
        //        new BuyOrderRequest() {
        //        StockSymbol = "AAPL",
        //        StockName = "Apple Inc.",
        //        DateTimeOrder = DateTime.Now,
        //        Quantity = 10,
        //        Price = 150.00
        //        },
        //        new BuyOrderRequest() {
        //            StockSymbol = "GOOGL",
        //            StockName = "Alphabet Inc.",
        //            DateTimeOrder = DateTime.Now,
        //            Price = 2800.00,
        //            Quantity = 5
        //        },
        //    };
        //}

        //private List<SellOrderRequest> GetMultipleSellOrderRequest()
        //{
        //    return new List<SellOrderRequest>()
        //    {
        //        new SellOrderRequest(){
        //        StockSymbol = "AAPL",
        //        StockName = "Apple Inc.",
        //        DateTimeOrder = DateTime.Now,
        //        Quantity = 10,
        //        Price = 150.00
        //        },
        //        new SellOrderRequest(){
        //            StockSymbol = "GOOGL",
        //            StockName = "Alphabet Inc.",
        //            DateTimeOrder = DateTime.Now,
        //            Price = 2800.00,
        //            Quantity = 5
        //        },

        //    };
        //}



        //#region CreateBuyOrder
        ////When supply a null BuyOrderRequest, it should throw an ArgumentNullException
        //[Fact]
        //public void CreateBuyOrder_NullBuyOrderRequest()
        //{
        //    BuyOrderRequest? buyOrderRequest = null;

        //    Assert.ThrowsAsync<ArgumentNullException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with less quantity than min required (0), it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_QuantityZero()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.Quantity = 0;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with too much quantity (more than 100000), it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_QuantityTooHigh()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.Quantity = 100001;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with a price too low (less than 1), it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_PriceTooLow()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.Price = 0;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with a price too high (more than 100000), it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_PriceTooHigh()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.Price = 100001;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with a null stockSymbol, it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_NoStockSymbol()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.StockSymbol = "";

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with a null stockSymbol, it should throw an ArgumentException
        //[Fact]
        //public void CreateBuyOrder_OlderDateThanAuthorized()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();
        //    buyOrderRequest.DateTimeOrder = DateTime.Parse("1999-12-31");

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateBuyOrder(buyOrderRequest);
        //    });
        //}

        ////When supply BuyOrderRequest with correct values, it should return a BuyOrderResponse with auto-generated ID
        //[Fact]
        //public async void CreateBuyOrder_CorrectValues()
        //{
        //    BuyOrderRequest buyOrderRequest = GetBuyOrderRequest();

        //    BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

        //    Assert.NotNull(buyOrderResponse);
        //    Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
        //}
        //#endregion

        //#region CreateSellOrder
        ////When supply a null SellOrderRequest, it should throw an ArgumentNullException
        //[Fact]
        //public void CreateSellOrder_NullBuyOrderRequest()
        //{
        //    SellOrderRequest? sellOrderRequest = null;

        //    Assert.ThrowsAsync<ArgumentNullException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with less quantity than min required (0), it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_QuantityZero()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.Quantity = 0;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with too much quantity (more than 100000), it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_QuantityTooHigh()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.Quantity = 100001;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with a price too low (less than 1), it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_PriceTooLow()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.Price = 0;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with a price too high (more than 100000), it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_PriceTooHigh()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.Price = 100001;

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with a null stockSymbol, it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_NoStockSymbol()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.StockSymbol = "";

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with a null stockSymbol, it should throw an ArgumentException
        //[Fact]
        //public void CreateSellOrder_OlderDateThanAuthorized()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();
        //    sellOrderRequest.DateTimeOrder = DateTime.Parse("1999-12-31");

        //    Assert.ThrowsAsync<ArgumentException>(async () =>
        //    {
        //        await _stocksService.CreateSellOrder(sellOrderRequest);
        //    });
        //}

        ////When supply SellOrderRequest with correct values, it should return a SellOrderResponse with auto-generated ID
        //[Fact]
        //public async void CreateSellOrder_CorrectValues()
        //{
        //    SellOrderRequest sellOrderRequest = GetSellOrderRequest();

        //    SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

        //    Assert.NotNull(sellOrderResponse);
        //    Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
        //}
        //#endregion

        //#region GetBuyOrders
        ////By default GetBuyOrders should return an empty list
        //[Fact]
        //public async void GetBuyOrders_EmptyList()
        //{
        //    List<BuyOrderResponse> buyOrders = await _stocksService.GetBuyOrders();
        //    Assert.NotNull(buyOrders);
        //    Assert.Empty(buyOrders);
        //}

        ////After adding a few orders, it should return a list with the added orders
        //[Fact]
        //public async void GetBuyOrders_AddFewOrders()
        //{
        //    List<BuyOrderRequest> dummyBuyOrdersRequest = GetMultipleBuyOrderRequest();

        //    List<BuyOrderResponse> buyOrderResponses = new List<BuyOrderResponse>();
        //    _testOutputHelper.WriteLine("Created buy orders: ");
        //    foreach (BuyOrderRequest order in dummyBuyOrdersRequest)
        //    {
        //        BuyOrderResponse buyResponse = await _stocksService.CreateBuyOrder(order);
        //        buyOrderResponses.Add(buyResponse);
        //        _testOutputHelper.WriteLine(buyResponse.ToString());
        //    }

        //    List<BuyOrderResponse> addedBuyOrders = await _stocksService.GetBuyOrders();

        //    Assert.NotNull(addedBuyOrders);
        //    Assert.NotEmpty(addedBuyOrders);

        //    _testOutputHelper.WriteLine("Added buy orders: ");
        //    foreach (BuyOrderResponse order in addedBuyOrders)
        //    {
        //        _testOutputHelper.WriteLine(order.ToString());
        //        Assert.Contains(order, addedBuyOrders);
        //    }
        //}
        //#endregion

        //#region GetSellOrders
        ////By default GetSellOrders should return an empty list
        //[Fact]
        //public async void GetSellOrders_EmptyList()
        //{
        //    List<SellOrderResponse> sellOrders = await _stocksService.GetSellOrders();
        //    Assert.NotNull(sellOrders);
        //    Assert.Empty(sellOrders);
        //}

        ////After adding a few orders, it should return a list with the added orders
        //[Fact]
        //public async void GetSellOrders_AddFewOrders()
        //{
        //    List<SellOrderRequest> dummyBuyOrdersRequest = GetMultipleSellOrderRequest();

        //    List<SellOrderResponse> sellOrderResponses = new List<SellOrderResponse>();
        //    _testOutputHelper.WriteLine("Created sell orders: ");
        //    foreach (SellOrderRequest order in dummyBuyOrdersRequest)
        //    {
        //        SellOrderResponse sellResponse = await _stocksService.CreateSellOrder(order);
        //        sellOrderResponses.Add(sellResponse);
        //        _testOutputHelper.WriteLine(sellResponse.ToString());
        //    }

        //    List<SellOrderResponse> addedSellOrders = await _stocksService.GetSellOrders();

        //    Assert.NotNull(addedSellOrders);
        //    Assert.NotEmpty(addedSellOrders);

        //    _testOutputHelper.WriteLine("Added sell orders: ");
        //    foreach (SellOrderResponse order in addedSellOrders)
        //    {
        //        _testOutputHelper.WriteLine(order.ToString());
        //        Assert.Contains(order, addedSellOrders);
        //    }
        //}
        //#endregion
    }
}