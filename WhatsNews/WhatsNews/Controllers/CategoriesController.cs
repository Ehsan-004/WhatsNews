using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IArticleRepository _articleRepository;

    public CategoriesController(ICategoryRepository categoryRepository, IArticleRepository articleRepository)
    {
        _categoryRepository = categoryRepository;
        _articleRepository = articleRepository;
    }

    public async Task<IActionResult> CategoryArticles(int categoryId)
    {
        var category = _categoryRepository.GetCategoryById(categoryId);
        var articles = _articleRepository.GetByCategory(category);
        return View(articles);
    }
}