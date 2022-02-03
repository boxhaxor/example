using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Microsoft.Extensions.Configuration;
using common;
using common.Model;
using data_import.Models;
using data_import.Repositories;
using Microsoft.AspNetCore.Cors;

namespace data_import.Controllers;

[EnableCors("Default")]
[ApiController]
[Route("[controller]")]
public class ImportController : Controller
{
    private readonly ISteelToeConfig<ConfigServerData> _steelToeConfig;
    private readonly ImportJobKafkaProducer _kafkaProducer;
    private readonly ImportJobRepository _jobRepository;
    private readonly ImportJobContext context;
    private readonly ILogger<ImportController> _logger;

    public ImportController(
        ILogger<ImportController> logger,
        ISteelToeConfig<ConfigServerData> steelToeConfig,
        ImportJobRepository jobRepository,
        ImportJobContext context,
        ImportJobKafkaProducer kafkaProducer
        )
    {
        this._logger = logger;
        this._steelToeConfig = steelToeConfig;
        this._jobRepository = jobRepository;
        this.context = context;
        this._kafkaProducer = kafkaProducer;
    }

    [HttpPost(Name = "PostImportData"), DisableRequestSizeLimit]
    public async Task<List<ImportJob>> Post(List<IFormFile> files)
    {
        List<ImportJob> jobsStarted = new List<ImportJob>();
        long size = files.Sum(f => f.Length);
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    //In a perfect world this would be s3 or some highly available blob service
                    await formFile.CopyToAsync(stream);
                }
                var importJob = new ImportJob(){ ImportPath = filePath, Status = ImportStatus.StartingProcessing};
                await this._jobRepository.Save(importJob);
                jobsStarted.Add(importJob);
                //Publish to kafka
                await this._kafkaProducer.SendProcessingRequest(importJob);
                //Todo: Implement a client for kafka to consume this
                //Create another project of type worker.
                //Use the same client (confluent)
                //https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html#dotnet-example-code
            }
        }
        return jobsStarted;
    }
}