using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class nullableparty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Party_PartyId",
                table: "Guests");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartyId",
                table: "Guests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                name: "FK_Guests_Party_PartyId",
                table: "Guests");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartyId",
                table: "Guests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Party_PartyId",
                table: "Guests",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
