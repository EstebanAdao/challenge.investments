using challenge.investments.domain.Interfaces.Repository;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace challenge.investments.tests.unit.Service.Investimentos
{
    public class InvestimentosTests
    {

        protected readonly Mock<ILcisRepository> _lcisRepositoryMock;
        protected readonly Mock<IFundosRepository> _fundosRepositoryMock;
        protected readonly Mock<ITesouroDiretoRepository> _tesourosRepositoryMock;
        protected readonly MemoryCache memoryCache;

        public InvestimentosTests()
        {
            _lcisRepositoryMock = new Mock<ILcisRepository>();
            _fundosRepositoryMock = new Mock<IFundosRepository>();
            _tesourosRepositoryMock = new Mock<ITesouroDiretoRepository>();
            memoryCache = memoryCache = new MemoryCache(new MemoryCacheOptions());
        }
    }
}
