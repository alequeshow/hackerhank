using GanhoCapital.Domain;

namespace GanhoCapital.Services;

public class OperationHandlerService : IOperationHandlerService
{
    public IEnumerable<OperationResult> HandleOperation(IEnumerable<MarketEvent> marketEvents)
    {
        var operationGain = new OperationGain(marketEvents);
        return [.. operationGain.CheckOperation()];
    }
}