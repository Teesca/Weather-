namespace WeatherApplication.Data;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;
    private const string WeatherApiUrl = "https://weatherapi-com.p.rapidapi.com/forecast.json?q=your-location";
    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
	{
		return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = startDate.AddDays(index),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray());
	}
    public async Task<WeatherForecast> GetWeatherForecast()
    {
        var response = await _httpClient.GetAsync(WeatherApiUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var forecast = JsonConvert.DeserializeObject<WeatherForecast>(content);

        return forecast;
    }

}

