using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFavToMainAndAddedFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Currencies_FavoriteCurrencyId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FavoriteCurrencyId",
                table: "Users",
                newName: "MainCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FavoriteCurrencyId",
                table: "Users",
                newName: "IX_Users_MainCurrencyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Currencies_MainCurrencyId",
                table: "Users",
                column: "MainCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Users_UserId",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Currencies_MainCurrencyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_UserId",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "MainCurrencyId",
                table: "Users",
                newName: "FavoriteCurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_MainCurrencyId",
                table: "Users",
                newName: "IX_Users_FavoriteCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Currencies_FavoriteCurrencyId",
                table: "Users",
                column: "FavoriteCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
