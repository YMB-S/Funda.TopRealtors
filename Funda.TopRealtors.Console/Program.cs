using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Funda.TopRealtors.Core;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        var topRealtorService = host.Services.GetRequiredService<ITopRealtorService>();
        var topRealtors = await topRealtorService.CalculateTopRealtorsAsync(10, 4);
        foreach (var realtor in topRealtors)
        {
            Console.WriteLine($"Name: {realtor.RealtorName}, listings: {realtor.AmountOfListings}");
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
    {
        services.AddSingleton<HttpClient>();
        services.AddSingleton<FundaApiConfig>(provider =>
        {
            // TODO move this to a config file or environment variable
            return new FundaApiConfig("76666a29898f491480386d966b75f949");
        });

        services.AddSingleton<IFundaApiClient, FundaApiClient>();
        services.AddSingleton<ITopRealtorService, TopRealtorService>();

    });
}