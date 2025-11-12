using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDurationInDaysInsteadOfDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SubscriptionTypies");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "SubscriptionTypies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationInDays",
                value: 30);

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DurationInDays",
                value: 90);

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 3,
                column: "DurationInDays",
                value: 365);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "SubscriptionTypies");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "SubscriptionTypies",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Duration",
                value: new TimeSpan(30, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Duration",
                value: new TimeSpan(90, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "SubscriptionTypies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Duration",
                value: new TimeSpan(365, 0, 0, 0, 0));
        }
    }
}
