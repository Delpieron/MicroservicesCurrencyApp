using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebMicroService.ViewModels;

namespace WebMicroService.Pages
{
    public class IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ILogger<IndexModel> _logger = logger;

        public List<CryptoCoin> Cryptos { get; set; } = [];

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-access-token", "coinrankingafa8710e1cfaacc7903413e17b674de298ddfec018b9a66f"); // Wstaw swoj¹ API Key

                var response = await client.GetAsync("https://api.coinranking.com/v2/coins?limit=3");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();


                var apiResponse = JsonSerializer.Deserialize<CryptoApiResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse?.Data?.Coins != null)
                {
                    Cryptos = apiResponse.Data.Coins;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"B³¹d pobierania danych: {ex.Message}");
            }
        }
    }
    
}
