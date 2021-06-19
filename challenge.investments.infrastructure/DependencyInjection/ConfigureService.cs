using challenge.investments.domain.Interfaces.Repository;
using challenge.investments.domain.Interfaces.Services;
using challenge.investments.infrastructure.Repository;
using challenge.investments.service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace challenge.investments.infrastructure.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IInvestimentosService, InvestimentosService>();
            serviceCollection.AddHttpClient<IFundosRepository, FundosRepository>();
            serviceCollection.AddHttpClient<ILcisRepository, LcisRepository>();
            serviceCollection.AddHttpClient<ITesouroDiretoRepository, TesouroDiretoRepository>();
        }
    }
}
