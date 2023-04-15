using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class guestupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_DietaryRequirements_DietaryRequirementsId",
                table: "Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Party_PartyId",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_PartyId",
                table: "Guests",
                newName: "IX_Guests_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_DietaryRequirementsId",
                table: "Guests",
                newName: "IX_Guests_DietaryRequirementsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InviteAccepted",
                table: "Guests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvitationOpened",
                table: "Guests",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_DietaryRequirements_DietaryRequirementsId",
                table: "Guests",
                column: "DietaryRequirementsId",
                principalTable: "DietaryRequirements",
                principalColumn: "DietaryRequirementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Party_PartyId",
                table: "Guests",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_DietaryRequirements_DietaryRequirementsId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Party_PartyId",
                table: "Guests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_PartyId",
                table: "Guest",
                newName: "IX_Guest_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_DietaryRequirementsId",
                table: "Guest",
                newName: "IX_Guest_DietaryRequirementsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InviteAccepted",
                table: "Guest",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvitationOpened",
                table: "Guest",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_DietaryRequirements_DietaryRequirementsId",
                table: "Guest",
                column: "DietaryRequirementsId",
                principalTable: "DietaryRequirements",
                principalColumn: "DietaryRequirementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Party_PartyId",
                table: "Guest",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId");
        }
    }
}
