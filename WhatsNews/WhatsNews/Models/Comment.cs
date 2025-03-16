namespace WhatsNews.Models;

public class Comment
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    
    public Article Article { get; set; }
}