using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenutzerverwaltungAP09.Migrations
{
    /// <inheritdoc />
    public partial class upd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_LoginData_LoginDataBenutzerId",
                table: "Benutzer");

            migrationBuilder.DropIndex(
                name: "IX_Benutzer_LoginDataBenutzerId",
                table: "Benutzer");

            migrationBuilder.DropColumn(
                name: "LoginDataBenutzerId",
                table: "Benutzer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginDataBenutzerId",
                table: "Benutzer",
                type: "int",
                nullable: true);

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
    }
}
