using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;

namespace WhatsNews.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private readonly ICategoryRepository _context;

    public CategoriesController(ICategoryRepository context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.GetCategories();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid) return View();

        if (_context.Create(category))
        {
            return RedirectToAction("index");
        }

        return View();
    }

    public IActionResult Edit(int id)
    {
        var category = _context.GetCategoryById(id);
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        
        _context.Update(category);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var category = _context.GetCategoryById(id);
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _context.GetCategoryById(id);
        if (category == null)
            return RedirectToAction("index");
        _context.Delete(category);
        return RedirectToAction("index");
    }
}