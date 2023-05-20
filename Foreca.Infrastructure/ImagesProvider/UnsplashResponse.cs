using System.Text.Json.Serialization;

namespace Foreca.Infrastructure.ImagesProvider;

public class UnsplashResponse
{
    [JsonPropertyName("urls")] public UnsplashUrls Urls { get; set; }
}

public class UnsplashUrls
{
    [JsonPropertyName("regular")] public string Regular { get; set; }
}