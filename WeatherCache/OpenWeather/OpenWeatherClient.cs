using System.Text.Json;

namespace WeatherCache.OpenWeather
{
    public class OpenWeatherClient
    {
        const string defaultLanguage = "ru";
        const string apiKey = "d0edb2714b11c310fcc847f5ec23e409";
        const string urlTemplate = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&lang={2}";

        public OpenWeatherClient()
        {
            _lastUpdateTime = DateTime.Now;
        }
        private DateTime _lastUpdateTime;

        private readonly Dictionary<string, CurrentWeatherDto> _cache = new Dictionary<string, CurrentWeatherDto>();
        public async ValueTask<CurrentWeatherDto> GetWeatherAsync(string cityName)
        {
            var lowerCasedCityName = cityName.ToLower();
            if (_cache.ContainsKey(lowerCasedCityName))
            {
                if (DateTime.Now.Subtract(_lastUpdateTime) < TimeSpan.FromMinutes(10))
                    return _cache[lowerCasedCityName];
            }

            _lastUpdateTime = DateTime.Now;

            string currentWeatherUrl = string.Format(urlTemplate, lowerCasedCityName, apiKey, defaultLanguage);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(currentWeatherUrl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Openweathermap response has a fault code {response.StatusCode}");
            
            var currentWeatherJson = await response.Content.ReadAsStringAsync();
            JsonDocument currentWeatherDocument = JsonDocument.Parse(currentWeatherJson);
            var currentWeatherDto = currentWeatherDocument.Deserialize<CurrentWeatherDto>();
            
            _cache[lowerCasedCityName] = currentWeatherDto;

            return currentWeatherDto;
        }
    }
}