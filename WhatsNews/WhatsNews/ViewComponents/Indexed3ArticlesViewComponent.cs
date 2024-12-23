using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WhatsNews.Data.Interfaces;
using WhatsNews.ViewModels;

namespace WhatsNews.ViewComponents;

public class Indexed3ArticlesViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public Indexed3ArticlesViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var indexedArticles = _context.GetIndexed3AsQueryable();
        var largeArticles = indexedArticles.Take(2).ToList();
        var smallArticles = indexedArticles.Skip(2).Take(4).ToList();
        var indexed1 = new IndexedArticlesViewModel
        {
            SmallArticles = smallArticles,
            LargeArticles = largeArticles
        };

        return View(indexed1);
    }
}