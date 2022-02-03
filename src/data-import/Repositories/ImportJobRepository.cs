using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using data_import.Models;
using common;
using common.Model;

namespace data_import.Repositories;

public class ImportJobConfiguration : IEntityTypeConfiguration<ImportJob>
{
    public void Configure(EntityTypeBuilder<ImportJob> builder)
    {
        //builder.HasKey(o => o.OrderNumber);
        //builder.Property(t => t.OrderDate)
        //        .IsRequired()
        //        .HasColumnType("Date")
        //        .HasDefaultValueSql("GetDate()");

        builder.HasKey(b => b.JobId);
        builder.Property(b => b.ImportPath)
            .IsRequired();
        builder.Property(b => b.Status)
            .IsRequired();
    }
}

public class ImportJobContext : DbContext
{
    private readonly ISteelToeConfig<ConfigServerData> _steelToeConfig;

    public ImportJobContext(
        ISteelToeConfig<ConfigServerData> steelToeConfig
        ) : base() {
        this._steelToeConfig = steelToeConfig;
    }
    public DbSet<ImportJob> ImportJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ImportJobConfiguration());
        //modelBuilder.Entity<ImportJob>( entity => {
        //    entity.HasKey(b => b.JobId);
        //    entity.Property(b => b.ImportPath)
        //        .IsRequired();
        //    entity.Property(b => b.Status)
        //        .IsRequired();
        //});
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        if (_steelToeConfig.IConfigServerData != null && _steelToeConfig.IConfigServerData.Value != null)
        {
            var data = _steelToeConfig.IConfigServerData.Value;
            optionsBuilder.UseNpgsql(data.Postgres.Home.ConnectionString);
        }
        else {
            throw new Exception("Can't get connection string for postgres!");
        }
    }
}

//Ideally we'd have a generic repository.
public class ImportJobRepository {
    private readonly ImportJobContext _jobContext;

    public ImportJobRepository(ImportJobContext jobContext){
        this._jobContext = jobContext;
    }

    public async Task Save(ImportJob importJob){
        this._jobContext.ImportJobs.Add(importJob);
        await this._jobContext.SaveChangesAsync();
    }
    public async Task<ImportJob> Get(int jobId){
        return await this._jobContext.ImportJobs.Where(x => x.JobId == jobId).FirstAsync();
    }
}