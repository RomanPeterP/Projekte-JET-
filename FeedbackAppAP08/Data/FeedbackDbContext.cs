using FeedbackAppAP08.Models;
using Microsoft.EntityFrameworkCore;

public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options): base(options) { }

    public DbSet<Feedback> Feedbacks { get; set; }
    public virtual DbSet<Doc> Docs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId);
            entity.Property(e => e.FeedbackId).UseIdentityColumn(1, 1);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Message).HasMaxLength(200);
            entity.Property(e => e.Country).HasMaxLength(2);
        });

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(e => e.DocsId);
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MimeType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Feedback).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}