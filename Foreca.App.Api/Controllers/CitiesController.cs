using System.Net;
using Foreca.App.Features;
using Foreca.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foreca.App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CitiesController : ControllerBase
{
    private readonly AddNotExistedCityHandler _addNotExistedCityHandler;
    private readonly GetCitiesHandler _getCitiesHandler;

    public CitiesController(GetCitiesHandler getCitiesHandler, AddNotExistedCityHandler addNotExistedCityHandler)
    {
        _getCitiesHandler = getCitiesHandler;
        _addNotExistedCityHandler = addNotExistedCityHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CityDetailsModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCities(string city)
    {
        var result = await _getCitiesHandler.Handle(city);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CityDetailsModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(string city)
    {
        var cityDetails = await _addNotExistedCityHandler.Handle(city);

        return Ok(cityDetails);
    }
}