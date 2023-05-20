using Foreca.Shared;

namespace Foreca.Infrastructure.GeocoderProvider;

public class YandexGeocoderOptions : IAutoRegisteredOptions
{
    public string BaseUrl { get; set; }

    public string ApiKey { get; set; }
}