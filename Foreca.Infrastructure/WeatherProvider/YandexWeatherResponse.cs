using System.Text.Json.Serialization;

namespace Foreca.Infrastructure.WeatherProvider;

public class YandexWeatherResponse
{
    [JsonPropertyName("fact")] public CurrentWeatherModel Current { get; set; }

    [JsonPropertyName("forecasts")] public IEnumerable<ForecastWeatherModel> Forecast { get; set; }
}

public class CurrentWeatherModel
{
    [JsonPropertyName("temp")] public float Temp { get; set; }

    [JsonPropertyName("feels_like")] public float FeelsLike { get; set; }

    [JsonPropertyName("condition")] public string Condition { get; set; }

    [JsonPropertyName("wind_speed")] public float WindSpeed { get; set; }

    [JsonPropertyName("wind_dir")] public string WindDirection { get; set; }

    [JsonPropertyName("pressure_mm")] public float PressureMM { get; set; }

    [JsonPropertyName("humidity")] public float Humidity { get; set; }
}

public class ForecastWeatherModel
{
    [JsonPropertyName("date")] public string Date { get; set; }

    [JsonPropertyName("parts")] public WeatherPart Parts { get; set; }
}

public class WeatherPart
{
    [JsonPropertyName("day")] public WeatherForecastDetails Day { get; set; }

    [JsonPropertyName("night")] public WeatherForecastDetails Night { get; set; }
}

public class WeatherForecastDetails
{
    [JsonPropertyName("temp_min")] public float TempMin { get; set; }

    [JsonPropertyName("temp_max")] public float TempMax { get; set; }

    [JsonPropertyName("condition")] public string Condition { get; set; }

    [JsonPropertyName("wind_speed")] public float WindSpeed { get; set; }

    [JsonPropertyName("wind_dir")] public string WindDirection { get; set; }

    [JsonPropertyName("pressure_mm")] public float PressureMM { get; set; }

    [JsonPropertyName("humidity")] public float Humidity { get; set; }
}