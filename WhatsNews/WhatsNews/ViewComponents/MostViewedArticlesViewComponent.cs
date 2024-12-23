using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.ViewComponents;

public class MostViewedArticlesViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public MostViewedArticlesViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var articles = _context.GetMostViewed();
        return View(articles);
    }
}