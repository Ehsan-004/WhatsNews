using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;
using WhatsNews.ViewModels;

namespace WhatsNews.Controllers;

public class CommentsController : Controller
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;

    public CommentsController(IArticleRepository articleRepository, ICommentRepository commentRepository)
    {
        _articleRepository = articleRepository;
        _commentRepository = commentRepository;
    }

    [HttpPost]
    public IActionResult NewComment(ArticleCommentViewModel articleComment, int postId)
    {
        var article = _articleRepository.GetById(postId);
        var newComment = new Comment()
        {
            Author = articleComment.CommentAuthor,
            Email = articleComment.CommentEmail,
            Text = articleComment.CommentText,
            Date = articleComment.CommentDate,
            Article = article
        };
        
        _commentRepository.Create(newComment);
        // return RedirectToAction("show", "Articles", new { id = postId });
        return Json(new { author = newComment.Author, text = newComment.Text });
    }
}