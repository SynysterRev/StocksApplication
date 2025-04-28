using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Net.Http;
using FluentAssertions;

namespace StockAppTests
{
    public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Index_ShouldReturnView()
        {
            HttpResponseMessage response = await _client.GetAsync("/Trade/Index/MSFT");

            response.IsSuccessStatusCode.Should().BeTrue();

            string responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseBody);
            var document = html.DocumentNode;

            document.QuerySelector("#new-order-panel").Should().NotBeNull();
            document.QuerySelector("#new-order-panel").InnerHtml.Should().Contain("New Order");
        }

        //add more test for others actions
    }
}
