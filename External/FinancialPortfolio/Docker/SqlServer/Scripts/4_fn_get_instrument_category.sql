USE FinancialPortfolio;
GO

CREATE FUNCTION dbo.GetInstrumentCategory
(
    @MarketValue DECIMAL
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @CategoryId UNIQUEIDENTIFIER;

    SELECT @CategoryId = Id
    FROM dbo.InstrumentCategories
    WHERE
            (@MarketValue < MinValue AND MaxValue IS NULL)
        OR  (MinValue <= @MarketValue AND @MarketValue <= MaxValue)
        OR  (MaxValue < @MarketValue AND MinValue IS NULL)        

    RETURN @CategoryId;
END;
GO