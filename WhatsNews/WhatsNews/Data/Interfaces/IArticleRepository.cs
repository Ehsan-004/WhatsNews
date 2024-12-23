using WhatsNews.Models;
using WhatsNews.Models.Enum;

namespace WhatsNews.Data.Interfaces;

public interface IArticleRepository
{
    public IEnumerable<Article> GetArticles();
    public IEnumerable<Article> GetArticlesWithCategory();
    public Task<IEnumerable<Article>> GetArticlesAsync();
    public Task<IEnumerable<Article>> GetIndexed1Async(int num = 5);
    public Task<IEnumerable<Article>> GetIndexed2Async(int num = 5);
    public IQueryable<Article> GetIndexed2AsQueryable(int num = 5);
    public Task<IEnumerable<Article>> GetIndexed3Async(int num = 5);
    public IQueryable<Article> GetIndexed3AsQueryable(int num = 5);
    public IEnumerable<Article> GetMostViewed(int num = 5);
    public IEnumerable<Article> GetRecent(int num = 5);
    public Task<IEnumerable<Article>> GetRecentAsync(int num = 5);
    public Task<IEnumerable<Article>> GetByTypeAsync(AType type);
    public IEnumerable<Article> GetByCategory(Category category);
    public Article GetById(int id);
    public Article GetArticleWithCategory(int id);
    public Article GetArticleNoTracking(int id);
    public bool ArticleExists(int id);
    public bool IncreaseViews(int id);
    public bool Create(Article article);
    public bool Update(Article article);
    public bool Delete(Article article);
    public bool Save();
}
