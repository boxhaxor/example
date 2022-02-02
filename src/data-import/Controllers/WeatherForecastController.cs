using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common;
using common.Model;

namespace data_import.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : SteelToeControllerBase
{
    private readonly IWeatherForcastService _forcastService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IWeatherForcastService weatherForcastService,
        IConfiguration config, 
        IOptionsSnapshot<ConfigServerData> configServerData, 
        IOptionsSnapshot<ConfigServerClientSettingsOptions> confgServerSettings) : 
        base(config, configServerData, confgServerSettings)
    {
        _logger = logger;
        _forcastService = weatherForcastService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return this._forcastService.GetWeatherForNextDays(1,5);
    }
}
