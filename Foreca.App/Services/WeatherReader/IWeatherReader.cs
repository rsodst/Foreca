namespace Foreca.App.Services.WeatherReader;

public interface IWeatherReader
{
    Task<WeatherDetailsModel> GetCurrent(float lat, float lon);

    Task<IEnumerable<WeatherForecastModel>> GetForecast(float lat, float lon, int forecastDays);
}