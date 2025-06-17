using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenutzerverwaltungAP09.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benutzer",
                columns: table => new
                {
                    BenutzerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alter = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benutzer", x => x.BenutzerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benutzer");
        }
    }
}
