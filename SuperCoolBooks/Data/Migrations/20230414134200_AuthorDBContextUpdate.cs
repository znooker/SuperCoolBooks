using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperCoolBooks.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorDBContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenresGenreID",
                table: "BookGenre");

            migrationBuilder.RenameColumn(
                name: "GenreID",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "GenresGenreID",
                table: "BookGenre",
                newName: "GenresGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenresGenreID",
                table: "BookGenre",
                newName: "IX_BookGenre_GenresGenreId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Authors",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenresGenreId",
                table: "BookGenre",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenresGenreId",
                table: "BookGenre");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "GenreID");

            migrationBuilder.RenameColumn(
                name: "GenresGenreId",
                table: "BookGenre",
                newName: "GenresGenreID");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenresGenreId",
                table: "BookGenre",
                newName: "IX_BookGenre_GenresGenreID");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenresGenreID",
                table: "BookGenre",
                column: "GenresGenreID",
                principalTable: "Genres",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
