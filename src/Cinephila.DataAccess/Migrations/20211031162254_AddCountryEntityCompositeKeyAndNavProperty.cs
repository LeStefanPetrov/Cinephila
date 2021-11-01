using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinephila.DataAccess.Migrations
{
    public partial class AddCountryEntityCompositeKeyAndNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountriesProductions_CountriesProductions_CountryProductionCountryID_CountryProductionProductionID",
                table: "CountriesProductions");

            migrationBuilder.DropIndex(
                name: "IX_CountriesProductions_CountryProductionCountryID_CountryProductionProductionID",
                table: "CountriesProductions");

            migrationBuilder.DropColumn(
                name: "CountryProductionCountryID",
                table: "CountriesProductions");

            migrationBuilder.DropColumn(
                name: "CountryProductionProductionID",
                table: "CountriesProductions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryProductionCountryID",
                table: "CountriesProductions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryProductionProductionID",
                table: "CountriesProductions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountriesProductions_CountryProductionCountryID_CountryProductionProductionID",
                table: "CountriesProductions",
                columns: new[] { "CountryProductionCountryID", "CountryProductionProductionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CountriesProductions_CountriesProductions_CountryProductionCountryID_CountryProductionProductionID",
                table: "CountriesProductions",
                columns: new[] { "CountryProductionCountryID", "CountryProductionProductionID" },
                principalTable: "CountriesProductions",
                principalColumns: new[] { "CountryID", "ProductionID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
