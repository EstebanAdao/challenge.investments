using System.Net;
using System.Threading.Tasks;
using challenge.investments.domain.Interfaces.Services;
using challenge.investments.tests.integrations.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace challenge.investments.tests.integrations.Controller
{
    public class WhenForExecutedInvestimentosController : IntegrationTest
    {
        public WhenForExecutedInvestimentosController(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task IsPossibleGetAllInvestments()
        {
            //Arrange
            var request = "/api/investimentos";

            // Act
            var response = await _client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(content);
        }

        [Fact]
        public async Task IsNotPossibleGetAllInvestments()
        {
            //Arrange
            var clientNotFound = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<IInvestimentosService, InvalidInvestimentosService>();
                });
            })
            .CreateClient();

            var request = "/api/investimentos";

            // Act
            var response = await clientNotFound.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(content);
        }
    }
}
