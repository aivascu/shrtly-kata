using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShrtLy.Api.Controllers;
using ShrtLy.BLL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShrtLy.UnitTest
{
    public class ControllerTests
    {
        public LinksController controller;
        public Mock<IShorteningService> serviceMock;

        public static List<LinkDto> viewModels = new List<LinkDto>
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

        public static List<LinkDto> linkDtos = new List<LinkDto>
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

        [SetUp]
        public void Setup()
        {
            serviceMock = new Mock<IShorteningService>();
            controller = new LinksController(serviceMock.Object);
        }

        [Test]
        public async Task GetShortLink_ProcessLinkHasBeenCalled()
        {
            this.controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            this.controller.ControllerContext.HttpContext.Request.Scheme = "http";
            this.controller.ControllerContext.HttpContext.Request.Host = new HostString("localhost", 5000);
            this.controller.ControllerContext.HttpContext.Request.Path = "/api/links/";

            this.serviceMock
                .Setup(s => s.GenerateLinkAsync(It.IsAny<string>())).Returns(Task.FromResult(new LinkDto { Id = 1 }));


            await controller.CreateLink("http://google.com");

            serviceMock.Verify(x => x.GenerateLinkAsync("http://google.com"), Times.Once);
        }

        [Test]
        public async Task GetShortLink_ProcessLinksHasBeenCalled()
        {
            serviceMock.Setup(x => x.GetLinksAsync()).Returns(Task.FromResult(new List<LinkDto>().AsEnumerable()));

            await controller.ListLinks();

            serviceMock.Verify(x => x.GetLinksAsync(), Times.Once);
        }

        [Test]
        public async Task GetShortLinks_AllLinksAreCorrect()
        {
            serviceMock.Setup(x => x.GetLinksAsync()).Returns(Task.FromResult(new List<LinkDto>().AsEnumerable()));

            await controller.ListLinks();

            for (int i = 0; i < linkDtos.Count; i++)
            {
                Assert.AreEqual(viewModels[i].Id, linkDtos[i].Id);
                Assert.AreEqual(viewModels[i].ShortUrl, linkDtos[i].ShortUrl);
                Assert.AreEqual(viewModels[i].Url, linkDtos[i].Url);
            }
        }
    }
}
