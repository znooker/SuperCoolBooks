using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditGenreTitle_Andreas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "Genre_Uniqe_Title_Constraint",
                table: "Genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "Genre_Uniqe_Title_Constraint",
                table: "Genres",
                column: "Title");
        }
    }
}
