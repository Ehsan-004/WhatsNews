using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
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

    public IEnumerable<Article> GetArticlesWithCategory()
    {
        return _context.Articles.Include(a => a.Category).ToList();
    }

    public Article GetArticleWithCategory(int id)
    {
        return _context.Articles.Include(a => a.Category).SingleOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetIndexed1Async(int num = 5)
    {
        return await _context.Articles.Where(a => a.Index1 == true).Take(num).ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetIndexed2Async(int num = 5)
    {
        return await _context.Articles.Where(a => a.Index2 == true).Take(num).ToListAsync();
    }

    public IQueryable<Article> GetIndexed2AsQueryable(int num = 5)
    {
        return _context.Articles.Where(a => a.Index2 == true).Take(num);
    }

    public async Task<IEnumerable<Article>> GetIndexed3Async(int num = 5)
    {
        return await _context.Articles.Where(a => a.Index3 == true).Take(num).ToListAsync();
    }

    public IQueryable<Article> GetIndexed3AsQueryable(int num = 5)
    {
        return _context.Articles.Where(a => a.Index3 == true).Take(num);
    }

    public IEnumerable<Article> GetMostViewed(int num = 5)
    {
        return _context.Articles.AsNoTracking().OrderByDescending(a => a.Views).Take(num).ToList();
    }

    public IEnumerable<Article> GetRecent(int num = 5)
    {
        return _context.Articles.OrderByDescending(a => a.PublishDate).Take(num).ToList();
    }

    public async Task<IEnumerable<Article>> GetRecentAsync(int num = 5)
    {
        return await _context.Articles.OrderByDescending(a => a.PublishDate).Take(num).ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetByTypeAsync(AType type)
    {
        return await _context.Articles.Where(a => a.AType == type).ToListAsync();
    }

    public IEnumerable<Article> GetByCategory(Category category)
    {
        return _context.Articles.Where(a=>a.Category ==category).ToList();
    }

    public Article GetById(int id)
    {
        return _context.Articles.FirstOrDefault(a => a.Id == id);
    }

    public Article GetArticleNoTracking(int id)
    {
        return _context.Articles.AsNoTracking().FirstOrDefault(a => a.Id == id);
    }

    public bool ArticleExists(int id)
    {
        return (_context.Articles.AsNoTracking().Any(a => a.Id == id));
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