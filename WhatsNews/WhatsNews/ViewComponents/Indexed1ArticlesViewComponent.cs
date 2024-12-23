using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.ViewComponents;

public class Indexed1ArticlesViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public Indexed1ArticlesViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var articles = await _context.GetIndexed2Async();
        return View(articles);
    }
}