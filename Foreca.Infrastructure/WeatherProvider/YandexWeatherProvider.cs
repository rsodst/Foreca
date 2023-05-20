using System.Globalization;
using System.Text.Json;
using Foreca.Shared;
using Microsoft.Extensions.Options;

namespace Foreca.Infrastructure.WeatherProvider;

public class YandexWeatherProvider : IAutoRegisteredService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<YandexWeatherOptions> _options;

    public YandexWeatherProvider(IOptions<YandexWeatherOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient();

        _httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Yandex-API-Key", _options.Value.ApiKey);
    }

    public async Task<YandexWeatherResponse> GetWeatherDetails(float lat, float lon, int limit = 1)
    {
        var numberFormat = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
        };

        limit %= 7; // кол-во дней в прогнозе, максимум 7

        var response =
            await _httpClient.GetAsync(
                $"v2/forecast?lat={lat.ToString(numberFormat)}&lon={lon.ToString(numberFormat)}&limit={limit}");

        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<YandexWeatherResponse>(await response.Content.ReadAsStreamAsync());
    }
}