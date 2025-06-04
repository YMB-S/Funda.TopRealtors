using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Funda.TopRealtors.Core;
using Funda.TopRealtors.RealtorConsole;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();

        var console = host.Services.GetRequiredService<TopRealtorObserverConsole>();
        var topRealtorService = host.Services.GetRequiredService<ITopRealtorService>();
        topRealtorService.AddObserver(console);

        var config = host.Services.GetRequiredService<FundaApiConfig>();

        Console.WriteLine("Calculating top realtors on Funda by objects for sale. Press enter to start.");
        Console.ReadLine();
        var topRealtors = await topRealtorService.CalculateTopRealtorsAsync(config.AmountOfRealtorsToRank, 10);
        Console.Clear();
        ShowTopRealtors(topRealtors);

        Console.WriteLine($"{Environment.NewLine}Next step is calculating top realtors on Funda by objects for sale, with a garden. Press enter to start.");
        Console.ReadLine();
        topRealtors = await topRealtorService.CalculateTopRealtorsWithGardensAsync(config.AmountOfRealtorsToRank, 10);
        Console.Clear();
        ShowTopRealtors(topRealtors);
    }

    private static void ShowTopRealtors(List<Realtor> topRealtors)
    {
        for (int i = 0; i < topRealtors.Count; i++)
        {
            var realtor = topRealtors[i];
            Console.WriteLine($"Nr. {i+1}: {realtor.RealtorName} with {realtor.AmountOfListings} listings.");
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
    {
        services.AddSingleton<HttpClient>();

        services.AddSingleton<FundaApiConfig>(provider =>
        {
            // TODO move this to a config file or environment variables
            return new FundaApiConfig(
                apiKey: "76666a29898f491480386d966b75f949",
                amountOfRealtorsToRank: 10
            );
        });

        services.AddSingleton<IFundaApiClient, FundaApiClient>();
        services.AddSingleton<ITopRealtorService, TopRealtorService>();

        services.AddSingleton<TopRealtorObserverConsole>();
    });
}