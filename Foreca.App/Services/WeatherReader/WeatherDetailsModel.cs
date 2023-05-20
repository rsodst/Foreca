namespace Foreca.App.Services.WeatherReader;

public class WeatherDetailsModel
{
    public float Temp { get; set; }

    public float FeelsLike { get; set; }

    public string Condition { get; set; }

    public float WindSpeed { get; set; }

    public string WindDirection { get; set; }

    public float Pressure { get; set; }

    public float Humidity { get; set; }
}