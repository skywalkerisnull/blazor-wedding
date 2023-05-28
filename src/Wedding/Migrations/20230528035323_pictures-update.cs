using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class picturesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileDescription",
                table: "Pictures",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativeText",
                table: "Pictures",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Pictures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PixelsX",
                table: "Pictures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PixelsY",
                table: "Pictures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailFilePath",
                table: "Pictures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ThumbnailSize",
                table: "Pictures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "Pictures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WeddingSetupId",
                table: "Pictures",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WeddingSetupId",
                table: "Pages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_WeddingSetupId",
                table: "Pictures",
                column: "WeddingSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_WeddingSetupId",
                table: "Pages",
                column: "WeddingSetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_WeddingSetups_WeddingSetupId",
                table: "Pages",
                column: "WeddingSetupId",
                principalTable: "WeddingSetups",
                principalColumn: "WeddingSetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_WeddingSetups_WeddingSetupId",
                table: "Pictures",
                column: "WeddingSetupId",
                principalTable: "WeddingSetups",
                principalColumn: "WeddingSetupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_WeddingSetups_WeddingSetupId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_WeddingSetups_WeddingSetupId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_WeddingSetupId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pages_WeddingSetupId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "AlternativeText",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PixelsX",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PixelsY",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ThumbnailFilePath",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ThumbnailSize",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "WeddingSetupId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "WeddingSetupId",
                table: "Pages");

            migrationBuilder.AlterColumn<string>(
                name: "FileDescription",
                table: "Pictures",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
