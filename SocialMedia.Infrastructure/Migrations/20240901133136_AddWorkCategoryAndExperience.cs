using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkCategoryAndExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkExperience",
                table: "Post",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkIndustry",
                table: "Post",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkExperience",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "WorkIndustry",
                table: "Post");
        }
    }
}
