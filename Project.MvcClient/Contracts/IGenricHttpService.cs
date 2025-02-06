using Project.MvcClient.Responses;

namespace Project.MvcClient.Contracts;

public interface IGenericHttpService
{
    Task<ResultStatus> PostAsync<T>(
        string clientName,
        string url,
        HttpContent content);

    Task<ResultStatus> GetAsync<T>(
        string clientName,
        string url);

    Task<ResultStatus> DeleteAsync<T>(
        string clientName,
        string url);

    Task<ResultStatus> UpdateAsync<T>(
        string clientName,
        string url,
        HttpContent content);

    Task<ResultStatus> SendAsync<T>(
        string clientName,
        HttpMethod httpMethod,
        string url,
        object obj);
}