using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; } = "";
        public string StockName { get; set; } = "";
        public DateTime DateTimeOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BuyOrderResponse response &&
                   BuyOrderID.Equals(response.BuyOrderID) &&
                   StockSymbol == response.StockSymbol &&
                   StockName == response.StockName &&
                   DateTimeOrder == response.DateTimeOrder &&
                   Quantity == response.Quantity &&
                   Price == response.Price &&
                   TradeAmount == response.TradeAmount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BuyOrderID, StockSymbol, StockName, DateTimeOrder, Quantity, Price, TradeAmount);
        }

        public override string ToString()
        {
            return $"BuyOrderID: {BuyOrderID}, StockSymbol: {StockSymbol}, " +
                $"StockName: {StockName}, DateTimeOrder: {DateTimeOrder}, " +
                $"Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }
    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateTimeOrder = buyOrder.DateTimeOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price
            };
        }
    }
}
