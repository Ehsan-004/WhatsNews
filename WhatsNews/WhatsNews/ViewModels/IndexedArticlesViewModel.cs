using WhatsNews.Models;

namespace WhatsNews.ViewModels;

public class IndexedArticlesViewModel
{
    public List<Article> LargeArticles { get; set; }
    public List<Article> SmallArticles { get; set; }
}