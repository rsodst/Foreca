namespace Foreca.App.Services.GeoReader;

public interface IGeoReader
{
    public Task<GeoReaderModel> GetAsync(string city);
}