namespace GanhoCapital.Domain;

public class OperationGain(IEnumerable<MarketEvent> marketEvents)
{
    private IEnumerable<MarketEvent> _marketEvents = marketEvents;

    private decimal _averagePrice;

    private long _totalQuantity;

    private decimal _totalLoss;

    private static bool IsUnderThreshold(decimal operationValue) => operationValue <= 20000;

    public IEnumerable<OperationResult> CheckOperation()
    {
        var operationResults = new List<OperationResult>();

        foreach (var marketEvent in _marketEvents)
        {
            if (marketEvent.Operation == OperationType.Buy)
            {
                ProcessBuy(marketEvent.UnitCost, marketEvent.Quantity);

                operationResults.Add(new () { Tax = 0 });
                continue;
            }
            else if (marketEvent.Operation == OperationType.Sell)
            {
                if(!HasEnoughQuantityToSell(marketEvent.Quantity))
                {
                    operationResults.Add(new () { Error = "Can't sell more stocks than you have" });
                    continue;
                }

                bool isLoss = IsLoss(marketEvent.UnitCost, marketEvent.Quantity);
                bool isUnderThreshold = IsUnderThreshold(marketEvent.UnitCost * marketEvent.Quantity);
                
                if (isLoss)
                {
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    operationResults.Add(new () { Tax = 0 });
                    continue;
                }
                else if (isUnderThreshold)
                {
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    operationResults.Add(new () { Tax = 0 });
                    continue;
                }
                else
                {
                    decimal gain = CalculateGain(marketEvent.UnitCost, marketEvent.Quantity);
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    
                    decimal tax = gain * 0.2m;

                    operationResults.Add(new () { Tax = tax });
                    continue;
                }
            }
        }

        return operationResults;
    }

    private void ProcessBuy(decimal unitCost, long quantity)
    {
        if (quantity == 0) return; // Guard against zero quantity
        
        var eventCost = unitCost * quantity;
        _averagePrice = (_averagePrice * _totalQuantity + eventCost) / (_totalQuantity + quantity);
        _totalQuantity += quantity;
    }

    private void UpdateQuantityOnSell(long quantity)
    {
        _totalQuantity -= quantity;

        if (_totalQuantity == 0)
        {
            _averagePrice = 0;
        }
    }

    private bool IsLoss(decimal unitCost, long quantity)
    {
        if (unitCost < _averagePrice)
        {
            _totalLoss += (_averagePrice - unitCost) * quantity;
            return true;
        }

        return false;
    }

    private decimal CalculateGain(decimal unitValue, long quantity)
    {
        var gain = (unitValue - _averagePrice) * quantity;

        if (_totalLoss > 0)
        {
            if (gain <= _totalLoss)
            {
                _totalLoss -= gain;
                gain = 0;
            }
            else
            {
                gain -= _totalLoss;
                _totalLoss = 0;
            }
        }

        return gain;
    }

    private bool HasEnoughQuantityToSell(long quantity)
    {
        return _totalQuantity >= quantity;
    }
}