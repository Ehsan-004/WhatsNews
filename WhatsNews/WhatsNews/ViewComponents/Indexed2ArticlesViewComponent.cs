using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.ViewModels;

namespace WhatsNews.ViewComponents;

public class Indexed2ArticlesViewComponent:ViewComponent
{
    private readonly IArticleRepository _context;

    public Indexed2ArticlesViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var indexedArticles = _context.GetIndexed2AsQueryable();
        var largeArticles = indexedArticles.Take(1).ToList();
        var smallArticles = indexedArticles.Skip(1).Take(3).ToList();
        var indexed1 = new IndexedArticlesViewModel
        {
            SmallArticles = smallArticles,
            LargeArticles = largeArticles
        };

    return View(indexed1);
    }
}