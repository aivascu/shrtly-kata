using Microsoft.AspNetCore.Mvc;
using ShrtLy.Api.ViewModels;
using ShrtLy.BLL;
using System.Collections.Generic;
using System.Linq;

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
        public string GetShortLink(string url)
        {
            return service.ProcessLink(url);
        }

        [HttpGet("all")]
        public IEnumerable<LinkViewModel> GetShortLinks()
        {
            return service.GetShortLinks().Select(e => 
                new LinkViewModel
                {
                    Id = e.Id, 
                    ShortUrl = e.ShortUrl, 
                    Url = e.Url
                }).ToList();
        }
    }
}
