using Newtonsoft.Json;

namespace Project.Api.ViewModels.CryptoCurrencyViewModels.ApiCallResponse;

public class CryptoCurrencyRate
{
    [JsonProperty("status")]
    public Status Status { get; set; }

    [JsonProperty("data")]
    public Dictionary<string, CryptoCurrencyQuote> Data { get; set; }
}

public class Status
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("error_code")]
    public int ErrorCode { get; set; }

    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }

    [JsonProperty("elapsed")]
    public int Elapsed { get; set; }

    [JsonProperty("credit_count")]
    public int CreditCount { get; set; }

    [JsonProperty("notice")]
    public string Notice { get; set; }
}

public class CryptoCurrencyQuote
{

    [JsonProperty("quote")]
    public Dictionary<string, Quote> Quote { get; set; }
}

public class Quote
{
    [JsonProperty("price")]
    public decimal Price { get; set; }
}
