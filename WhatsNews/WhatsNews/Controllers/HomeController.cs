using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;

namespace WhatsNews.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticleRepository _articleRepository;

    public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
    {
        _logger = logger;
        _articleRepository = articleRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AllArticles()
    {
        var articles = _articleRepository.GetArticles();
        return Json(articles);
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