using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.ViewComponents;

public class RecentArticlesViewComponent : ViewComponent
{
    private readonly IArticleRepository _articleRepository;

    public RecentArticlesViewComponent(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int count = 8)
    {
        var articles = await _articleRepository.GetArticlesAsync(count);
        return View(articles);
    }
}