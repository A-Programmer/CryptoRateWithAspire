using Newtonsoft.Json;
using Project.MvcClient.Contracts;
using Project.MvcClient.Responses;
using System.Net.Http.Headers;

namespace Project.MvcClient.Services;

public class GenericHttpService : IGenericHttpService
{
    private readonly IHttpClientFactory _clientFactory;
    private HttpClient _httpClient;
    private readonly ILogger<GenericHttpService> _logger;

    public GenericHttpService(
        IHttpClientFactory clientFactory,
        ILogger<GenericHttpService> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }

    private async Task CreateClient(string clientName)
    {
        _httpClient = _clientFactory.CreateClient(clientName);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ResultStatus> PostAsync<T>(
        string clientName,
        string url,
        HttpContent content)
    {
        await CreateClient(clientName);

        var result = new ResultStatus(false, Status.ServerError, "", "info");

        HttpResponseMessage response = await _httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Successfully added.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }

    public async Task<ResultStatus> GetAsync<T>(
        string clientName,
        string url)
    {
        await CreateClient(clientName);

        var result = new ResultStatus(false, Status.ServerError, "", "info");

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Found the record.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }

    public async Task<ResultStatus> DeleteAsync<T>(
        string clientName,
        string url)
    {
        await CreateClient(clientName);

        var result = new ResultStatus(false, Status.ServerError, "", "info");

        HttpResponseMessage response = await _httpClient.DeleteAsync(url);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Successfully Deleted.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }

    public async Task<ResultStatus> UpdateAsync<T>(
        string clientName,
        string url,
        HttpContent content)
    {
        await CreateClient(clientName);

        var result = new ResultStatus(false, Status.ServerError, "", "info");

        HttpResponseMessage response = await _httpClient.PutAsync(url, content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        try
        {
            var convertResult = JsonConvert.DeserializeObject<T>(responseContent);

            result.Update(true, Status.Success, "Successfully Updated.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }

    public async Task<ResultStatus> SendAsync<T>(
        string clientName,
        HttpMethod httpMethod,
        string url,
        object obj)
    {
        await CreateClient(clientName);

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

            result.Update(true, Status.Success, "Successfully Updated.", "success",
                convertResult);

        }
        catch (Exception e)
        {
            result.Update(false, Status.ServerError, e.Message, "danger");
        }

        return result;
    }
}