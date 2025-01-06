using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyAppApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTableNameToSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sources",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sources");
        }
    }
}
