using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class AddedBondBetweenFavCurrenciesAndCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Currencies");

            migrationBuilder.CreateTable(
                name: "CurrencyUser",
                columns: table => new
                {
                    FavoriteCurrenciesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUser", x => new { x.FavoriteCurrenciesId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CurrencyUser_Currencies_FavoriteCurrenciesId",
                        column: x => x.FavoriteCurrenciesId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyUser_UserId",
                table: "CurrencyUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Currencies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UserId",
                table: "Currencies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
