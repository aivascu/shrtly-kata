using Microsoft.AspNetCore.Mvc;
using ShrtLy.Api.ViewModels;
using ShrtLy.BLL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShrtLy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IShorteningService service;

        public LinksController(IShorteningService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<string> GetShortLink(string url)
        {
            return await service.ProcessLink(url);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<LinkViewModel>> GetShortLinks()
        {
            var shortLinks = await service.GetShortLinks();
            return shortLinks.Select(e => 
                new LinkViewModel
                {
                    Id = e.Id, 
                    ShortUrl = e.ShortUrl, 
                    Url = e.Url
                }).ToList();
        }
    }
}
