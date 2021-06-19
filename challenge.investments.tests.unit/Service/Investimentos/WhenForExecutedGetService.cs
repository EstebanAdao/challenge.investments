using System.Threading.Tasks;
using AutoFixture;
using challenge.investments.domain.Entities;
using challenge.investments.service.Service;
using Moq;
using Xunit;

namespace challenge.investments.tests.unit.Service.Investimentos
{
    public class WhenForExecutedGetService : InvestimentosTests
    {
        [Fact]
        public async Task IsPossibelGetAllInvestments()
        {
            //Arrange
            var fixture = new Fixture();
            var lcis = fixture.Create<LCIsModel>();
            var fundos = fixture.Create<FundosModel>();
            var tesouros = fixture.Create<TesourosModel>();

            _lcisRepositoryMock.Setup(x => x.Get()).ReturnsAsync(lcis);
            _fundosRepositoryMock.Setup(x => x.Get()).ReturnsAsync(fundos);
            _tesourosRepositoryMock.Setup(x => x.Get()).ReturnsAsync(tesouros);

            var investimentosService = new InvestimentosService(_fundosRepositoryMock.Object, _lcisRepositoryMock.Object, _tesourosRepositoryMock.Object, memoryCache);

            //Act
            var result = await investimentosService.Get();

            //Assert
            _lcisRepositoryMock.Verify(x => x.Get(), Times.Once);
            _fundosRepositoryMock.Verify(x => x.Get(), Times.Once);
            _tesourosRepositoryMock.Verify(x => x.Get(), Times.Once);
            Assert.NotEmpty(result.Investimentos);
        }

        [Fact]
        public async Task IsNotPossibelGetAllInvestments()
        {
            //Arrange
            _lcisRepositoryMock.Setup(x => x.Get()).ReturnsAsync(new LCIsModel());
            _fundosRepositoryMock.Setup(x => x.Get()).ReturnsAsync(new FundosModel());
            _tesourosRepositoryMock.Setup(x => x.Get()).ReturnsAsync(new TesourosModel());

            var investimentosService = new InvestimentosService(_fundosRepositoryMock.Object, _lcisRepositoryMock.Object, _tesourosRepositoryMock.Object, memoryCache);

            //Act
            var result = await investimentosService.Get();

            //Assert
            _lcisRepositoryMock.Verify(x => x.Get(), Times.Once);
            _fundosRepositoryMock.Verify(x => x.Get(), Times.Once);
            _tesourosRepositoryMock.Verify(x => x.Get(), Times.Once);
            Assert.Empty(result.Investimentos);
        }
    }
}
