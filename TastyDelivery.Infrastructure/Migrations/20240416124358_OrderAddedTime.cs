using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TastyDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderAddedTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeDelivered",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOrdered",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDelivered",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TimeOrdered",
                table: "Orders");
        }
    }
}
