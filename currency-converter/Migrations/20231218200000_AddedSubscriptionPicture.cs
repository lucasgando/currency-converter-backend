using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currency_converter.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubscriptionPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionPicture",
                table: "Subscriptions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubscriptionPicture",
                value: "http://2.bp.blogspot.com/-Kjm_y-4VY6Q/T-CZuH4eDMI/AAAAAAAABUg/H5mIxef6Tjc/s1600/Free1.jpg");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubscriptionPicture",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionPicture",
                table: "Subscriptions");
        }
    }
}
