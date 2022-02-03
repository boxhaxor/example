using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common;
using common.Model;
using home.Models;
using System.Diagnostics;

namespace home.Controllers;

public class HomeController : Controller
{
    private readonly ISteelToeConfig<ConfigServerData> _steelToeConfig;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ILogger<HomeController> logger,
        ISteelToeConfig<ConfigServerData> steelToeConfig)
    {
        _logger = logger;
        _steelToeConfig = steelToeConfig;
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
        if (_steelToeConfig.Config != null)
        {
            _steelToeConfig.Config.Reload();
        }

        return View();
    }

    public IActionResult ConfigServer()
    {
        CreateConfigServerDataViewData();
        return View();
    }

    public IActionResult UploadData()
    {
        UploadDataViewModel model = new UploadDataViewModel();
        if (_steelToeConfig.IConfigServerData != null && _steelToeConfig.IConfigServerData.Value != null) {
            var data = _steelToeConfig.IConfigServerData.Value;
            model.DataUploadServiceUrl = data.DataImportService.Url ?? "Not returned";
        }
        return View(model);
    }

    private void CreateConfigServerDataViewData()
    {

        ViewData["ASPNETCORE_ENVIRONMENT"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // IConfigServerData property is set to a IOptionsSnapshot<ConfigServerData> that has been
        // initialized with the configuration data returned from the Spring Cloud Config Server
        if (_steelToeConfig.IConfigServerData != null && _steelToeConfig.IConfigServerData.Value != null)
        {
            var data = _steelToeConfig.IConfigServerData.Value;
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

    public IActionResult ConfigServerSettings() => View(_steelToeConfig.ConfigServerClientSettingsOptions);
}
