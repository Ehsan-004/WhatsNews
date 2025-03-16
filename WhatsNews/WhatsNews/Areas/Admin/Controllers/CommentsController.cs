using Microsoft.AspNetCore.Mvc;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;

namespace WhatsNews.Areas.Admin.Controllers;

[Area("Admin")]
public class CommentsController : Controller
{
    private readonly ICommentRepository _context;

    public CommentsController(ICommentRepository commentRepository)
    {
        _context = commentRepository;
    }

    public IActionResult Index()
    {
        var comments = _context.GetComments();
        return View(comments);
    }

    public IActionResult Edit(int id)
    {
        var comment = _context.GetById(id);
        return View(comment);
    }

    [HttpPost]
    public IActionResult Edit(Comment comment)
    {
        if (!ModelState.IsValid) return View(comment);

        _context.Update(comment);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {   
        var comment = _context.GetById(id);
        return View(comment);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var comment = _context.GetById(id);
        if (comment == null)
            return RedirectToAction("index");

        _context.Delete(comment);
        return RedirectToAction("index");
    }
}