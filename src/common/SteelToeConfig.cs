using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common.Model;

namespace common;

public interface ISteelToeConfig<T> where T : class
{
    IOptionsSnapshot<T> IConfigServerData {get;set;}
    IConfigurationRoot Config { get; set; }
    ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }
}
public class SteelToeConfig: ISteelToeConfig<ConfigServerData>
{
    public IOptionsSnapshot<ConfigServerData> IConfigServerData { get; set; }

    public ConfigServerClientSettingsOptions ConfigServerClientSettingsOptions { get; set; }

    public IConfigurationRoot Config { get; set; }
    public SteelToeConfig(IConfiguration config, IOptionsSnapshot<ConfigServerData> configServerData, IOptionsSnapshot<ConfigServerClientSettingsOptions> confgServerSettings)
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
