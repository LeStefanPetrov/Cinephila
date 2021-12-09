using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinephila.DataAccess.Migrations
{
    public partial class RemovingPKsFromMovieAndTVShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows");

            migrationBuilder.DropIndex(
                name: "IX_TVShows_ProductionID",
                table: "TVShows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ProductionID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "TVShows");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Movies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows",
                column: "ProductionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "ProductionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "TVShows",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TVShows",
                table: "TVShows",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_TVShows_ProductionID",
                table: "TVShows",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ProductionID",
                table: "Movies",
                column: "ProductionID");
        }
    }
}
