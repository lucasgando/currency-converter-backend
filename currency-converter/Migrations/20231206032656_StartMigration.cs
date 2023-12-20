using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class StartMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    ConversionIndex = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ConverterLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    UsdPrice = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    SubscriptionId = table.Column<int>(type: "INTEGER", nullable: false),
                    FavoriteCurrencyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ConverterUses = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Currencies_FavoriteCurrencyId",
                        column: x => x.FavoriteCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FromCurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromCurrencyIndex = table.Column<float>(type: "REAL", nullable: false),
                    Amount = table.Column<float>(type: "REAL", nullable: false),
                    ToCurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToCurrencyIndex = table.Column<float>(type: "REAL", nullable: false),
                    Result = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_FromCurrencyId",
                        column: x => x.FromCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversions_Currencies_ToCurrencyId",
                        column: x => x.ToCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ConverterLimit", "Name", "UsdPrice" },
                values: new object[,]
                {
                    { 1, 10, "free", 0f },
                    { 2, -1, "premium", 10f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConverterUses", "Email", "FavoriteCurrencyId", "PasswordHash", "Role", "SubscriptionId", "Username" },
                values: new object[,]
                {
                    { 1, 0, "admin@mail.com", null, "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", 1, 2, "Admin" },
                    { 2, 0, "user@mail.com", null, "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", 0, 1, "user1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_FromCurrencyId",
                table: "Conversions",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ToCurrencyId",
                table: "Conversions",
                column: "ToCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_UserId",
                table: "Conversions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteCurrencyId",
                table: "Users",
                column: "FavoriteCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
