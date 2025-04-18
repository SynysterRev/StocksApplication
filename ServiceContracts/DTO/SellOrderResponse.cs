using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; } = "";
        public string StockName { get; set; } = "";
        public DateTime DateTimeOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SellOrderResponse response &&
                   SellOrderID.Equals(response.SellOrderID) &&
                   StockSymbol == response.StockSymbol &&
                   StockName == response.StockName &&
                   DateTimeOrder == response.DateTimeOrder &&
                   Quantity == response.Quantity &&
                   Price == response.Price &&
                   TradeAmount == response.TradeAmount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SellOrderID, StockSymbol, StockName, DateTimeOrder, Quantity, Price, TradeAmount);
        }

        public override string ToString()
        {
            return $"SellOrderID: {SellOrderID}, StockSymbol: {StockSymbol}, " +
                $"StockName: {StockName}, DateTimeOrder: {DateTimeOrder}, " +
                $"Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateTimeOrder = sellOrder.DateTimeOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }
}
