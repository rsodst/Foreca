using Foreca.Infrastructure.WeatherProvider;
using Foreca.Shared;

namespace Foreca.App.Services.WeatherReader;

public class YandexWeatherReader : IWeatherReader, IAutoRegisteredService<IWeatherReader>
{
    private readonly YandexWeatherProvider _weatherProvider;

    public YandexWeatherReader(YandexWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
    }

    public async Task<WeatherDetailsModel> GetCurrent(float lat, float lon)
    {
        var providerResponse = await _weatherProvider.GetWeatherDetails(lat, lon);

        return new WeatherDetailsModel
        {
            Temp = providerResponse.Current.Temp,
            FeelsLike = providerResponse.Current.FeelsLike,
            Condition = providerResponse.Current.Condition,
            WindSpeed = providerResponse.Current.WindSpeed,
            WindDirection = providerResponse.Current.WindDirection,
            Pressure = providerResponse.Current.PressureMM,
            Humidity = providerResponse.Current.Humidity,
        };
    }

    public async Task<IEnumerable<WeatherForecastModel>> GetForecast(float lat, float lon, int forecastDays)
    {
        var providerResponse = await _weatherProvider.GetWeatherDetails(lat, lon, forecastDays);

        return providerResponse.Forecast.Select(forecast => new WeatherForecastModel
        {
            Date = DateTime.Parse(forecast.Date),
            TempMax = forecast.Parts.Day.TempMax,
            TempMin = forecast.Parts.Night.TempMin,
            Condition = forecast.Parts.Day.Condition,
            WindSpeed = forecast.Parts.Day.WindSpeed,
            WindDirection = forecast.Parts.Day.WindDirection,
            Pressure = forecast.Parts.Day.PressureMM,
            Humidity = forecast.Parts.Day.Humidity,
        });
    }
}