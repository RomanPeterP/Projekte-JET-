using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableReservationSystem.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name2",
                table: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name2",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
