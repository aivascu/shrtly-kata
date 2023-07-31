using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShrtLy.Api.Controllers;
using ShrtLy.BLL.Dtos;
using ShrtLy.BLL.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShrtLy.UnitTest.Controllers
{
    public class LinksControllerTests
    {
        private readonly LinksController _linksController;
        private readonly Mock<IShorteningService> _shorteningServiceMock;
        private readonly List<LinkDto> _links = new()
        {
            new LinkDto
            {
                Id = 1,
                ShortUrl = "short-url-1",
                Url = "url-1"
            },
            new LinkDto
            {
                Id = 2,
                ShortUrl = "short-url-2",
                Url = "url-2"
            }
        };

        public LinksControllerTests()
        {
            _shorteningServiceMock = new Mock<IShorteningService>();
            _linksController = new LinksController(_shorteningServiceMock.Object);
        }

        [Fact]
        public async Task Creates_a_new_link_based_on_a_valid_url()
        {
            // Arrange
            _linksController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _linksController.ControllerContext.HttpContext.Request.Scheme = "http";
            _linksController.ControllerContext.HttpContext.Request.Host = new HostString("localhost", 5000);
            _linksController.ControllerContext.HttpContext.Request.Path = "/api/links/";

            var generatedLinkId = 1;
            _shorteningServiceMock
                .Setup(s => s.GenerateLinkAsync(It.IsAny<string>())).Returns(Task.FromResult(new LinkDto { Id = generatedLinkId }));

            var validUrl = "https://www.google.com/";

            // Act
            var result = await _linksController.CreateLink(validUrl) as CreatedResult;

            // Assert
            _shorteningServiceMock.Verify(x => x.GenerateLinkAsync(validUrl), Times.Once);
            result.Should().NotBeNull().And.BeAssignableTo<CreatedResult>();
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            result.Location.Should().NotBeNullOrEmpty().And.EndWith(generatedLinkId.ToString());
        }

        [Fact]
        public async Task Rejects_to_create_a_new_link_using_an_invalid_url()
        {
            // Arrange
            _linksController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _linksController.ControllerContext.HttpContext.Request.Scheme = "http";
            _linksController.ControllerContext.HttpContext.Request.Host = new HostString("localhost", 5000);
            _linksController.ControllerContext.HttpContext.Request.Path = "/api/links/";

            var generatedLinkId = 1;
            _shorteningServiceMock
                .Setup(s => s.GenerateLinkAsync(It.IsAny<string>())).Returns(Task.FromResult(new LinkDto { Id = generatedLinkId }));

            var invalidUrl = "1invalid-url1.cg";

            // Act
            var result = await _linksController.CreateLink(invalidUrl) as BadRequestResult;

            // Assert
            _shorteningServiceMock.Verify(x => x.GenerateLinkAsync(invalidUrl), Times.Never);
            result.Should().NotBeNull().And.BeAssignableTo<BadRequestResult>();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Gets_all_generated_links()
        {
            // Arrange
            _shorteningServiceMock.Setup(s => s.GetLinksAsync()).Returns(Task.FromResult(_links.AsEnumerable()));

            // Act
            var result = await _linksController.ListLinks() as OkObjectResult;
            var actualLinks = result.Value as List<LinkDto>;
            var expectedLinks = _links;

            // Assert
            _shorteningServiceMock.Verify(x => x.GetLinksAsync(), Times.Once);
            result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            actualLinks.Should().NotBeNull().And.HaveCount(expectedLinks.Count);
            actualLinks.Should().BeEquivalentTo(expectedLinks);
        }
    }
}
