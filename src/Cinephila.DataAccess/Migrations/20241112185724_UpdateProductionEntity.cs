using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearOfCreation",
                table: "Productions",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "ApiID",
                table: "Productions",
                newName: "VoteCount");

            migrationBuilder.RenameColumn(
                name: "LengthInMinutes",
                table: "Movies",
                newName: "Runtime");

            migrationBuilder.AddColumn<int>(
                name: "Budget",
                table: "Productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Popularity",
                table: "Productions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Revenue",
                table: "Productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Productions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmdbID",
                table: "Productions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "VoteAverage",
                table: "Productions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "TmdbID",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "VoteAverage",
                table: "Productions");

            migrationBuilder.RenameColumn(
                name: "VoteCount",
                table: "Productions",
                newName: "ApiID");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Productions",
                newName: "YearOfCreation");

            migrationBuilder.RenameColumn(
                name: "Runtime",
                table: "Movies",
                newName: "LengthInMinutes");
        }
    }
}
