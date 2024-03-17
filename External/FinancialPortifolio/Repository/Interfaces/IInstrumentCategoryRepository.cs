namespace FinancialPortifolio.Repository.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPortifolio.Domain;

public interface IInstrumentCategoryRepository
{
    Task<IEnumerable<InstrumentCategory>> GetCategories();
}