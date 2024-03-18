namespace FinancialPortfolio.Services;

using FinancialPortfolio.Repository;

public static class ServiceConfiguration
{
    public static IPortfolioService GetService()
    {
        //In a real-case application this setuo should be replaced by a dependency injection container configuration
        var repository = new InstrumentCategoryRepository();

        var service = new PortfolioService(repository);

        return service;
    }
}