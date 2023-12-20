using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrialSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Free");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "UsdPrice" },
                values: new object[] { "Premium", 9.99f });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ConverterLimit", "Name", "SubscriptionPicture", "UsdPrice" },
                values: new object[] { 3, 100, "Trial", "", 2.49f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "free");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "UsdPrice" },
                values: new object[] { "premium", 10f });
        }
    }
}
