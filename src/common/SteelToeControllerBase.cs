using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common.Model;
using System;

namespace common;

public class SteelToeControllerBase : Controller
{
    protected IOptionsSnapshot<ConfigServerData> IConfigServerData { get; set; }

    protected ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }

    protected IConfigurationRoot Config { get; set; }
    public SteelToeControllerBase(IConfiguration config, IOptionsSnapshot<ConfigServerData> configServerData, IOptionsSnapshot<ConfigServerClientSettingsOptions> confgServerSettings)
    {
        // The ASP.NET DI mechanism injects the data retrieved from the Spring Cloud Config Server 
        // as an IOptionsSnapshot<ConfigServerData>. This happens because we added the call to:
        // "services.Configure<ConfigServerData>(Configuration);" in the StartUp class
        if (configServerData != null)
        IConfigServerData = configServerData;

        // The settings used in communicating with the Spring Cloud Config Server
        if (confgServerSettings != null)
        ConfigServerClientSettingsOptions = confgServerSettings.Value;

        Config = config as IConfigurationRoot;
    }
}
