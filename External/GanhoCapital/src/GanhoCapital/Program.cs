using GanhoCapital.Domain;
using GanhoCapital.JsonHelpers;
using GanhoCapital.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text.Json;

namespace GanhoCapital;

public class Program
{
    public static async Task Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        try
        {
            await ProcessInputAsync();
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error: {ex.Message}");
            Environment.Exit(1);
        }
        finally
        {
            ServiceProviderFactory.DisposeServiceProvider();
        }
    }

    private static async Task ProcessInputAsync()
    {
        string? input;

        while (!ShouldEnd(input = await Console.In.ReadLineAsync()))
        {
            var result = await ProcessJsonInputAsync(input!.Trim());
            Console.WriteLine(result);            
        }
    }

    private static Task<string> ProcessJsonInputAsync(string jsonInput)
    {
        var marketEvents = JsonSerializer.Deserialize<List<MarketEvent>>(jsonInput, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        using (var scope = ServiceProviderFactory.ServiceProvider.CreateScope())
        {
            var handlerService = scope.ServiceProvider.GetRequiredService<IOperationHandlerService>();
            var processedData = handlerService.HandleOperation(marketEvents!);

            var result = processedData.ToInlineJson();

            return Task.FromResult(result);
        }        
    }

    private static bool ShouldEnd(string? input)
    {
        return string.IsNullOrEmpty(input);
    }
}
