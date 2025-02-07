using Newtonsoft.Json;
using Project.Api.Contracts;
using Project.Api.ViewModels.CryptoCurrencyViewModels;

namespace Project.Api.Services;

public class GenericHttpService : IGenericHttpService
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient _httpClient;

    public GenericHttpService(
        IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _httpClient = _clientFactory.CreateClient("CoinMarketCap");
    }

    // Other HttpRequests like POST, UPDATE, DELETE could be implemented

    public async Task<ResultStatus> GetAsync<T>(
        string url)
    {
        var result = new ResultStatus(false, Status.ServerError, "", "info");

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Success.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }

    public async Task<ResultStatus> SendAsync<T>(
        HttpMethod httpMethod,
        string url,
        object obj)
    {
        var result = new ResultStatus(false, Status.ServerError, "", "info");

        var httpRequest = new HttpRequestMessage(httpMethod, url)
        {
            Content = JsonContent.Create(obj)
        };

        HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Success.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }
}
