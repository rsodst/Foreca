using Foreca.App.Models;
using Foreca.App.Services.CityImage;
using Foreca.App.Services.GeoReader;
using Foreca.App.ValueObjects;
using Foreca.Domain;
using Foreca.Infrastructure.Database.Database;
using Foreca.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foreca.App.Features;

public class AddNotExistedCityHandler : IAutoRegisteredService
{
    private readonly ICityImageReader _cityImageReader;
    private readonly ForecaContext _forecaContext;
    private readonly IGeoReader _geoReader;

    public AddNotExistedCityHandler(ICityImageReader cityImageReader, ForecaContext forecaContext, IGeoReader geoReader)
    {
        _cityImageReader = cityImageReader;
        _forecaContext = forecaContext;
        _geoReader = geoReader;
    }

    public async Task<CityDetailsModel> Handle(CityName cityName)
    {
        // пробуем в "тупую" найти по явному совпадению текста, что бы сэкономить запросы к геокодеру
        var city = await _forecaContext.CityDetails
            .FirstOrDefaultAsync(p => p.Name == cityName);

        if (city != null)
        {
            return new CityDetailsModel
            {
                Id = city.Id,
                ImageUrl = city.ImageUrl,
                Name = city.Name,
            };
        }

        var geo = await _geoReader.GetAsync(cityName);

        // ищем по совпадению долготы\широты
        city = await _forecaContext.CityDetails
            .FirstOrDefaultAsync(p => p.Latitude == geo.Latitude && p.Longitude == geo.Longitude);

        if (city != null)
        {
            return new CityDetailsModel
            {
                Id = city.Id,
                ImageUrl = city.ImageUrl,
                Name = city.Name,
            };
        }

        // не нашли город, забираем картинку, сохраняем новый город в БД
        var imageUrl = await _cityImageReader.GetCityImageUrl(cityName);

        city = new CityDetails
        {
            Name = geo.Name,
            ImageUrl = imageUrl,
            Longitude = geo.Latitude,
            Latitude = geo.Longitude,
        };

        await _forecaContext.AddAsync(city);

        await _forecaContext.SaveChangesAsync();

        return new CityDetailsModel
        {
            Id = city.Id,
            ImageUrl = city.ImageUrl,
            Name = city.Name,
        };
    }
}