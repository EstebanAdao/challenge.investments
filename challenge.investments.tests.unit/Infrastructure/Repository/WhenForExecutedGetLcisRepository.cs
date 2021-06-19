using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using challenge.investments.domain.Entities;
using challenge.investments.infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Xunit;

namespace challenge.investments.tests.unit.Infrastructure.Repository
{
    public class WhenForExecutedGetLcisRepository : RepositoryTests
    {

        [Fact]
        public async Task IsPossibleGetLcisRepository_ReturnData()
        {
            //Arrange
            var lcis = fixture.Create<LCIsModel>();

            //Setup method HttpMessageHandler
            _httMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
            {
                var response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonSerializer.Serialize(lcis));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return response;
            });
            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);

            var httpClient = new HttpClient(_httMessageHandlerMock.Object)
            {
                BaseAddress = fixture.Create<Uri>()
            };

            var lcisRepository = new LcisRepository(httpClient, _configurationMock.Object, memoryCache);

            //Act
            var result = await lcisRepository.Get();

            //Assert
            _httMessageHandlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
            Assert.NotEmpty(result.Lcis);
        }

        [Fact]
        public async Task IsPossibleGetLcisRepository_ReturnNoData()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            //Setup method HttpMessageHandler
            _httMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);

            var httpClient = new HttpClient(_httMessageHandlerMock.Object)
            {
                BaseAddress = fixture.Create<Uri>()
            };

            var lcisRepository = new LcisRepository(httpClient, _configurationMock.Object, memoryCache);

            //Act
            var result = await lcisRepository.Get();

            //Assert
            _httMessageHandlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
            Assert.Empty(result.Lcis);
        }
    }
}
