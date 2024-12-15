using WhatsNews.Models;
using WhatsNews.Models.Enum;

namespace WhatsNews.Data.Interfaces;

public interface IArticleRepository
{
    public IEnumerable<Article> GetArticles();
    public Task<IEnumerable<Article>> GetArticlesAsync();
    public IEnumerable<Article> GetIndexed1(int num = 5);
    public IEnumerable<Article> GetIndexed2(int num = 5);
    public IEnumerable<Article> GetIndexed3(int num = 5);
    public IEnumerable<Article> GetMostViewed(int num = 5);
    public IEnumerable<Article> GetRecent(int num = 5);
    public IEnumerable<Article> GetByType(AType type);
    public IEnumerable<Article> GetByCategory(int categoryId);
    public Article GetById(int id);
    public bool IncreaseViews(int id);
    public bool Create(Article article);
    public bool Update(Article article);
    public bool Delete(Article article);
    public bool Save();
}
