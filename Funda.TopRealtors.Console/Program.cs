using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Funda.TopRealtors.Core.services;
using Funda.TopRealtors.Core.clients;

internal class Program
{
    private static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        var topRealtorService = host.Services.GetRequiredService<ITopRealtorService>();

        Console.WriteLine(topRealtorService.Test());
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IFundaApiClient, FundaApiClient>();
        services.AddSingleton<ITopRealtorService, TopRealtorService>();
    });
}