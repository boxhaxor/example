using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common;
using common.Model;

namespace data_import.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController 
    : ControllerBase
{
    private readonly IWeatherForcastService _forcastService;
    private readonly ISteelToeConfig<ConfigServerData> _steelToeConfig;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IWeatherForcastService weatherForcastService,
        ISteelToeConfig<ConfigServerData> steelToeConfig)
    {
        _logger = logger;
        _forcastService = weatherForcastService;
        this._steelToeConfig = steelToeConfig;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return this._forcastService.GetWeatherForNextDays(1,5);
    }
}
