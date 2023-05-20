using System.Net;
using System.Text.Json;
using Foreca.Shared;
using Microsoft.Extensions.Options;

namespace Foreca.Infrastructure.ImagesProvider;

public class UnsplashProvider : IAutoRegisteredService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<UnsplashProviderOptions> _options;

    public UnsplashProvider(IOptions<UnsplashProviderOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient();

        _httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
    }

    public async Task<UnsplashResponse> GetImageByText(string text)
    {
        var response =
            await _httpClient.GetAsync(
                $"photos/random?orientation=landscape&per_page=1&query={text}&client_id={_options.Value.Accesskey}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return await GetImageByText("city");
        }

        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<UnsplashResponse>(await response.Content.ReadAsStreamAsync());
    }
}