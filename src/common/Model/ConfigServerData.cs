using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace common.Model;

/// <summary>
/// An object used with the DI Options mechanism for exposing the data retrieved 
/// from the Spring Cloud Config Server
/// </summary>
public class ConfigServerData
{
    public string Bar { get; set; }
    public string Foo { get; set; }
    public Info Info { get; set; }
    public Postgres Postgres { get;set; }
    public Kafka Kafka { get;set; }

}

public class Kafka {
    public string BootstrapServers { get;set; }
}

public class Postgres {
    //I'd rather have this be a list, but not taking the time to look into
    public PostgresConnectionInfo Home { get; set; }
    public PostgresConnectionInfo Data_import { get; set; }
}

public class PostgresConnectionInfo {
    public string ConnectionString {get;set;}
}

public class Info
{
    public string Description { get; set; }
    public string Url { get; set; }
}
