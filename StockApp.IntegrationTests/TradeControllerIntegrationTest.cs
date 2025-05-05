using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;

namespace StockApp.IntegrationTests
{
    public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication(defaultScheme: "TestScheme")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                "TestScheme", options => { });
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });
            _client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue(scheme: "TestScheme");
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

        [Fact]
        public async Task Index_ShouldReturnRedirect()
        {
            HttpResponseMessage response = await _client.GetAsync("/Trade/Index/MSFT");

            response.StatusCode.Should().Be(HttpStatusCode.Redirect);

            //string responseBody = await response.Content.ReadAsStringAsync();

            //HtmlDocument html = new HtmlDocument();
            //html.LoadHtml(responseBody);
            //var document = html.DocumentNode;

            //document.QuerySelector("#new-order-panel").Should().NotBeNull();
            //document.QuerySelector("#new-order-panel").InnerHtml.Should().Contain("New Order");
        }

        //add more test for others actions
    }
}
