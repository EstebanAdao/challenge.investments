using System.Threading.Tasks;
using challenge.investments.api.Controllers;
using challenge.investments.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace challenge.investments.tests.unit.Application.Investimentos
{
    public class WhenForExecutedGetController : InvestimentosTests
    {
        [Fact]
        public async Task IsPossibleExecuteMethodGet_ReturnOkRequest()
        {
            //Arrange
            _serviceMock.Setup(service => service.Get()).ReturnsAsync(() =>
            {
                return investimentos;
            });

            var controller = new InvestimentosController(_serviceMock.Object);

            //Act
            var result = await controller.Get();

            //Assert
            _serviceMock.Verify(x => x.Get(), Times.Once);
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task IsPossibleExecuteMethodGet_ReturnNotFound()
        {
            //Arrange
            _serviceMock.Setup(service => service.Get()).ReturnsAsync(() =>
            {
                return new InvestimentosModel();
            });

            var controller = new InvestimentosController(_serviceMock.Object);

            //Act
            var result = await controller.Get();

            //Assert
            _serviceMock.Verify(x => x.Get(), Times.Once);
            var notFound = result as NotFoundResult;
            Assert.NotNull(notFound);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}
