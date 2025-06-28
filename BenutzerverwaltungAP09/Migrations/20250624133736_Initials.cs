using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenutzerverwaltungAP09.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginDataBenutzerId",
                table: "Benutzer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoginData",
                columns: table => new
                {
                    BenutzerId = table.Column<int>(type: "int", nullable: false),
                    Benutzername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passwort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginData", x => x.BenutzerId);
                    table.ForeignKey(
                        name: "FK_LoginData_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "BenutzerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_LoginDataBenutzerId",
                table: "Benutzer",
                column: "LoginDataBenutzerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benutzer_LoginData_LoginDataBenutzerId",
                table: "Benutzer",
                column: "LoginDataBenutzerId",
                principalTable: "LoginData",
                principalColumn: "BenutzerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_LoginData_LoginDataBenutzerId",
                table: "Benutzer");

            migrationBuilder.DropTable(
                name: "LoginData");

            migrationBuilder.DropIndex(
                name: "IX_Benutzer_LoginDataBenutzerId",
                table: "Benutzer");

            migrationBuilder.DropColumn(
                name: "LoginDataBenutzerId",
                table: "Benutzer");
        }
    }
}
