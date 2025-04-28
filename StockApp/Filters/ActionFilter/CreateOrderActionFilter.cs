using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StockApp.Controllers;
using StockApp.Models;

namespace StockApp.Filters.ActionFilter
{
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;

                if (orderRequest != null)
                {
                    orderRequest!.DateAndTimeOfOrder = DateTime.Now;

                    tradeController.ModelState.Clear();
                    tradeController.TryValidateModel(orderRequest!);
                    if (!tradeController.ModelState.IsValid)
                    {
                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).ToList().Select(e => e.ErrorMessage).ToList();
                        StockTrade stockTrade = new StockTrade()
                        {
                            StockSymbol = orderRequest!.StockSymbol,
                            StockName = orderRequest.StockName,
                            Price = orderRequest.Price,
                        };
                        tradeController.ViewBag.CurrentPage = "Trade";

                        context.Result = tradeController.View("Index", stockTrade);
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
