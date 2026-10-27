using GanhoCapital.Domain;
using Xunit;

namespace GanhoCapital.Tests;

public class OperationGainTests
{
    [Fact]
    public void Should_Return_Empty_Results_For_Empty_MarketEvents()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>();
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void Should_Return_Zero_Tax_For_Only_Buy_Operations()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.0m, Quantity = 1000 },
            new() { Operation = OperationType.Buy, UnitCost = 15.0m, Quantity = 500 }
        };

        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Buy operations yield zero tax results
        Assert.Equal(2, results.Count);
        Assert.All(results, result => Assert.Equal(0, result.Tax));
    }

    [Fact]
    public void Should_Calculate_Tax_For_Simple_Buy_Sell_With_Gain()
    {
        // Arrange - Use amounts over 20000 threshold
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 2000 },
            new() { Operation = OperationType.Sell, UnitCost = 15.00m, Quantity = 1500 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Should have 2 results: buy (0 tax) + sell (calculated tax)
        Assert.Equal(2, results.Count);
        Assert.Equal(0, results[0].Tax); // Buy operation
        // Sale value: 15 * 1500 = 22500 (over 20000 threshold)
        // Gain = (15 - 10) * 1500 = 7500, Tax = 7500 * 0.2 = 1500
        Assert.Equal(1500.0m, results[1].Tax); // Sell operation over threshold
    }

    [Fact]
    public void Should_Return_Zero_Tax_For_Loss_Operation()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 15.00m, Quantity = 1000 },
            new() { Operation = OperationType.Sell, UnitCost = 10.00m, Quantity = 500 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Should have 2 results: buy (0 tax) + sell (0 tax for loss)
        Assert.Equal(2, results.Count);
        Assert.Equal(0, results[0].Tax); // Buy operation
        Assert.Equal(0, results[1].Tax); // Sell operation (loss)
    }

    [Fact]
    public void Should_Return_Zero_Tax_For_Operations_Under_20000_Threshold()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 1000 },
            new() { Operation = OperationType.Sell, UnitCost = 15.00m, Quantity = 500 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Should have 2 results: buy (0 tax) + sell (0 tax under threshold)
        Assert.Equal(2, results.Count);
        Assert.Equal(0, results[0].Tax); // Buy operation
        Assert.Equal(0, results[1].Tax); // Sell operation under 20000 threshold (15 * 500 = 7500)
    }

    [Fact]
    public void Should_Calculate_Tax_For_Operations_Over_20000_Threshold()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 2000 },
            new() { Operation = OperationType.Sell, UnitCost = 15.00m, Quantity = 2000 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Should have 2 results: buy (0 tax) + sell (calculated tax)
        Assert.Equal(2, results.Count);
        Assert.Equal(0, results[0].Tax); // Buy operation
        // Gain = (15 - 10) * 2000 = 10000, Tax = 10000 * 0.2 = 2000
        Assert.Equal(2000.0m, results[1].Tax); // Sell operation over threshold (15 * 2000 = 30000)
    }

    [Fact]
    public void Should_Calculate_Correct_Average_Price_With_Multiple_Buys()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 1000 },
            new() { Operation = OperationType.Buy, UnitCost = 20.00m, Quantity = 1000 },
            new() { Operation = OperationType.Sell, UnitCost = 25.00m, Quantity = 2000 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert - Should have 3 results: buy1 (0) + buy2 (0) + sell (calculated tax)
        Assert.Equal(3, results.Count);
        Assert.Equal(0, results[0].Tax); // First buy
        Assert.Equal(0, results[1].Tax); // Second buy
        // Average price = (10*1000 + 20*1000) / 2000 = 15
        // Gain = (25 - 15) * 2000 = 20000, Tax = 20000 * 0.2 = 4000
        Assert.Equal(4000.0m, results[2].Tax); // Sell operation
    }

    [Fact]
    public void Should_Handle_Zero_Quantity_Operations()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 0 },
            new() { Operation = OperationType.Sell, UnitCost = 15.00m, Quantity = 0 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act & Assert - Should not throw exception
        var results = operationGain.CheckOperation().ToList();
        Assert.Equal(2, results.Count); // Should still yield results
        Assert.All(results, result => Assert.Equal(0, result.Tax)); // All should have zero tax
    }

    [Fact]
    public void Should_Handle_Error_When_SellingMoreThanAvailable()
    {
        // Arrange
        var marketEvents = new List<MarketEvent>
        {
            new() { Operation = OperationType.Buy, UnitCost = 10.00m, Quantity = 10000 },
            new() { Operation = OperationType.Sell, UnitCost = 20.00m, Quantity = 11000 }
        };
        var operationGain = new OperationGain(marketEvents);

        // Act
        var results = operationGain.CheckOperation().ToList();

        // Assert
        Assert.Equal(2, results.Count); // Should still yield results
        Assert.Equal("Can't sell more stocks than you have", results.Last().Error); // Sell operation shows error
    }
}