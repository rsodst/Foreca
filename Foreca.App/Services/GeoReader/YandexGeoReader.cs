using Foreca.Infrastructure.GeocoderProvider;
using Foreca.Shared;

namespace Foreca.App.Services.GeoReader;

public class YandexGeoReader : IGeoReader, IAutoRegisteredService<IGeoReader>
{
    private readonly YandexGeocoderProvider _geocoderProvider;

    public YandexGeoReader(YandexGeocoderProvider geocoderProvider)
    {
        _geocoderProvider = geocoderProvider;
    }

    public async Task<GeoReaderModel> GetAsync(string city)
    {
        var providerResponse = await _geocoderProvider.GetPointAsync(city);

        return new GeoReaderModel
        {
            Name = providerResponse.Name,
            Latitude = providerResponse.Latitude,
            Longitude = providerResponse.Longitude,
        };
    }
}