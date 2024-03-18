USE FinancialPortfolio;
GO

CREATE PROCEDURE UpsertInstrument
    @Type VARCHAR(50),
    @MarketValue DECIMAL
AS
BEGIN
    
    -- Find the proper domain entities
    DECLARE @InstrumentTypeId UNIQUEIDENTIFIER = (SELECT Id FROM dbo.IntrumentTypes WHERE Name = @Type);
    DECLARE @InstrumentCategoryId UNIQUEIDENTIFIER = (SELECT dbo.GetInstrumentCategory(@MarketValue));   
    DECLARE @InstrumentCategoryName VARCHAR(50) =  (SELECT Name FROM dbo.InstrumentCategories WHERE Id = @InstrumentCategoryId);
    DECLARE @InstrumentId UNIQUEIDENTIFIER;

    SELECT @InstrumentId = Id FROM dbo.FinancialInstruments WHERE TypeId = @InstrumentTypeId

    IF @InstrumentId IS NOT NULL
    BEGIN
        UPDATE dbo.FinancialInstruments SET 
            MarketValue = @MarketValue,
            CategoryId = @InstrumentCategoryId 
        WHERE TypeId = @InstrumentCategoryId;        
    END
    ELSE
    BEGIN
        SET @InstrumentId = NEWID();
        INSERT INTO dbo.FinancialInstruments VALUES 
        (@InstrumentId, @InstrumentTypeId, @InstrumentCategoryId, @MarketValue);
    END

    -- INSERT VALUE IN HISTORY TABLE TO FOLLOW CHANGES
    INSERT INTO dbo.FinancialInstrumentsHistory VALUES
    (@InstrumentId, @Type, @InstrumentCategoryName, @MarketValue, GETUTCDATE());
END
GO
