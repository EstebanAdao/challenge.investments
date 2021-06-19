using System.Net.Http;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;

namespace challenge.investments.tests.unit.Infrastructure.Repository
{
    public class RepositoryTests
    {
        protected readonly Mock<HttpMessageHandler> _httMessageHandlerMock;
        protected readonly Mock<IConfiguration> _configurationMock;
        protected readonly Fixture fixture;
        protected readonly MemoryCache memoryCache;

        public RepositoryTests()
        {
            _httMessageHandlerMock = new Mock<HttpMessageHandler>();
            _configurationMock = new Mock<IConfiguration>();
            fixture = new Fixture();
            memoryCache = new MemoryCache(new MemoryCacheOptions());
        }
    }
}
