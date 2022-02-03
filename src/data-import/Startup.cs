using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.ConfigServer;
using common;
using common.Model;
using data_import.Repositories;

namespace data_import
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            // Optional: Adds ConfigServerClientOptions to service container
            services.ConfigureConfigServerClientOptions(Configuration);

            // Add framework services.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Adds the configuration data POCO configured with data returned from the Spring Cloud Config Server
            services.Configure<ConfigServerData>(Configuration);

            //Dependency injection
            services.AddScoped<IWeatherForcastService, WeatherForcastService>();
            services.AddScoped<ISteelToeConfig<ConfigServerData>, SteelToeConfig>();
            services.AddScoped<ImportJobRepository, ImportJobRepository>();
            services.AddScoped<ImportJobContext, ImportJobContext>();
            services.AddScoped<ImportJobKafkaProducer, ImportJobKafkaProducer>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "Default",
                            builder =>
                            {
                                //Todo: get this from spring config
                                builder.WithOrigins("http://example.com",
                                    "https://localhost:7096",
                                    "https://localhost:7139")
                                        .WithMethods("PUT", "POST", "PATCH", "DELETE", "GET");
                            });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ImportJobContext importJobContext)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ImportJobContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }


            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }

    }
}