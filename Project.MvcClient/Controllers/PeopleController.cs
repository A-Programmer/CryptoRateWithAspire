using Microsoft.AspNetCore.Mvc;
using Project.MvcClient.Contracts;
using Project.MvcClient.Responses;
using Project.MvcClient.ViewModels.PeopleViewModels;

namespace Project.MvcClient.Controllers;

public class PeopleController : Controller
{
    private readonly ILogger<PeopleController> _logger;
    private readonly IGenericHttpService _genericHttpService;


    public PeopleController(ILogger<PeopleController> logger,
        IGenericHttpService genericHttpService)
    {
        _logger = logger;
        _genericHttpService = genericHttpService;
    }

    public async Task<ActionResult<List<PersonItem>>> Index()
    {
        ResultStatus result = await _genericHttpService.GetAsync<IList<PersonItem>>("Api", "api/People");
        if (result.IsSuccess)
        {
            return View((List<PersonItem>)result.Data);
        }
        throw new Exception(result.Message);
    }

    [HttpGet("{id}"), ActionName("Details")]
    public async Task<ActionResult<PersonItem>> Details(Guid id)
    {
        ResultStatus result = await _genericHttpService.GetAsync<PersonItem>("Api", $"api/People/{id}");
        if (result.IsSuccess)
        {
            return View((PersonItem)result.Data);
        }
        throw new Exception(result.Message);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CreatePersonResponse>> CreateAsync(CreatePersonRequest personRequest)
    {
        HttpContent content = JsonContent.Create(personRequest);

        ResultStatus result = await _genericHttpService.PostAsync<CreatePersonResponse>("Api", "api/People", content);

        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        throw new Exception(result.Message);
    }

    [HttpGet, ActionName("Edit")]
    public async Task<IActionResult> UpdateAsync(Guid id)
    {
        ResultStatus result = await _genericHttpService.GetAsync<UpdatePerson>("Api", $"api/People/{id}");

        if (result.IsSuccess)
        {
            return View((UpdatePerson)result.Data);
        }
        throw new Exception(result.Message);
    }

    [HttpPost, ActionName("Edit")]
    public async Task<ActionResult<UpdatePerson>> UpdateAsync(UpdatePerson personRequest)
    {
        HttpContent content = JsonContent.Create(personRequest);

        ResultStatus result = await _genericHttpService.UpdateAsync<UpdatePerson>("Api", $"api/People/{personRequest.Id}", content);

        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        throw new Exception(result.Message);
    }

    [HttpGet, ActionName("Delete")]
    public async Task<ActionResult<PersonItem>> DeleteAsync(Guid id)
    {
        ResultStatus result = await _genericHttpService.GetAsync<PersonItem>("Api", $"api/People/{id}");

        if (result.IsSuccess)
        {
            return View((PersonItem)result.Data);
        }
        throw new Exception(result.Message);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<ActionResult<CreatePersonResponse>> PostDeleteAsync(Guid id)
    {
        ResultStatus result = await _genericHttpService.DeleteAsync<Guid>("Api", $"api/People/{id}");

        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        throw new Exception(result.Message);
    }
}
