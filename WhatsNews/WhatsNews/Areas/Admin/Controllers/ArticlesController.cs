using Microsoft.AspNetCore.Mvc;
using WhatsNews.Areas.Admin.ViewModels;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;

namespace WhatsNews.Areas.Admin.Controllers;

[Area("Admin")]
public class ArticlesController : Controller
{
    private readonly IArticleRepository _context;
    private readonly ICategoryRepository _categoryContext;
    private readonly IWebHostEnvironment _environment;

    public ArticlesController(
        IArticleRepository context, 
        ICategoryRepository categoryContext, 
        IWebHostEnvironment environment)
    {
        _context = context;
        _categoryContext = categoryContext;
        _environment = environment;
    }

    public IActionResult Index()
    {
        var articles = _context.GetArticlesWithCategory();
        return View(articles);
    }

    public IActionResult all()
    {
        var articles = _context.GetArticles();
        return Json(new { articles = articles });
    }
    
    public IActionResult Create()
    {
        var categories = _categoryContext.GetCategories();
        
        var articleVm = new ArticleCategoryVm
        {
            Categories = categories,
        };
        return View(articleVm);
    }

    [HttpPost]
    public IActionResult Create(ArticleCategoryVm articleVm, IFormFile imageFile)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("=== == == == == == == ModelState not valid == == == == == == ===");
            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                if (value != null)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($">>Error in {modelStateKey}: {error.ErrorMessage}");
                        if (error.Exception != null)
                        {
                            Console.WriteLine($">>Exception: {error.Exception.Message}");
                        }
                    }
                }
            }

            Console.WriteLine("=== == == == == == == ModelState not valid == == == == == == ===");
            return RedirectToAction("Create");
        }
        
        var uploadDir = Path.Combine(_environment.WebRootPath, "uploads/articles");
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var filePath = Path.Combine(uploadDir, fileName);
        
        if (!Directory.Exists(uploadDir))
            Directory.CreateDirectory(uploadDir);
        
        using (var fileStream = new FileStream(filePath, FileMode.Create))
            imageFile.CopyToAsync(fileStream);
        
        articleVm.ImagePath = "/uploads/articles/" + fileName;
        
        var category = _categoryContext.GetCategoryById(articleVm.CategoryId.Value);

        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine($"created the category: {category.Name}");
        Console.WriteLine($"Id of the article: {articleVm.Id}");
        Console.WriteLine($"Size of image is {imageFile.Length}");
        Console.WriteLine($"Image path is {articleVm.ImagePath}");
        Console.WriteLine("----------------------------------------------------");

        var article = new Article
        {
            Id = articleVm.Id,
            Title = articleVm.Title,
            ImagePath = articleVm.ImagePath,
            Content = articleVm.Content,
            ShortDescription = articleVm.ShortDescription,
            PublishDate = articleVm.PublishDate,
            Index1 = articleVm.Index1,
            Index2 = articleVm.Index2,
            Index3 = articleVm.Index3,
            AType = articleVm.AType,
            Category = category,
        };  
        Console.WriteLine(article.Category);
        Console.WriteLine(article.Category.Name);

        _context.Create(article);
        _context.Save();
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int id)
    {
        var article = _context.GetById(id);
        if (_context.ArticleExists(id)) return View("Error");
        
        var articleVm = new ArticleCategoryVm
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            PublishDate = article.PublishDate,
            Views = article.Views,
            ImagePath = article.ImagePath,
            Index1 = article.Index1,
            Index2 = article.Index2,
            Index3 = article.Index3,
            ShortDescription = article.ShortDescription ,
            CategoryId = article.CategoryId,
            Categories = _categoryContext.GetCategories(),
            Likes = article.Likes,
        };
        
        return View("Edit", articleVm);
    }
    
    [HttpPost]
    public IActionResult Edit(int id, ArticleCategoryVm articleVm)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("=== ModelState not valid ===");
            return RedirectToAction("Index"); // Return the view with the current model
        }

        // Fetch the existing category
        
        if (!_context.ArticleExists(id)) return View("Error");

        // Check if a new file is uploaded
        if (articleVm.ImageFile is { Length: > 0 }) // image not null and length > 0
        {
            // Create the upload directory if it doesn't exist
            var uploadDir = Path.Combine(_environment.WebRootPath, "uploads/articles/");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Delete the old image if it exists
            if (!string.IsNullOrEmpty(articleVm.ImagePath))
            {
                var oldImagePath = Path.Combine(_environment.WebRootPath, articleVm.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            // Create a new file name and path for the new image
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(articleVm.ImageFile.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            // Save the new image
            using (var fileStream = new FileStream(filePath, FileMode.Create))
                articleVm.ImageFile.CopyToAsync(fileStream);

            // Update the ImagePath in the view model
            articleVm.ImagePath = "/uploads/articles/" + fileName;
        }
        else
        {
            // If no new image is uploaded, keep the old image path
            articleVm.ImagePath = _context.GetArticleNoTracking(id).ImagePath;
        }

        var category = _categoryContext.GetCategoryById(articleVm.CategoryId.Value);

        // Create the updated category object
        var updatedArticle = new Article
        {
            Id = articleVm.Id,
            Title = articleVm.Title,
            Content = articleVm.Content,
            ImagePath = articleVm.ImagePath,
            Category = category,
            Index1 = articleVm.Index1,
            Index2 = articleVm.Index2,
            Index3 = articleVm.Index3,
            ShortDescription = articleVm.ShortDescription,
            PublishDate = articleVm.PublishDate,
            Views = articleVm.Views,
            AType = articleVm.AType,
            Likes = articleVm.Likes, 
        };

        // Update the category in the context
        _context.Update(updatedArticle);
        Console.WriteLine("----------------------------");
        Console.WriteLine(updatedArticle);
        Console.WriteLine("----------------------------");
        _context.Save();

        // Redirect to the Index action
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        Console.WriteLine("================================");
        Console.WriteLine("Deleting article");
        var article = _context.GetArticlesWithCategory().Single(a => a.Id == id);

        if (article == null) return Json(new{ErrorText = "Article Not Found"});
    return View("Delete", article);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var article = _context.GetById(id);
        if (article == null)return View("Error");

        _context.Delete(article);
        _context.Save();
        return RedirectToAction("Index");
    }
}