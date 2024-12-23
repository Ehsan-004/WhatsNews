using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models.Enum;

namespace WhatsNews.ViewComponents;

public class ArticlesTypetextViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public ArticlesTypetextViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var articles = await _context.GetByTypeAsync(AType.Report);
        return View(articles);
    }
}