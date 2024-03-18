USE FinancialPortfolio;
GO

INSERT INTO InstrumentCategories VALUES
(NEWID(), 'Low Value', 1000000, NULL),
(NEWID(), 'Medium Value', 1000000, 5000000),
(NEWID(), 'High Value', NULL, 5000000);

INSERT INTO IntrumentTypes VALUES
(NEWID(), 'Stock'),
(NEWID(), 'Bond'),
(NEWID(), 'Derivative');