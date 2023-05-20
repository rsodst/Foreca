using Foreca.App.Models;
using Foreca.App.ValueObjects;
using Foreca.Infrastructure.Database.Database;
using Foreca.Shared;
using Microsoft.EntityFrameworkCore;

namespace Foreca.App.Features;

public class GetCitiesHandler : IAutoRegisteredService
{
    private readonly ForecaContext _forecaContext;

    public GetCitiesHandler(ForecaContext forecaContext)
    {
        _forecaContext = forecaContext;
    }

    public async Task<IEnumerable<CityDetailsModel>> Handle(CityName cityName)
    {
        // Скидываем n похожих названий городов
        var result = await _forecaContext.CityDetails
            .Where(p => EF.Functions.Like(p.Name, $"%{cityName}%"))
            .Take(50)
            .Select(p => new CityDetailsModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
            })
            .ToListAsync();

        return result;
    }
}