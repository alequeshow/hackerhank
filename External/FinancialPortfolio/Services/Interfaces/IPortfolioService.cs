namespace FinancialPortfolio.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialPortfolio.Domain;

public interface IPortfolioService
{
    Task<IEnumerable<string>> EvaluateInstrumentsAsync(IEnumerable<IFinancialInstrument> instruments);
}