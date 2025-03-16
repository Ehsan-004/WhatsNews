using System.Collections;
using WhatsNews.Models;
using WhatsNews.Models.Enum;

namespace WhatsNews.ViewModels;

public class ArticleCommentViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ImagePath { get; set; }
    public string Content { get; set; }
    public string ShortDescription { get; set; }
    public DateTime PublishDate { get; set; }
    public int Views { get; set; } = 0;
    public int Likes { get; set; } = 0;
    public bool Index1 { get; set; } = false;
    public bool Index2 { get; set; } = false;
    public bool Index3 { get; set; } = false;
    public AType AType { get; set; } = AType.Normal;
    public Category Category { get; set; }
    
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    public string? CommentAuthor { get; set; }
    public string? CommentText { get; set; }
    public string? CommentEmail { get; set; }
    public DateTime CommentDate { get; set; } = DateTime.Now;
}