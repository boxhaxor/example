using common;


public interface IWeatherForcastService {
    IEnumerable<WeatherForecast> GetWeatherForNextDays(int startDay, int endDay);
}
public class WeatherForcastService : IWeatherForcastService {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public IEnumerable<WeatherForecast> GetWeatherForNextDays(int startDay = 1, int endDay = 5){
        return Enumerable.Range(startDay, endDay).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}