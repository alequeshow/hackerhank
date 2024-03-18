namespace FinancialPortfolio.Repository;

using FinancialPortfolio.Domain;
using FinancialPortfolio.Repository.Interfaces;

public class InstrumentCategoryRepository : IInstrumentCategoryRepository
{
    public async Task<IEnumerable<InstrumentCategory>> GetCategories()
    {
        var categories = await Task.Run(IEnumerable<InstrumentCategory>() => 
        {
            var fixCategories = new List<InstrumentCategory> 
            {
                new InstrumentCategory(Name: "Low Value", MinValue: 1_000_000),
                new InstrumentCategory(Name: "Medium Value", MinValue: 1_000_000, MaxValue: 5_000_000),
                new InstrumentCategory(Name: "High Value", MaxValue: 5_000_000),
            };

            return fixCategories;
        });

        return categories;
    }
}