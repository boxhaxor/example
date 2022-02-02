using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Steeltoe;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Steeltoe.Discovery.Client;

namespace home
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args){
            return WebHost.CreateDefaultBuilder(args)
                    .AddConfigServer()
                    //.AddDiscoveryClient()
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}