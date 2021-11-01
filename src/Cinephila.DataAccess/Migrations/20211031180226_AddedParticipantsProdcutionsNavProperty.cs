using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinephila.DataAccess.Migrations
{
    public partial class AddedParticipantsProdcutionsNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParticipantsProductions_ProductionID",
                table: "ParticipantsProductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantsProductions",
                table: "ParticipantsProductions",
                columns: new[] { "ProductionID", "ParticipantID", "RoleID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantsProductions",
                table: "ParticipantsProductions");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsProductions_ProductionID",
                table: "ParticipantsProductions",
                column: "ProductionID");
        }
    }
}
