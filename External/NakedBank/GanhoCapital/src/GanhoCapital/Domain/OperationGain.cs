using System.Text.Json.Serialization;

namespace GanhoCapital.Domain;

public class OperationGain(IEnumerable<MarketEvent> marketEvents)
{
    private IEnumerable<MarketEvent> _marketEvents = marketEvents;

    private decimal _averagePrice;

    private long _totalQuantity;

    private decimal _totalLoss;

    public IEnumerable<OperationResult> CheckOperation()
    {
        foreach (var marketEvent in _marketEvents)
        {
            if (marketEvent.Operation == OperationType.Buy)
            {
                ProcessBuy(marketEvent.UnitCost, marketEvent.Quantity);

                yield return new OperationResult { Tax = 0 };
            }
            else if (marketEvent.Operation == OperationType.Sell)
            {                
                bool isLoss = IsLoss(marketEvent.UnitCost, marketEvent.Quantity);
                bool isUnderThreshold = IsUnderThreshold(marketEvent.UnitCost * marketEvent.Quantity);
                
                if (isLoss)
                {
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    yield return new OperationResult { Tax = 0 };
                }
                else if (isUnderThreshold)
                {
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    yield return new OperationResult { Tax = 0 };
                }
                else
                {
                    decimal gain = CalculateGain(marketEvent.UnitCost, marketEvent.Quantity);
                    UpdateQuantityOnSell(marketEvent.Quantity);
                    
                    decimal tax = gain * 0.2m;

                    yield return new OperationResult { Tax = tax };
                }
            }
        }
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

    private bool IsUnderThreshold(decimal operationValue)
    {
        return operationValue <= 20000;
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
}