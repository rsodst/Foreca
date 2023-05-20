using System.Globalization;
using System.Text.Json.Nodes;
using Foreca.Shared;
using Microsoft.Extensions.Options;

namespace Foreca.Infrastructure.GeocoderProvider;

public class YandexGeocoderProvider : IAutoRegisteredService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<YandexGeocoderOptions> _options;

    public YandexGeocoderProvider(IOptions<YandexGeocoderOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient();

        _httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Yandex-API-Key", _options.Value.ApiKey);
    }

    public async Task<GeocoderResponse> GetPointAsync(string city)
    {
        var numberFormat = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
        };

        var response =
            await _httpClient.GetAsync(
                $"1.x?apikey={_options.Value.ApiKey}&geocode={city}&format=json");

        response.EnsureSuccessStatusCode();

        var jsonObject = JsonNode.Parse(await response.Content.ReadAsStreamAsync());

        var formattedName =
            jsonObject["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["metaDataProperty"]
                ["GeocoderMetaData"]["text"].GetValue<string>();

        var point = jsonObject["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["Point"]["pos"]
            .GetValue<string>().Split(" ");

        return new GeocoderResponse
        {
            Name = formattedName,
            Latitude = float.Parse(point[0], numberFormat),
            Longitude = float.Parse(point[1], numberFormat),
        };
    }
}