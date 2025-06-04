using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackApp.Migrations
{
    /// <inheritdoc />
    public partial class NeueFelder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Feedbacks",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Ratinggrade",
                table: "Feedbacks",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Ratinggrade",
                table: "Feedbacks");
        }
    }
}
