using BenutzerverwaltungAP09.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCNorthwindUebungen.Data;

public partial class BenutzerContext : DbContext
{
    public virtual DbSet<Benutzer> Benutzer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BenutzerVerwaltung;Data Source=DESKTOP-KCGE85K\\SQLEXPRESS;TrustServerCertificate=true");

}
