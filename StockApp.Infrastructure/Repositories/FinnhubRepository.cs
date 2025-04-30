using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;

namespace Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public FinnhubRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                if (string.IsNullOrEmpty(stockSymbol))
                {
                    throw new ArgumentNullException(nameof(stockSymbol), "Stock symbol cannot be null or empty.");
                }
                string? token = _configuration.GetSection("finnhubtoken").Value;
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
                }
                string url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}";

                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);

                StreamReader streamReader = new StreamReader(responseMessage.Content.ReadAsStream());
                string content = streamReader.ReadToEnd();

                Dictionary<string, object>? dicContent = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                if (dicContent == null)
                    throw new InvalidOperationException("No response from server");

                if (dicContent.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dicContent["error"]));

                return dicContent;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                if (string.IsNullOrEmpty(stockSymbol))
                {
                    throw new ArgumentNullException(nameof(stockSymbol), "Stock symbol cannot be null or empty.");
                }
                string? token = _configuration["finnhubtoken"];

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
                }
                string url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}";

                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);

                StreamReader streamReader = new StreamReader(responseMessage.Content.ReadAsStream());
                string content = streamReader.ReadToEnd();

                Dictionary<string, object>? dicContent = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                if (dicContent == null)
                    throw new InvalidOperationException("No response from server");

                if (dicContent.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dicContent["error"]));

                return dicContent;
            }
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                string? token = _configuration["finnhubtoken"];

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
                }
                string url = $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={token}";

                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);

                StreamReader streamReader = new StreamReader(responseMessage.Content.ReadAsStream());
                string content = streamReader.ReadToEnd();

                List<Dictionary<string, string>>? listContent = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(content);

                if (listContent == null)
                    throw new InvalidOperationException("No response from server");

                foreach(var item in listContent)
                {
                   if (item.ContainsKey("error"))
                        throw new InvalidOperationException(Convert.ToString(item["error"]));
                }

                return listContent;
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                if (string.IsNullOrEmpty(stockSymbolToSearch))
                {
                    throw new ArgumentNullException(nameof(stockSymbolToSearch), "Stock symbol cannot be null or empty.");
                }
                string? token = _configuration["finnhubtoken"];

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");
                }
                string url = $"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={token}";

                HttpResponseMessage responseMessage = await httpClient.GetAsync(url);

                StreamReader streamReader = new StreamReader(responseMessage.Content.ReadAsStream());
                string content = streamReader.ReadToEnd();

                Dictionary<string, object>? dicContent = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                if (dicContent == null)
                    throw new InvalidOperationException("No response from server");

                if (dicContent.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dicContent["error"]));

                return dicContent;
            }
        }
    }
}
