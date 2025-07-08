using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenutzerverwaltungAP09.Migrations
{
    /// <inheritdoc />
    public partial class Korr23dd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginData_Benutzer_BenutzerId",
                table: "LoginData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoginData",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginData_Benutzer_BenutzerId",
                table: "LoginData",
                column: "BenutzerId",
                principalTable: "Benutzer",
                principalColumn: "BenutzerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginData_Benutzer_BenutzerId",
                table: "LoginData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LoginData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginData_Benutzer_BenutzerId",
                table: "LoginData",
                column: "BenutzerId",
                principalTable: "Benutzer",
                principalColumn: "BenutzerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
