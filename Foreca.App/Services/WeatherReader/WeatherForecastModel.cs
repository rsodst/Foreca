namespace Foreca.App.Services.WeatherReader;

public class WeatherForecastModel
{
    public DateTime Date { get; set; }

    public float TempMax { get; set; }

    public float TempMin { get; set; }

    public string Condition { get; set; }

    public float WindSpeed { get; set; }

    public string WindDirection { get; set; }

    public float Pressure { get; set; }

    public float Humidity { get; set; }
}