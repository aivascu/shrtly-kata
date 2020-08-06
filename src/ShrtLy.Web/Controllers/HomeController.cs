using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShrtLy.BLL;
using ShrtLy.DAL;
using ShrtLy.Web.Models;

namespace ShrtLy.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IShorteningService service;
    private readonly ShrtLyContext context;

    public HomeController(ILogger<HomeController> logger, IShorteningService service, ShrtLyContext context)
    {
        _logger = logger;
        this.service = service;
        this.context = context;
    }

    [HttpGet("{id}")]
    public IActionResult Index(string id)
    {
        var link = service.GetShortLinks().FirstOrDefault(x => x.ShortUrl == id);
        return Redirect(link.Url);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new ShortenModel());
    }

    [HttpPost]
    public IActionResult Shorten(ShortenModel model)
    {
        var shortenedUrl = service.ProcessLink(model.Url);
        context.Links.Add(new LinkEntity { Url = model.Url, ShortUrl = shortenedUrl });
        context.SaveChanges();
        TempData["ShortenedUrl"] = shortenedUrl;
        return RedirectToAction("Index");
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}