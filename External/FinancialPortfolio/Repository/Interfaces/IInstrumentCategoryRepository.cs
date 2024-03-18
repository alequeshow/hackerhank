namespace FinancialPortfolio.Repository.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPortfolio.Domain;

public interface IInstrumentCategoryRepository
{
    Task<IEnumerable<InstrumentCategory>> GetCategories();
}