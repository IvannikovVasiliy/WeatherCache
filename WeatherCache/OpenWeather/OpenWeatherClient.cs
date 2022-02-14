using System.Text.Json;

namespace WeatherCache.OpenWeather
{
    public class OpenWeatherClient
    {
        const string defaultLanguage = "ru";
        const string apiKey = "d0edb2714b11c310fcc847f5ec23e409";
        const string urlTemplate = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&lang={2}";

        public async ValueTask<CurrentWeatherDto> GetWeatherAsync(string cityName)
        {
            var lowerCasedCityName = cityName.ToLower();

            string currentWeatherUrl = string.Format(urlTemplate, lowerCasedCityName, apiKey, defaultLanguage);
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(currentWeatherUrl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Openweathermap response has a fault code {response.StatusCode}");
            var currentWeatherJson = await response.Content.ReadAsStringAsync();

            JsonDocument currentWeatherDocument = JsonDocument.Parse(currentWeatherJson);
            return currentWeatherDocument.Deserialize<CurrentWeatherDto>();
        }
    }
}