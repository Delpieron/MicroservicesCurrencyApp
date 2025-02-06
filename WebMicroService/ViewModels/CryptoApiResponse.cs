namespace WebMicroService.ViewModels
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class CryptoApiResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [JsonPropertyName("data")]
        public required CryptoData Data { get; set; }
    }

    public class CryptoData
    {

        [JsonPropertyName("coins")]
        public required List<CryptoCoin> Coins { get; set; }
    }

    public class CryptoCoin
    {

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }
    }

}
