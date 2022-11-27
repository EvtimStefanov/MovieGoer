using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Extensions;

public class Program
{
    public static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var webHost = CreateHostBuilder(args).Build();

        await ApplyMigrations(webHost.Services);

        await webHost.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsProduction())
                {
                    config.ConfigureKeyVault();
                }
                else
                {                   
                    config.WriteConfigurationSources();
                }
            })
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

    private static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}