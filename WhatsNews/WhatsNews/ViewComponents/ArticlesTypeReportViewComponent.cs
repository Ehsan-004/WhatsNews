using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models.Enum;

namespace WhatsNews.ViewComponents;

public class ArticlesTypeReportViewComponent : ViewComponent
{
    private readonly IArticleRepository _context;

    public ArticlesTypeReportViewComponent(IArticleRepository context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var articless = await _context.GetByTypeAsync(AType.Report);
        return View(articless);
    }
}