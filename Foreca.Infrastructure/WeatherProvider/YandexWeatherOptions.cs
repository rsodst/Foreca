using Foreca.Shared;

namespace Foreca.Infrastructure.WeatherProvider;

public class YandexWeatherOptions : IAutoRegisteredOptions
{
    public string BaseUrl { get; set; }

    public string ApiKey { get; set; }
}