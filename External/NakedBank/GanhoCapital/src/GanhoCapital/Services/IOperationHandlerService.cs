using GanhoCapital.Domain;

namespace GanhoCapital.Services;

public interface IOperationHandlerService
{
    IEnumerable<OperationResult> HandleOperation(IEnumerable<MarketEvent> marketEvents);
}