using Microsoft.EntityFrameworkCore;

public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options)
        : base(options) { }

    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).UseIdentityColumn(1, 1);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Message).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(2);
        });
    }
}