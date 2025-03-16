using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WhatsNews.Data.Interfaces;
using WhatsNews.ViewModels;

namespace WhatsNews.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;

    public ArticlesController(IArticleRepository articleRepository, ICommentRepository commentRepository)
    {
        _articleRepository = articleRepository;
        _commentRepository = commentRepository;
    }

    [HttpGet]
    [Route("/article/{id}")]
    public IActionResult Show(int id)
    {
        var article = _articleRepository.GetById(id);
        if (article == null)
            return NotFound();

        var comments = _commentRepository.GetByPostId(id);
        
        var articleComment = new ArticleCommentViewModel()
        {
            Id = article.Id,
            Category = article.Category,
            Content = article.Content,
            Index1 = article.Index1,
            Index2 = article.Index2,
            Index3 = article.Index3,
            Likes = article.Likes,
            Title = article.Title,
            AType = article.AType,
            ImagePath = article.ImagePath,
            PublishDate = article.PublishDate,
            ShortDescription = article.ShortDescription,
            Views = article.Views,
            Comments = comments
        };

        
        return View(articleComment);
    }

}