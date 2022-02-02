using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common;
using common.Model;
using home.Models;
using System.Diagnostics;

namespace home.Controllers;

public class HomeController : SteelToeControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ILogger<HomeController> logger,
        IConfiguration config, 
        IOptionsSnapshot<ConfigServerData> configServerData, 
        IOptionsSnapshot<ConfigServerClientSettingsOptions> confgServerSettings) : 
        base(config, configServerData, confgServerSettings)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public IActionResult Reload()
    {
        if (Config != null)
        {
            Config.Reload();
        }

        return View();
    }

    public IActionResult ConfigServer()
    {
        CreateConfigServerDataViewData();
        return View();
    }

    private void CreateConfigServerDataViewData()
    {

        ViewData["ASPNETCORE_ENVIRONMENT"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // IConfigServerData property is set to a IOptionsSnapshot<ConfigServerData> that has been
        // initialized with the configuration data returned from the Spring Cloud Config Server
        if (IConfigServerData != null && IConfigServerData.Value != null)
        {
            var data = IConfigServerData.Value;
            ViewData["Bar"] = data.Bar ?? "Not returned";
            ViewData["Foo"] = data.Foo ?? "Not returned";

            ViewData["Kafka"] = data.Kafka.BootstrapServers ?? "Not returned";
            ViewData["HomeConnectionString"] = data.Postgres.Home.ConnectionString ?? "Not returned";
            ViewData["DataImportConnectionString"] = data.Postgres.Data_import.ConnectionString ?? "Not returned";

            ViewData["Info.Url"] = "Not returned";
            ViewData["Info.Description"] = "Not returned";

            if (data.Info != null)
            {
                ViewData["Info.Url"] = data.Info.Url ?? "Not returned";
                ViewData["Info.Description"] = data.Info.Description ?? "Not returned";
            }
        }
        else {
            ViewData["Bar"] = "Not Available";
            ViewData["Foo"] = "Not Available";
            ViewData["Info.Url"] = "Not Available";
            ViewData["Info.Description"] = "Not Available";
        }

    }

    public IActionResult ConfigServerSettings() => View(ConfigServerClientSettingsOptions);
}
