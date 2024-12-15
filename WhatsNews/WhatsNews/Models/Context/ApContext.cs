using Microsoft.EntityFrameworkCore;

namespace WhatsNews.Models.Context;

public class ApContext : DbContext
{
    public ApContext(DbContextOptions<ApContext> options) : base(options) { }
    
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
}