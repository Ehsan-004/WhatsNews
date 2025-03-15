using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticleRepository _articleRepository;

    public ArticlesController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    [HttpGet]
    [Route("/article/{id}")]
    public IActionResult Show(int id)
    {
        var article = _articleRepository.GetById(id);
        if (article == null)
            return NotFound();
        return View(article);
    }

}