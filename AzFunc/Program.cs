using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AzFunc.Models.School;
using Microsoft.Extensions.Configuration;

// var host = new HostBuilder()
//     .ConfigureFunctionsWebApplication()
//     .ConfigureServices(services => {
//         services.AddApplicationInsightsTelemetryWorkerService();
//         services.ConfigureFunctionsApplicationInsights();
//     })
//     .Build();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(x => x.AddEnvironmentVariables())
    .ConfigureHostConfiguration(x => x.AddEnvironmentVariables())
    .ConfigureServices(services =>
    {
        services.AddSingleton<HttpClient>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<SchoolContext>(
            x =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
                x.UseSqlServer(connectionString);
            }
        );
    })
    .Build();


host.Run();
