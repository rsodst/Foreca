using Foreca.App.Services.WeatherReader;
using Foreca.Infrastructure.Database.Database;
using Foreca.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foreca.App.Features;

public class GetWeatherForecast : IAutoRegisteredService
{
    private readonly ForecaContext _forecaContext;
    private readonly IWeatherReader _weatherReader;

    public GetWeatherForecast(ForecaContext forecaContext, IWeatherReader weatherReader)
    {
        _forecaContext = forecaContext;
        _weatherReader = weatherReader;
    }

    public async Task<IEnumerable<WeatherForecastModel>> HandleAsync(int cityId, int forecastDays)
    {
        var cityDetails = await _forecaContext.CityDetails.FirstOrDefaultAsync(p => p.Id == cityId);

        if (cityDetails != null)
        {
            return await _weatherReader.GetForecast(cityDetails.Latitude, cityDetails.Longitude, forecastDays);
        }

        return null;
    }
}