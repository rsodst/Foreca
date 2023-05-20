using System.Net;
using Foreca.App.Features;
using Foreca.App.Services.WeatherReader;
using Microsoft.AspNetCore.Mvc;

namespace Foreca.App.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly GetCurrentWeatherHandler _getCurrentWeatherHandler;
    private readonly GetWeatherForecast _getWeatherForecast;

    public WeatherController(GetCurrentWeatherHandler getCurrentWeatherHandler, GetWeatherForecast getWeatherForecast)
    {
        _getCurrentWeatherHandler = getCurrentWeatherHandler;
        _getWeatherForecast = getWeatherForecast;
    }

    [HttpGet("{cityId}/current")]
    [ProducesResponseType(typeof(WeatherDetailsModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCurrentWeather(int cityId)
    {
        return Ok(await _getCurrentWeatherHandler.HandleAsync(cityId));
    }

    [HttpGet("{cityId}/forecast/{forecastForDays}")]
    [ProducesResponseType(typeof(WeatherForecastModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetForecast(int cityId, int forecastForDays)
    {
        return Ok(await _getWeatherForecast.HandleAsync(cityId, forecastForDays));
    }
}