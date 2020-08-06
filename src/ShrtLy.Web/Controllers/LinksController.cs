using Microsoft.AspNetCore.Mvc;
using ShrtLy.Api.ViewModels;
using ShrtLy.BLL;

namespace ShrtLy.Web.Controllers;

public class LinksController : Controller
{
    private readonly IShorteningService service;

    public LinksController(IShorteningService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var dtos = service.GetShortLinks();

        List<LinkViewModel> viewModels = new List<LinkViewModel>();
        for (int i = 0; i < dtos.Count(); i++)
        {
            var element = dtos.ElementAt(i);
            viewModels.Add(new LinkViewModel
            {
                Id = element.Id,
                ShortUrl = element.ShortUrl,
                Url = element.Url
            });
        }

        return View(viewModels);
    }
}
