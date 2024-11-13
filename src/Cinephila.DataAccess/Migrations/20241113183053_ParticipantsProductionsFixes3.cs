using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephila.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ParticipantsProductionsFixes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantImageEntity_Images_ImageID",
                table: "ParticipantImageEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantImageEntity_Participants_ParticipantID",
                table: "ParticipantImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantImageEntity",
                table: "ParticipantImageEntity");

            migrationBuilder.RenameTable(
                name: "ParticipantImageEntity",
                newName: "ParticipantsImages");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantImageEntity_ImageID",
                table: "ParticipantsImages",
                newName: "IX_ParticipantsImages_ImageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantsImages",
                table: "ParticipantsImages",
                columns: new[] { "ParticipantID", "ImageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsImages_Images_ImageID",
                table: "ParticipantsImages",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsImages_Images_ImageID",
                table: "ParticipantsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantsImages_Participants_ParticipantID",
                table: "ParticipantsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantsImages",
                table: "ParticipantsImages");

            migrationBuilder.RenameTable(
                name: "ParticipantsImages",
                newName: "ParticipantImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantsImages_ImageID",
                table: "ParticipantImageEntity",
                newName: "IX_ParticipantImageEntity_ImageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantImageEntity",
                table: "ParticipantImageEntity",
                columns: new[] { "ParticipantID", "ImageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantImageEntity_Images_ImageID",
                table: "ParticipantImageEntity",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantImageEntity_Participants_ParticipantID",
                table: "ParticipantImageEntity",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
