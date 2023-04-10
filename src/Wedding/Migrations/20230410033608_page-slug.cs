using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    /// <inheritdoc />
    public partial class pageslug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Pages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Slug",
                table: "Pages",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pages_Slug",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Pages");
        }
    }
}
