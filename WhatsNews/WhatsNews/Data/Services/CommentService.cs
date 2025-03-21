﻿using Microsoft.EntityFrameworkCore;
using WhatsNews.Data.Interfaces;
using WhatsNews.Models;
using WhatsNews.Models.Context;

namespace WhatsNews.Data.Services;

public class CommentService : ICommentRepository
{
    private readonly ApContext _context;

    public CommentService(ApContext context)
    {
        _context = context;
    }

    public IEnumerable<Comment> GetComments()
    {
        return _context.Comments.ToList();
    }

    public Comment GetById(int id)
    {
        return _context.Comments.FirstOrDefault(c => c.Id == id);
    }

    public ICollection<Comment> GetByPostId(int postId)
    {
        var article = _context.Articles.FirstOrDefault(a => a.Id == postId);
        return _context.Comments.Where(c => c.Article == article).ToList();
    }

    public bool Create(Comment comment)
    {
        _context.Comments.Add(comment);
        return Save();
    }

    public bool Update(Comment comment)
    {
        _context.Comments.Update(comment);
        return Save();
    }

    public bool Delete(Comment comment)
    {
        _context.Comments.Remove(comment);
        return Save();
    }

    public bool Save()
    {
        return (_context.SaveChanges() >= 0);
    }
}