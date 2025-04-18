using Entities;
using ServiceContracts.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; } = "";

        [Required]
        public string StockName { get; set; } = "";

        [MaxDateValidator("2000-01-01")]
        public DateTime DateTimeOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateTimeOrder = DateTimeOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
