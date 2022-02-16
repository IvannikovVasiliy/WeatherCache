﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherCache.OpenWeather;

namespace WeatherCache.Controllers
{
    [Route("weather")]
    public class WeatherController : Controller
    {
        public async Task<ActionResult> Get([Required, FromQuery(Name = "city")] string cityName)
        {
            if (!ModelState.IsValid)
                return BadRequest("Name of a ccity is not provided");

            var openWeatherClient = new OpenWeatherClient();
            CurrentWeatherDto currentWeatherDto = await openWeatherClient.GetWeatherAsync(cityName);

            return Ok(currentWeatherDto);
        }
    }
}
