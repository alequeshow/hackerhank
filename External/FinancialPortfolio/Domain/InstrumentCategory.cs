namespace FinancialPortfolio.Domain;

public record InstrumentCategory(string Name, double? MinValue = default, double? MaxValue = default);