using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foreca.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class imageField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CityDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CityDetails_Name",
                table: "CityDetails",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CityDetails_Name",
                table: "CityDetails");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CityDetails");
        }
    }
}
