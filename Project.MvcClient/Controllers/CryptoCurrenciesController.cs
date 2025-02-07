using Microsoft.AspNetCore.Mvc;
using Project.MvcClient.Contracts;
using Project.MvcClient.Responses;
using Project.MvcClient.ViewModels.CryptoCurrencyViewModels;

namespace Project.MvcClient.Controllers;

[Route("[controller]/[action]")]
public class CryptoCurrenciesController : Controller
{
    private readonly ILogger<CryptoCurrenciesController> _logger;
    private readonly IGenericHttpService _genericHttpService;


    public CryptoCurrenciesController(
        ILogger<CryptoCurrenciesController> logger,
        IGenericHttpService genericHttpService)
    {
        _logger = logger;
        _genericHttpService = genericHttpService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        ResultStatus result = await _genericHttpService.GetAsync<CryptoCurrencyRate>("Api", "api/CryptoCurrencyRate/1");
        if (result.IsSuccess)
        {
            return View((CryptoCurrencyRate)result.Data!);
        }
        throw new Exception(result.Message);
    }
}
