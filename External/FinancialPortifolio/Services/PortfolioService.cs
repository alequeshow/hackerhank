namespace FinancialPortifolio.Services;

using System.Threading.Tasks;
using FinancialPortifolio.Repository.Interfaces;

public class PortfolioService : IPortfolioService
{
    private readonly IInstrumentCategoryRepository instrumentCategoryRepository;

    public PortfolioService(IInstrumentCategoryRepository instrumentCategoryRepository)
    {
        this.instrumentCategoryRepository = instrumentCategoryRepository;    
    }

    public async Task<IEnumerable<string>> EvaluateInstrumentsAsync(IEnumerable<IFinancialInstrument> instruments)
    {
        var tasks = instruments.Select(instrument => Task.Run(async Task<string>() =>
        {
            return await EvaluateSingleInstrumentAsync(instrument);
        })).ToList();

        var taskResults = await Task.WhenAll(tasks);

        return taskResults;
    }

    private async Task<string> EvaluateSingleInstrumentAsync(IFinancialInstrument instrument)
    {
        var categories = await this.instrumentCategoryRepository.GetCategories();

        // For perfomance purposes and avoid over engineering, the service follows these assumptions:
        // 1 - The categories are ordered by maket-value. First by non-null min value, then by non-null max value
        // 2 - The value ranges are not conflictants and without gaps among categories

        foreach(var category in categories)
        {
            if(category.MinValue.HasValue 
                && !category.MaxValue.HasValue 
                && instrument.MarketValue < category.MinValue)
            {
                return category.Name;
            }
            else if(category.MinValue.HasValue 
                    && category.MaxValue.HasValue 
                    && instrument.MarketValue >= category.MinValue 
                    && instrument.MarketValue <= category.MaxValue)
            {
                return category.Name;
            }
            else if(category.MaxValue.HasValue 
                    && !category.MinValue.HasValue 
                    && instrument.MarketValue > category.MaxValue)
            {
                return category.Name;
            }
        }

        return "Market value not mapped";
    }
}