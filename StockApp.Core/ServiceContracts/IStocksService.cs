using StockApp.Core.DTO;

namespace StockApp.Core.ServiceContracts
{
    public interface IStocksService
    {
        /// <summary>
        /// Creates a buy order for a stock.
        /// </summary>
        /// <param name="buyOrderRequest">The buy order request object</param>
        /// <returns>Returns a BuyOrderResponse object</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Creates a sell order for a stock.
        /// </summary>
        /// <param name="sellOrderRequest">The sell order request object</param>
        /// <returns>Returns a SellOrderResponse object</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Get a list of buy orders.
        /// </summary>
        /// <returns>Returns all the BuyOrderResponse stocked as a list</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders();


        /// <summary>
        /// Get a list of sell orders.
        /// </summary>
        /// <returns>Returns all the SellOrderResponse stocked as a list</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
