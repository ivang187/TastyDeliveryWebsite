using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TastyDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllProductsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProductsRestaurants",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "ProductsRestaurants",
                columns: new[] { "ProductId", "RestaurantId", "Price" },
                values: new object[,]
                {
                    { 1, 1, 10.5 },
                    { 2, 1, 11.9 },
                    { 3, 1, 15.5 },
                    { 5, 1, 4.5 },
                    { 8, 1, 9.5 },
                    { 10, 1, 11.9 },
                    { 11, 1, 14.5 },
                    { 3, 2, 15.6 },
                    { 4, 2, 9.0999999999999996 },
                    { 5, 2, 4.7000000000000002 },
                    { 7, 2, 6.2000000000000002 },
                    { 9, 2, 7.9000000000000004 },
                    { 10, 2, 12.4 },
                    { 11, 2, 14.9 },
                    { 12, 2, 3.2999999999999998 },
                    { 13, 2, 10.800000000000001 },
                    { 1, 3, 9.6999999999999993 },
                    { 2, 3, 10.5 },
                    { 3, 3, 15.5 },
                    { 4, 3, 8.0 },
                    { 5, 3, 4.5 },
                    { 6, 3, 4.9000000000000004 },
                    { 7, 3, 5.0 },
                    { 8, 3, 9.5 },
                    { 10, 3, 10.800000000000001 },
                    { 11, 3, 11.800000000000001 },
                    { 12, 3, 2.5 },
                    { 13, 3, 8.8000000000000007 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 13, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsRestaurants",
                keyColumns: new[] { "ProductId", "RestaurantId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductsRestaurants",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
