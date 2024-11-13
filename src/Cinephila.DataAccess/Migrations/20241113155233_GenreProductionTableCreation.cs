using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GenreProductionTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenresProductions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenresProductions", x => new { x.ProductionID, x.GenreID });
                    table.ForeignKey(
                        name: "FK_GenresProductions_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenresProductions_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenresProductions_GenreID",
                table: "GenresProductions",
                column: "GenreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenresProductions");
        }
    }
}
