using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class AddedFlagAttributeToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flag",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                table: "Currencies");
        }
    }
}
