using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GanhoCapital.Services;

namespace GanhoCapital;

public static class ServiceProviderFactory
{
    private static IServiceProvider? _serviceProvider;

    public static IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = CreateServiceProvider();
            }
            return _serviceProvider;
        }
    }

    private static IServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddTransient<IOperationHandlerService, OperationHandlerService>();       
        
        return services.BuildServiceProvider();
    }

    public static void DisposeServiceProvider()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
        _serviceProvider = null;
    }
}