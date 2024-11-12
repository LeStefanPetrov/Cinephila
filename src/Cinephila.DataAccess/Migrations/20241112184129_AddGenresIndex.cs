using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGenresIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Genres_TmdbId",
                table: "Genres",
                column: "TmdbId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genres_TmdbId",
                table: "Genres");
        }
    }
}
