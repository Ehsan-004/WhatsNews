using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;

namespace WhatsNews.ViewComponents;

public class CategoriesViewComponent : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesViewComponent(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(int count = 10)
    {
        var categories = await _categoryRepository.GetCategoriesAsync(count);
        return View(categories);
    }
}