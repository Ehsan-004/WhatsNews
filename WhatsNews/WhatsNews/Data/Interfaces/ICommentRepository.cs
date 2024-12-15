using WhatsNews.Models;

namespace WhatsNews.Data.Interfaces;

public interface ICommentRepository
{
    public IEnumerable<Comment> GetComments();  
    public Comment GetById(int id);
    public Comment GetByPostId(int postId);
    public bool Create(Comment comment);
    public bool Update(Comment comment);
    public bool Delete(Comment comment);
    public bool Save();
}