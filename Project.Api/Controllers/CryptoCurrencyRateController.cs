using Microsoft.AspNetCore.Mvc;
using Project.Api.Contracts;
using Project.Api.ViewModels.CryptoCurrencyViewModels.ApiCallResponse;

namespace Project.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CryptoCurrencyRateController : ControllerBase
{
    private readonly IGenericHttpService _genericHttpService;
    private readonly ILogger<CryptoCurrencyRateController> _logger;

    public CryptoCurrencyRateController(
        IGenericHttpService genericHttpService,
        ILogger<CryptoCurrencyRateController> logger)
    {
        _genericHttpService = genericHttpService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CryptoCurrencyRate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CryptoCurrencyRate>> GetAsync(string id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _genericHttpService.GetAsync<CryptoCurrencyRate>($"v2/cryptocurrency/quotes/latest?convert=USD,EUR,BRL,GBP,AUD&id={id}");

            if (result.IsSuccess)
            {
                return Ok((CryptoCurrencyRate)result.Data!);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new Exception(e.Message);
        }
    }
}
