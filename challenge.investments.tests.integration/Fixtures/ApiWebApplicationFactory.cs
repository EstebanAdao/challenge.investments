using System;
using challenge.investments.api;
using challenge.investments.domain.Interfaces.Repository;
using challenge.investments.domain.Interfaces.Services;
using challenge.investments.infrastructure.Repository;
using challenge.investments.service.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace challenge.investments.tests.integrations.Fixtures
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public IConfiguration Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

            builder.ConfigureAppConfiguration(config =>
            {
                Configuration = new ConfigurationBuilder()
                  .AddJsonFile("integrationsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"integrationsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables()
                  .Build();

                config.AddConfiguration(Configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IInvestimentosService, InvestimentosService>();
                services.AddHttpClient<IFundosRepository, FundosRepository>();
                services.AddHttpClient<ILcisRepository, LcisRepository>();
                services.AddHttpClient<ITesouroDiretoRepository, TesouroDiretoRepository>();
            });
        }
    }
}
