using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackApp.Migrations
{
    /// <inheritdoc />
    public partial class MimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_Feedbacks_FeedbackId",
                table: "Docs");

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "Docs",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_Feedbacks_FeedbackId",
                table: "Docs",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Docs_Feedbacks_FeedbackId",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "Docs");

            migrationBuilder.AddForeignKey(
                name: "FK_Docs_Feedbacks_FeedbackId",
                table: "Docs",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "FeedbackId");
        }
    }
}
