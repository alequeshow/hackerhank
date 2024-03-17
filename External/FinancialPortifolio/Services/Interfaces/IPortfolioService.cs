namespace FinancialPortifolio.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPortifolio.Domain;

public interface IPortfolioService
{
    Task<IEnumerable<string>> EvaluateInstrumentsAsync(IEnumerable<IFinancialInstrument> instruments);
}