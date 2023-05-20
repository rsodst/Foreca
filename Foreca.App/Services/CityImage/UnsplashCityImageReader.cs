using Foreca.App.ValueObjects;
using Foreca.Infrastructure.ImagesProvider;
using Foreca.Shared;

namespace Foreca.App.Services.CityImage;

public class UnsplashCityImageReader : ICityImageReader, IAutoRegisteredService<ICityImageReader>
{
    private readonly UnsplashProvider _unsplashProvider;

    public UnsplashCityImageReader(UnsplashProvider unsplashProvider)
    {
        _unsplashProvider = unsplashProvider;
    }

    public async Task<string> GetCityImageUrl(CityName cityName)
    {
        var image = await _unsplashProvider.GetImageByText(cityName);

        return image.Urls.Regular;
    }
}