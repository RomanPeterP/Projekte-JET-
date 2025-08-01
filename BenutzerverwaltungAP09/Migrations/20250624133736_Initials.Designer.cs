﻿// <auto-generated />
using System;
using BenutzerverwaltungAP09.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BenutzerverwaltungAP09.Migrations
{
    [DbContext(typeof(BenutzerContext))]
    [Migration("20250624133736_Initials")]
    partial class Initials
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BenutzerverwaltungAP09.Models.Benutzer", b =>
                {
                    b.Property<int>("BenutzerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenutzerId"));

                    b.Property<byte>("Alter")
                        .HasColumnType("tinyint");

                    b.Property<int?>("LoginDataBenutzerId")
                        .HasColumnType("int");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("BenutzerId");

                    b.HasIndex("LoginDataBenutzerId");

                    b.ToTable("Benutzer");
                });

            modelBuilder.Entity("BenutzerverwaltungAP09.Models.LoginData", b =>
                {
                    b.Property<int>("BenutzerId")
                        .HasColumnType("int");

                    b.Property<string>("Benutzername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Passwort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BenutzerId");

                    b.ToTable("LoginData");
                });

            modelBuilder.Entity("BenutzerverwaltungAP09.Models.Benutzer", b =>
                {
                    b.HasOne("BenutzerverwaltungAP09.Models.LoginData", "LoginData")
                        .WithMany()
                        .HasForeignKey("LoginDataBenutzerId");

                    b.Navigation("LoginData");
                });

            modelBuilder.Entity("BenutzerverwaltungAP09.Models.LoginData", b =>
                {
                    b.HasOne("BenutzerverwaltungAP09.Models.Benutzer", "Benutzer")
                        .WithOne()
                        .HasForeignKey("BenutzerverwaltungAP09.Models.LoginData", "BenutzerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benutzer");
                });
#pragma warning restore 612, 618
        }
    }
}
