using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foreca.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDisplayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayedName",
                table: "CityDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayedName",
                table: "CityDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
