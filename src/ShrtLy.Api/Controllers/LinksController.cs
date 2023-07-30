using Microsoft.AspNetCore.Mvc;
using ShrtLy.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShrtLy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IShorteningService _shorteningService;

        public LinksController(IShorteningService service)
        {
            this._shorteningService = service;
        }

        [HttpGet]
        public async Task<LinkDto> GetShortLink(string url)
        {
            return await _shorteningService.GenerateLinkAsync(url);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<LinkDto>> GetShortLinks()
        {
            return await _shorteningService.GetLinksAsync();
        }
    }
}
