using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShrtLy.BLL;
using System;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LinkDto))]
        public async Task<IActionResult> CreateLink(string url)
        {
            var createdLink = await _shorteningService.GenerateLinkAsync(url);
            var uri = new Uri(HttpContext.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Path) + "/" + createdLink.Id;
            return Created(uri, createdLink);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LinkDto>))]
        public async Task<IActionResult> ListLinks()
        {
            return Ok(await _shorteningService.GetLinksAsync());
        }
    }
}
