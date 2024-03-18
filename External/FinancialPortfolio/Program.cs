namespace FinancialPortfolio;

using FinancialPortfolio.Domain;
using FinancialPortfolio.Services;

public class Program
{    
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Categorizing Financial Instruments in a Portfolio");
        Console.WriteLine();

        var portfolioService = ServiceConfiguration.GetService();

        var instruments = new List<FinancialInstrument>()
        {
            new() { MarketValue = 800000, Type = "Stock"},
            new() { MarketValue = 1500000, Type = "Bond" },
            new() { MarketValue = 6000000, Type = "Derivative" },
            new() { MarketValue = 300000, Type = "Stock" }
        };

        instruments.ForEach(x => 
            Console.WriteLine($"{nameof(x.MarketValue)}: {x.MarketValue}, {nameof(x.Type)}: {x.Type}"));

        Console.WriteLine();
        Console.WriteLine("Evaluating instrument samples:");

        var result = await portfolioService.EvaluateInstrumentsAsync(instruments);

        Console.WriteLine(string.Join(", ", result));
    }
}