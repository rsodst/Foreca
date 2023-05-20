using Foreca.App.Services.WeatherReader;
using Foreca.Infrastructure.Database.Database;
using Foreca.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foreca.App.Features;

public class GetCurrentWeatherHandler : IAutoRegisteredService
{
    private readonly ForecaContext _forecaContext;
    private readonly IWeatherReader _weatherReader;

    public GetCurrentWeatherHandler(ForecaContext forecaContext, IWeatherReader weatherReader)
    {
        _forecaContext = forecaContext;
        _weatherReader = weatherReader;
    }

    public async Task<WeatherDetailsModel> HandleAsync(int cityId)
    {
        var cityDetails = await _forecaContext.CityDetails.FirstOrDefaultAsync(p => p.Id == cityId);

        if (cityDetails != null)
        {
            return await _weatherReader.GetCurrent(cityDetails.Latitude, cityDetails.Longitude);
        }

        return null;
    }
}