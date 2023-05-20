using Foreca.App.ValueObjects;

namespace Foreca.App.Services.CityImage;

public interface ICityImageReader
{
    public Task<string> GetCityImageUrl(CityName cityName);
}