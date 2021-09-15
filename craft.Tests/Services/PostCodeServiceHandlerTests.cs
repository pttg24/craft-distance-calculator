namespace craft.Tests.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture;
    using craft.Services;
    using craft.Domain;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;
    using Shouldly;

    public class PostCodeServiceHandlerTests
    {
        private const string InvalidJson = "}{";
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<HttpMessageHandler> _httpMessageHandler = new Mock<HttpMessageHandler>();

        [Test]
        public async Task ShouldReturnPostCodeResponse()
        {
            var postCodeResponseDetails = new PostCodeResponseDetails
            {
                AdminCounty = "test",
                Region = "test",
                Country = "test",
                Latitude = 51.033f,
                Longitude = 0.007f
            };

            var postCodeResponse = new PostCodeResponse
            {
                Result = postCodeResponseDetails,
                Status = "OK"
            };

            SetupHttpResponseMessage(HttpStatusCode.OK, postCodeResponse);

            var result = await GetSut().GetPostCodes(_fixture.Create<string>());

            result.IsSuccess.ShouldBeTrue();
            result.Value.Status.ShouldBe("OK");
            //result.Value.Result.ShouldBeEquivalentTo(postCodeResponseDetails);
        }

        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.Unauthorized)]
        [TestCase(HttpStatusCode.UnprocessableEntity)]
        [TestCase(HttpStatusCode.TooManyRequests)]
        public async Task ShouldReturnFailureIfResponseNotOk(HttpStatusCode httpStatusCode)
        {
            SetupHttpResponseMessage(httpStatusCode, "");

            var result = await GetSut().GetPostCodes(_fixture.Create<string>());

            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public async Task ShouldReturnFailureIfHttpRequestCannotBeDeserialized()
        {
            SetupHttpResponseMessage(HttpStatusCode.OK, InvalidJson);

            var result = await GetSut().GetPostCodes(_fixture.Create<string>());

            result.IsFailure.ShouldBeTrue();
        }

        private PostCodeServiceHandler GetSut() => new PostCodeServiceHandler(GetHttpClient());

        private void SetupHttpResponseMessage<T>(HttpStatusCode httpStatusCode, T content)
        {
            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = httpStatusCode,
                    Content = JsonContent.Create(content,
                        options: new JsonSerializerOptions()),
                });
        }

        private HttpClient GetHttpClient() =>
            new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost/endpoint")
            };
    }
}
