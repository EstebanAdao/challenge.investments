using Bogus;
using challenge.investments.domain.Entities;
using challenge.investments.domain.Interfaces.Repository;
using challenge.investments.domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace challenge.investments.tests.unit.Application.Investimentos
{
    public class InvestimentosTests
    {
        protected readonly InvestimentosModel investimentos;
        protected readonly Mock<ILcisRepository> _lcisRepositoryMock;
        protected readonly Mock<IFundosRepository> _fundosRepositoryMock;
        protected readonly Mock<ITesouroDiretoRepository> _tesourosRepositoryMock;
        protected readonly Mock<IInvestimentosService> _serviceMock;
        protected readonly MemoryCache memoryCache;

        public InvestimentosTests()
        {
            _lcisRepositoryMock = new Mock<ILcisRepository>();
            _fundosRepositoryMock = new Mock<IFundosRepository>();
            _tesourosRepositoryMock = new Mock<ITesouroDiretoRepository>();
            _serviceMock = new Mock<IInvestimentosService>();
            memoryCache = new MemoryCache(new MemoryCacheOptions());

            var faker = new Faker("pt_BR");
            investimentos = new InvestimentosModel();
            investimentos.ValorTotal = faker.Random.Decimal();
            for (int i = 0; i < 8; i++)
            {
                var investimento = new Investimento
                {
                    Nome = faker.Company.CompanyName(),
                    Ir = faker.Random.Decimal(),
                    ValorInvestido = faker.Random.Decimal(),
                    ValorResgate = faker.Random.Decimal(),
                    ValorTotal = faker.Random.Decimal(),
                    Vencimento = faker.Date.Future()
                };
                investimentos.Investimentos.Add(investimento);
            }
        }
    }
}
