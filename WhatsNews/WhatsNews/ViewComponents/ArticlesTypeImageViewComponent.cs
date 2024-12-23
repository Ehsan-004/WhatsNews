using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models.Enum;

namespace WhatsNews.ViewComponents;

public class ArticlesTypeImageViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public ArticlesTypeImageViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var imageArticles = await _context.GetByTypeAsync(AType.Picture);
        return View(imageArticles);
    }
}