namespace FinancialPortfolio.Domain;

public class FinancialInstrument : IFinancialInstrument 
{
    public double MarketValue { get; set; }
    
    public string Type { get; set; }
}