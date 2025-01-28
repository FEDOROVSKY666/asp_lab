using lr_nine.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace lr_nine.Controllers
{
    [Route("Weather")]
    public class WeatherController : Controller
    {
        private readonly HttpClient _client;
        public WeatherController()
        {
            _client = new HttpClient();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("City")]
        public async Task<IActionResult> GetWeather(CityViewModel model)
        {
            if (!ModelState.IsValid) {
                return View("Index", model);
            }
            try
            {
                string apiToken = "3eeac7d6dd6b9160fbf462797f7bc605";
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={model.CityName}&appid={apiToken}&units=metric";

                HttpResponseMessage response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var serializedWeatherData = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherViewModel>(serializedWeatherData);
                    return View(weatherData);
                }
                else
                {
                    ModelState.AddModelError(nameof(model.CityName), $"City {model.CityName} not found:(");
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(505);
            }
        }
    }
}
