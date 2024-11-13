using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GenreProductionFixRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresProductions_Genres_GenreID",
                table: "GenresProductions");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Genres_TmdbId",
                table: "Genres",
                column: "TmdbId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresProductions_Genres_GenreID",
                table: "GenresProductions",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "TmdbId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresProductions_Genres_GenreID",
                table: "GenresProductions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Genres_TmdbId",
                table: "Genres");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresProductions_Genres_GenreID",
                table: "GenresProductions",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
