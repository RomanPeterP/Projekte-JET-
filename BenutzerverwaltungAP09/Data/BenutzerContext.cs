using BenutzerverwaltungAP09.Models;
using Microsoft.EntityFrameworkCore;

namespace BenutzerverwaltungAP09.Data;

public partial class BenutzerContext : DbContext
{
    public virtual DbSet<Benutzer> Benutzer { get; set; }
    public virtual DbSet<LoginData> LoginData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginData>()
              .HasKey(ld => ld.BenutzerId); // Primärschlüssel

        // Hier muss man die 1:1-Beziehung abbilden
        modelBuilder.Entity<LoginData>()
            .HasOne(ld => ld.Benutzer)   // Navigation zu Benutzer
            .WithOne(b => b.LoginData)  // Rücknavigation
            .HasForeignKey<LoginData>(ld => ld.BenutzerId)   // Fremdschlüssel ist gleichzeitig Primärschlüssel
            .OnDelete(DeleteBehavior.Restrict);  // Löschweitergabe deaktiviert
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BenutzerVerwaltung;Data Source=DESKTOP-KCGE85K\\SQLEXPRESS;TrustServerCertificate=true");

}
