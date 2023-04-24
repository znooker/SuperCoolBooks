using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikeCounterReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Reviews",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Reviews");
        }
    }
}
