using WhatsNews.Models;

namespace WhatsNews.Data.Interfaces;

public interface ICategoryRepository
{
    public IEnumerable<Category> GetCategories();
    public Category GetCategoryById(int id);
    public bool Create(Category category);
    public bool Update(Category category);
    public bool Delete(Category category);
    public bool Save();
}