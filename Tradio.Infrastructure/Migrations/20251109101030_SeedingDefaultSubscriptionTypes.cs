using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultSubscriptionTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubscriptionTypies",
                columns: new[] { "Id", "Duration", "Name", "PriceInCents" },
                values: new object[,]
                {
                    { 1, new TimeSpan(30, 0, 0, 0, 0), "Standart", 1000 },
                    { 2, new TimeSpan(90, 0, 0, 0, 0), "Premium", 2500 },
                    { 3, new TimeSpan(365, 0, 0, 0, 0), "Ultimate", 9000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
