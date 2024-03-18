USE FinancialPortfolio;
GO

EXEC dbo.UpsertInstrument  @MarketValue = 800000, @Type = "Stock";
EXEC dbo.UpsertInstrument  @MarketValue = 1500000, @Type = "Bond";
EXEC dbo.UpsertInstrument  @MarketValue = 6000000, @Type = "Derivative";
EXEC dbo.UpsertInstrument  @MarketValue = 300000, @Type = "Stock";

SELECT * FROM DBO.FinancialInstrumentsHistory;