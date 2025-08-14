using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TableReservationSystem.Logic;
using TableReservationSystem.Models;

namespace TableReservationSystem.Data;

public partial class TableReservationSystemContext : DbContext
{
    public TableReservationSystemContext()
    {
    }

    public TableReservationSystemContext(DbContextOptions<TableReservationSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactInfo> ContactInfo { get; set; }

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<Reservation> Reservation { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatus { get; set; }

    public virtual DbSet<ReservationTime> ReservationTime { get; set; }

    public virtual DbSet<Restaurant> Restaurant { get; set; }

    public virtual DbSet<Table> Table { get; set; }

    public virtual DbSet<UpcomingReservation> UpcomingReservation { get; set; }

    public virtual DbSet<Doc> Docs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlServerInstanceName = Environment.GetEnvironmentVariable("SqlServerInstanceName", EnvironmentVariableTarget.User);
        optionsBuilder.UseSqlServer(Config.ConfigItems.GetConnectionString("default")
            .Replace("@SqlServerInstanceName", sqlServerInstanceName));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.ContactInfoId);
            entity.Property(e => e.ContactInfoId).UseIdentityColumn(1, 1);
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

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId);
            entity.Property(e => e.ReservationId).UseIdentityColumn(1, 1);
            entity.Property(e => e.ReservationId).ValueGeneratedOnAdd();
            entity.Property(e => e.AdditionalMessage).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.ReservationStatusId).HasDefaultValue((byte)1);

            entity.HasOne(e => e.ContactInfo).WithMany(p => p.Reservations)
                .HasForeignKey(e => e.ContactInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(e => e.ReservationStatus).WithMany(p => p.Reservations)
                .HasForeignKey(e => e.ReservationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(e => e.ReservationTimes).WithMany(p => p.Reservations)
                .HasForeignKey(e => e.ReservationTimeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(e => e.Restaurant).WithMany(p => p.Reservations)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(e => e.Table).WithMany(p => p.Reservations)
                .HasForeignKey(e => e.TableNumber)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.HasKey(e => e.ReservationStatusId);
            entity.Property(e => e.ReservationStatusId).UseIdentityColumn(1, 1);
            entity.Property(e => e.ReservationStatusId).ValueGeneratedOnAdd();
            entity.Property(e => e.Caption).HasMaxLength(15);
        });

        modelBuilder.Entity<ReservationTime>(entity =>
        {
            entity.HasKey(e => e.ReservationTimeId);
            entity.Property(e => e.ReservationTimeId).UseIdentityColumn(1, 1);
            entity.HasKey(e => e.ReservationTimeId);
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId);
            entity.Property(e => e.RestaurantId).UseIdentityColumn(1, 1);
            entity.Property(e => e.RestaurantId).ValueGeneratedOnAdd();
            entity.Property(e => e.City).HasMaxLength(20);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PostalCode).HasMaxLength(15);
            entity.Property(e => e.StreetHouseNr).HasMaxLength(25);
            entity.Property(e => e.Activ).HasDefaultValue(true);

            entity.HasOne(e => e.ContactInfo).WithMany(p => p.Restaurants)
                .HasForeignKey(e => e.ContactInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(e => e.Country).WithMany(p => p.Restaurants)
                .HasForeignKey(e => e.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableNumber);
            entity.Property(e => e.Activ).HasDefaultValue(true);
            entity.Property(e => e.TableNumber).HasMaxLength(10);

            entity.HasOne(e => e.Restaurant).WithMany(p => p.Tables)
                .HasForeignKey(e => e.RestaurantId);
        });

        modelBuilder
             .Entity<UpcomingReservation>()
             .ToView("UpcomingReservation")
             .HasNoKey();

        modelBuilder.Entity<ReservationsFromDay>()
            .ToFunction("ReservationsFromDay")
            .HasNoKey();

        modelBuilder.Entity<Doc>(entity =>
        {
            entity.HasKey(e => e.DocsId);
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MimeType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Documents)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).UseIdentityColumn(1, 1);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(30);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
