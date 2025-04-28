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
            TradeController tradeController = (TradeController)context.Controller;
            if (tradeController != null)
            {
                IDictionary<string, object?>? parameters = context.HttpContext.Items["arguments"] as IDictionary<string, object?>;
                if (parameters != null && parameters.ContainsKey("orderRequest"))
                {
                    var orderRequest = parameters["orderRequest"] as IOrderRequest;

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
