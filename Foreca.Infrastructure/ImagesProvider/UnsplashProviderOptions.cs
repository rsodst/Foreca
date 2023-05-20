using Foreca.Shared;

namespace Foreca.Infrastructure.ImagesProvider;

public class UnsplashProviderOptions : IAutoRegisteredOptions
{
    public string Accesskey { get; set; }

    public string BaseUrl { get; set; }
}