using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class accomodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WeddingSetupId",
                table: "Party",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WeddingSetups",
                columns: table => new
                {
                    WeddingSetupId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartnerOneId = table.Column<string>(type: "text", nullable: false),
                    PartnerTwoId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingSetups", x => x.WeddingSetupId);
                    table.ForeignKey(
                        name: "FK_WeddingSetups_AspNetUsers_PartnerOneId",
                        column: x => x.PartnerOneId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeddingSetups_AspNetUsers_PartnerTwoId",
                        column: x => x.PartnerTwoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationOptions",
                columns: table => new
                {
                    AccomodationOptionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingSetupId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccomodationName = table.Column<string>(type: "text", nullable: false),
                    AccomodationType = table.Column<string>(type: "text", nullable: false),
                    AccomodationDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AccomodationUrl = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PictureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationOptions", x => x.AccomodationOptionsId);
                    table.ForeignKey(
                        name: "FK_AccomodationOptions_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccomodationOptions_WeddingSetups_WeddingSetupId",
                        column: x => x.WeddingSetupId,
                        principalTable: "WeddingSetups",
                        principalColumn: "WeddingSetupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Party_WeddingSetupId",
                table: "Party",
                column: "WeddingSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationOptions_PictureId",
                table: "AccomodationOptions",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationOptions_WeddingSetupId",
                table: "AccomodationOptions",
                column: "WeddingSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingSetups_PartnerOneId",
                table: "WeddingSetups",
                column: "PartnerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingSetups_PartnerTwoId",
                table: "WeddingSetups",
                column: "PartnerTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_WeddingSetups_WeddingSetupId",
                table: "Party",
                column: "WeddingSetupId",
                principalTable: "WeddingSetups",
                principalColumn: "WeddingSetupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_WeddingSetups_WeddingSetupId",
                table: "Party");

            migrationBuilder.DropTable(
                name: "AccomodationOptions");

            migrationBuilder.DropTable(
                name: "WeddingSetups");

            migrationBuilder.DropIndex(
                name: "IX_Party_WeddingSetupId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "WeddingSetupId",
                table: "Party");
        }
    }
}
