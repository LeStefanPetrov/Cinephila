using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantsProductionsFixes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KnowForDepartment",
                table: "Participants",
                newName: "KnownForDepartment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KnownForDepartment",
                table: "Participants",
                newName: "KnowForDepartment");
        }
    }
}
