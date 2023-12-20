using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class RemovedMainCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Currencies_MainCurrencyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MainCurrencyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MainCurrencyId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainCurrencyId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "MainCurrencyId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "MainCurrencyId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MainCurrencyId",
                table: "Users",
                column: "MainCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Currencies_MainCurrencyId",
                table: "Users",
                column: "MainCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
