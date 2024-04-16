using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TastyDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRequiredFromOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryManId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryManId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryManId",
                table: "Orders",
                column: "DeliveryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryManId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryManId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DeliveryManId",
                table: "Orders",
                column: "DeliveryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
