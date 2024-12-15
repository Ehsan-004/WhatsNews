using WhatsNews.Data.Interfaces;
using WhatsNews.Models;
using WhatsNews.Models.Context;

namespace WhatsNews.Data.Services;

public class CategoryService : ICategoryRepository
{
    private readonly ApContext _context;

    public CategoryService(ApContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.Id == id);
    }

    public bool Create(Category category)
    {
        _context.Categories.Add(category);
        return Save();
    }

    public bool Update(Category category)
    {
        _context.Categories.Update(category);
        return Save();
    }

    public bool Delete(Category category)
    {
        _context.Categories.Remove(category);
        return Save();
    }

    public bool Save()
    {
        return (_context.SaveChanges() >= 0);
    }
}