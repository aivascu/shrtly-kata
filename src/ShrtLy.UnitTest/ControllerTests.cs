using Moq;
using NUnit.Framework;
using ShrtLy.Api.Controllers;
using ShrtLy.BLL;
using System.Collections.Generic;
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
        public async void GetShortLink_ProcessLinkHasBeenCalled()
        {
            await controller.GetShortLink("http://google.com");

            serviceMock.Verify(x => x.GenerateLinkAsync("http://google.com"), Times.Once);
        }

        [Test]
        public async void GetShortLink_ProcessLinksHasBeenCalled()
        {
            serviceMock.Setup(x => x.GetLinksAsync()).Returns(async () => await Task.Run(() => new List<LinkDto>()));

            await controller.GetShortLinks();

            serviceMock.Verify(x => x.GetLinksAsync(), Times.Once);
        }

        [Test]
        public async void GetShortLinks_AllLinksAreCorrect()
        {
            serviceMock.Setup(x => x.GetLinksAsync()).Returns(async () => await Task.Run(() => new List<LinkDto>()));

            await controller.GetShortLinks();

            for (int i = 0; i < linkDtos.Count; i++)
            {
                Assert.AreEqual(viewModels[i].Id, linkDtos[i].Id);
                Assert.AreEqual(viewModels[i].ShortUrl, linkDtos[i].ShortUrl);
                Assert.AreEqual(viewModels[i].Url, linkDtos[i].Url);
            }
        }
    }
}
