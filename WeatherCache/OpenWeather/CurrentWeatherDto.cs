using System.Text.Json.Serialization;

namespace WeatherCache.OpenWeather
{
    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }
    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string IconCode { get; set; }
    }
    public class Main
    {
        [JsonPropertyName("temp")]
        public double TemperatureKelvin { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLikeKelvin { get; set; }
    }
    public class CurrentWeatherDto
    {
        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weathers { get; set; }

        [JsonPropertyName("main")]
        public Main main { get; set; }

        [JsonPropertyName("dt")]
        public int LastUpdatedDateUnixSeconds { get; set; }
    }
}