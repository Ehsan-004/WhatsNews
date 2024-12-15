using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;
using WhatsNews.Models.Context;
using WhatsNews.Models.Enum;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WhatsNews.Data.Services;

public class ArticleService : IArticleRepository
{
    private readonly ApContext _context;
    private readonly IHostingEnvironment _hostingEnvironment;

    public ArticleService(ApContext context, IHostingEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    public IEnumerable<Article> GetArticles()
    {
        return _context.Articles.ToList();
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public IEnumerable<Article> GetIndexed1(int num = 5)
    {
        return _context.Articles.Where(a => a.Index1 == true).Take(num).ToList();
    }

    public IEnumerable<Article> GetIndexed2(int num = 5)
    {
        return _context.Articles.Where(a => a.Index2 == true).Take(num).ToList();
    }

    public IEnumerable<Article> GetIndexed3(int num = 5)
    {
        return _context.Articles.Where(a => a.Index3 == true).Take(num).ToList();
    }

    public IEnumerable<Article> GetMostViewed(int num = 5)
    {
        return _context.Articles.OrderByDescending(a => a.Views).Take(num).ToList();
    }

    public IEnumerable<Article> GetRecent(int num = 5)
    {
        return _context.Articles.OrderByDescending(a => a.PublishDate).Take(num).ToList();
    }

    public IEnumerable<Article> GetByType(AType type)
    {
        return _context.Articles.Where(a => a.AType == type).ToList();
    }

    public IEnumerable<Article> GetByCategory(int categoryId)
    {
        return _context.Articles.Where(a=>a.CategoryId ==categoryId).ToList();
    }

    public Article GetById(int id)
    {
        return _context.Articles.FirstOrDefault(a => a.Id == id);
    }

    public bool IncreaseViews(int id)
    {
        var article = GetById(id);
        article.Views++;
        return Save();
    }

    public bool Create(Article article)
    {
        _context.Articles.Add(article);
        return Save();
    }

    public bool Update(Article article)
    {
        _context.Articles.Update(article);
        return Save();
    }

    public bool Delete(Article article)
    {
        var imagePath = article.ImagePath;
        if (!string.IsNullOrEmpty(imagePath))
        {
            var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, imagePath.TrimStart('/'));
            Console.WriteLine(oldImagePath);

            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
        }
        _context.Remove(article);
        return Save();
    }
    

    public bool Save()
    {
        return (_context.SaveChanges() > 0);
    }
}