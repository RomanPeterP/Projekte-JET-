using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TRSAP09V2.Logic;
using TRSAP09V2.Models;

namespace TRSAP09V2.Data;

public class Trsap09v2Context : DbContext
{
    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public virtual DbSet<ReservationTime> ReservationTimes { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tabellennamen in Einzahl-Schreibung erzwingen
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entity.Name).ToTable(entity.ClrType.Name);
        }

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.Property(e => e.Caption).HasMaxLength(15);
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdateAt);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.StreetHouseNr).HasMaxLength(25);

            // Beziehungen
            entity.HasOne(d => d.ContactInfo).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.ContactInfoId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Country).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.Property(e => e.AdditionalMessage).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.ReservationStatusId).HasDefaultValue(1);

            // Beziehungen
            entity.HasOne(d => d.ContactInfo).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ContactInfoId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.ReservationStatus).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReservationStatusId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.ReservationTime).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReservationTimeId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.Table).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TableNumber).HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.HasIndex(b => new { b.TableNumber, b.RestaurantId })
                .IsUnique().HasDatabaseName("UQ_TableNumber_RestaurantId"); ;

            // Beziehungen
            entity.HasOne(d => d.Restaurant).WithMany(p => p.Tables).HasForeignKey(d => d.RestaurantId);
        });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(Config.ConfigItems.GetConnectionString("default"));

}
