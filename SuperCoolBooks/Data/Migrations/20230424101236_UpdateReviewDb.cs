using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Reviews");
        }
    }
}
