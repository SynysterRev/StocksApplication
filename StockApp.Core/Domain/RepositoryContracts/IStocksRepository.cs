using StockApp.Core.Domain.Entities;

namespace StockApp.Core.Domain.RepositoryContracts
{
    public interface IStocksRepository
    {
        /// <summary>
        /// Register a buy order in the database
        /// </summary>
        /// <param name="buyOrder">The buy order object</param>
        /// <returns>Returns the same BuyOrder object</returns>
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        /// <summary>
        /// Register a sell order in the database
        /// </summary>
        /// <param name="sellOrder">The sell order object</param>
        /// <returns>Returns the same SellOrder object</returns>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// Get a list of buy orders.
        /// </summary>
        /// <returns>Returns all the BuyOrder stocked as a list</returns>
        Task<List<BuyOrder>> GetBuyOrders();


        /// <summary>
        /// Get a list of sell orders.
        /// </summary>
        /// <returns>Returns all the SellOrder stocked as a list</returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}
