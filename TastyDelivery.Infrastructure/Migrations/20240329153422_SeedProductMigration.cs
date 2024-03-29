using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TastyDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Crispy Chicken Fillet, Tomato, Iceberg Lettuce, Mayonnaise, Burger Bun, Fries", "Chicken Burger" },
                    { 2, 0, "Beef Patty, Pickles, Caramelized Onion, Sauce, Burger Bun, Fries", "Beef Burger" },
                    { 3, 0, "300g", "Pork Schnitzel with Fries" },
                    { 4, 1, "Fresh Tomatoes, Cucumber, Bell Peppers, Feta Cheese, Olives", "Shopska Salad" },
                    { 5, 3, "300ml", "Chicken Soup" },
                    { 6, 2, "120g", "Cheesecake" },
                    { 7, 3, "300ml", "Shkembe Chorba" },
                    { 8, 0, "Pasta, Pancetta, Parmesan", "Pasta Carbonara" },
                    { 9, 2, "100g", "Chocolate Cake" },
                    { 10, 1, "Romaine Lettuce, Parmesan Cheese, Croutons, Dressing", "Caesar Salad" },
                    { 11, 0, "200g", "Grilled Trout" },
                    { 12, 0, "100g", "Meatball" },
                    { 13, 0, "150g", "Chicken bites with Cornflakes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");
        }
    }
}
