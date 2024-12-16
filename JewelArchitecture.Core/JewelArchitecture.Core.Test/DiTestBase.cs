using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Core.Test;

public abstract class DiTestBase : IDisposable
{
    protected readonly IServiceCollection _serviceCollection;
    private ServiceProvider? _serviceProvider;

    protected ServiceProvider? ServiceProvider { get => _serviceProvider; }

    public DiTestBase(bool buildServiceProvider = false)
    {
        _serviceCollection = GetServiceCollection();

        if (buildServiceProvider)
        {
            BuildServiceProvider();
        }
    }

    public void BuildServiceProvider()
    {
        _serviceProvider= _serviceCollection.BuildServiceProvider();
    }

    protected abstract IServiceCollection GetServiceCollection();       

    public virtual void Dispose()
    {
        _serviceProvider?.Dispose();
    }
}
