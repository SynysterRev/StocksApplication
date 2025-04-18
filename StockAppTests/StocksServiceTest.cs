using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace StockAppTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;

        private readonly IFixture _fixture;

        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _stocksService = new StocksService(_stocksRepositoryMock.Object);
            _testOutputHelper = testOutputHelper;
        }

        #region CreateBuyOrder
        //When supply a null BuyOrderRequest, it should throw an ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrderRequest()
        {
            BuyOrderRequest? buyOrderRequest = null;
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When supply BuyOrderRequest with less quantity than min required (0), it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_QuantityZero()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(x => x.Quantity, (uint)0)
                .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with too much quantity (more than 100000), it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_QuantityTooHigh()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                 .With(x => x.Quantity, (uint)100001)
                 .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with a price too low (less than 1), it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_PriceTooLow()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                 .With(x => x.Price, 0f)
                 .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with a price too high (more than 100000), it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_PriceTooHigh()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                 .With(x => x.Price, 100001f)
                 .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with a null stockSymbol, it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_NoStockSymbol()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                 .With(x => x.StockSymbol, null as string)
                 .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with a null stockSymbol, it should throw an ValidationException
        [Fact]
        public async Task CreateBuyOrder_OlderDateThanAuthorized()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(x => x.DateTimeOrder, DateTime.Parse("1999-12-31"))
                .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply BuyOrderRequest with correct values, it should return a BuyOrderResponse with auto-generated ID
        [Fact]
        public async Task CreateBuyOrder_CorrectValues()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Create<BuyOrderRequest>();
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            BuyOrderResponse buyOrderResponseExpected = buyOrder.ToBuyOrderResponse();

            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrderResponseExpected.BuyOrderID = buyOrderResponse.BuyOrderID;

            buyOrderResponse.Should().NotBeNull();
            buyOrderResponse.BuyOrderID.Should().NotBe(Guid.Empty);
            buyOrderResponse.Should().BeEquivalentTo(buyOrderResponseExpected);
        }
        #endregion

        #region CreateSellOrder
        //When supply a null SellOrderRequest, it should throw an ArgumentNullException
        [Fact]
        public async Task CreateSellOrder_NullBuyOrderRequest()
        {
            SellOrderRequest? sellOrderRequest = null;

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When supply SellOrderRequest with less quantity than min required (0), it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_QuantityZero()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(x => x.Quantity, (uint)0)
                .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply SellOrderRequest with too much quantity (more than 100000), it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_QuantityTooHigh()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
               .With(x => x.Quantity, (uint)100001)
               .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }
        //When supply SellOrderRequest with a price too low (less than 1), it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_PriceTooLow()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
               .With(x => x.Price, 0f)
               .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply SellOrderRequest with a price too high (more than 100000), it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_PriceTooHigh()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
              .With(x => x.Quantity, 100001f)
              .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply SellOrderRequest with a null stockSymbol, it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_NoStockSymbol()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
               .With(x => x.StockSymbol, null as string)
               .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply SellOrderRequest with a null stockSymbol, it should throw an ValidationException
        [Fact]
        public async Task CreateSellOrder_OlderDateThanAuthorized()
        {
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
               .With(x => x.DateTimeOrder, DateTime.Parse("1999-12-31"))
               .Create();

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        //When supply SellOrderRequest with correct values, it should return a SellOrderResponse with auto-generated ID
        [Fact]
        public async void CreateSellOrder_CorrectValues()
        {
            SellOrderRequest sellOrderRequest = _fixture.Create<SellOrderRequest>();
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            SellOrderResponse sellOrderResponseExpected = sellOrder.ToSellOrderResponse();

            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponseExpected.SellOrderID = sellOrderResponse.SellOrderID;

            sellOrderResponse.Should().NotBeNull();
            sellOrderResponse.SellOrderID.Should().NotBe(Guid.Empty);
            sellOrderResponse.Should().BeEquivalentTo(sellOrderResponseExpected);
        }
        #endregion

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