using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class guestremovediet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_DietaryRequirements_DietaryRequirementsId",
                table: "Guests");

            migrationBuilder.DropTable(
                name: "DietaryRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Guests_DietaryRequirementsId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "DietaryRequirementsId",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "Guests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "CommonRequirements",
                table: "Guests",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Guests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CommonRequirements",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Guests");

            migrationBuilder.AddColumn<Guid>(
                name: "DietaryRequirementsId",
                table: "Guests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DietaryRequirements",
                columns: table => new
                {
                    DietaryRequirementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Allergies = table.Column<string>(type: "text", nullable: true),
                    CommonRequirements = table.Column<int[]>(type: "integer[]", nullable: false),
                    Other = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRequirements", x => x.DietaryRequirementsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_DietaryRequirementsId",
                table: "Guests",
                column: "DietaryRequirementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_DietaryRequirements_DietaryRequirementsId",
                table: "Guests",
                column: "DietaryRequirementsId",
                principalTable: "DietaryRequirements",
                principalColumn: "DietaryRequirementsId");
        }
    }
}
