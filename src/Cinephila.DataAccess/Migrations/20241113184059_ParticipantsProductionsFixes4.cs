using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantsProductionsFixes4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "TmdbId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
