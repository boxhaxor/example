using Confluent.Kafka;
using System.Net;
using common;
using common.Model;
using data_import.Models;


public class ImportJobKafkaProducer {
    private readonly ISteelToeConfig<ConfigServerData> _steelToeConfig;
    private readonly ILogger<ImportJobKafkaProducer> _logger;

    public ImportJobKafkaProducer(
            ISteelToeConfig<ConfigServerData> steelToeConfig,
            ILogger<ImportJobKafkaProducer> logger
        ) {
        this._steelToeConfig = steelToeConfig;
        this._logger = logger;
    }

    public async Task SendProcessingRequest(ImportJob job){
        if (_steelToeConfig.IConfigServerData == null || _steelToeConfig.IConfigServerData.Value == null) {
            throw new Exception("Can't get connection string for kafka!");
        }
        var data = _steelToeConfig.IConfigServerData.Value;
        var config = new ProducerConfig
        {
            BootstrapServers = data.Kafka.BootstrapServers,
            ClientId = Dns.GetHostName()
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build()) {
            var t = producer.ProduceAsync("topic", new Message<Null, string> { Value="hello world" });
            await t.ContinueWith(task => {
                if (task.IsFaulted)
                {
                    this._logger.LogInformation("Failed to send message");
                }
                else
                {
                    this._logger.LogInformation($"Wrote to offset: {task.Result.Offset}");
                }
            });
        }
    }
}
