using Project.Api.ViewModels.CryptoCurrencyViewModels;

namespace Project.Api.Contracts;

public interface IGenericHttpService
{
    Task<ResultStatus> GetAsync<T>(
        string url);

    Task<ResultStatus> SendAsync<T>(
        HttpMethod httpMethod,
        string url,
        object obj);
}
