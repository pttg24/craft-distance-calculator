namespace craft.Tests.Services
{
    using System.Threading.Tasks;
    using AutoFixture;
    using CSharpFunctionalExtensions;
    using Moq;
    using craft.Services;
    using Domain;
    using NUnit.Framework;
    using Shouldly;
    using craft.Configuration;

    public class PostCodeServiceTests
    {
        private const string Error = "error";
        private readonly Fixture _fixture = new Fixture();
        private Mock<IPostCodeServiceHandler> _postCodeServiceHandler;

        [SetUp]
        public void SetUp()
        {
            _postCodeServiceHandler = new Mock<IPostCodeServiceHandler>();
        }

        [Test]
        public async Task ShouldReturnPostCodesIfPostCodeServiceHandlerSucceeds()
        {
            var postCodeResponse = _fixture.Create<PostCodeResponse>();
            _postCodeServiceHandler
                .Setup(x => x.GetPostCodes(It.IsAny<string>()))
                .ReturnsAsync(Result.Ok(postCodeResponse));
            var postCodeService = new PostCodeService(
                _postCodeServiceHandler.Object,
                new DatabaseConfiguration
                { 
                    CollectionName = "PostCodes",
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "PostCodesDb"
                });

            var result = await postCodeService.GetPostCodes(It.IsAny<string>());

            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBeOfType<PostCodeResponse>();
        }

        [Test]
        public async Task ShouldReturnFailureIfClientHandlerFails()
        {
            _postCodeServiceHandler
                .Setup(x => x.GetPostCodes(It.IsAny<string>()))
                .ReturnsAsync(Result.Fail<PostCodeResponse>(Error));
            var postCodeService = new PostCodeService(
                _postCodeServiceHandler.Object,
                new DatabaseConfiguration
                {
                    CollectionName = "PostCodes",
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "PostCodesDb"
                });

            var result = await postCodeService.GetPostCodes(It.IsAny<string>());

            result.IsFailure.ShouldBeTrue();
        }
    }
}
