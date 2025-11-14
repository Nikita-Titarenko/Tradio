using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CityId", "ConcurrencyStamp", "CreditCount", "Email", "EmailConfirmed", "Fullname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationCode" },
                values: new object[] { "2d13340c-d9ab-4439-bd7c-897c12f8ceba", 0, 4, "73fa19ca-4374-4a0a-b180-0104ef50f211", 105, "nikitatitarenko81@gmail.com", true, "Микита", false, null, "NIKITATITARENKO81@GMAIL.COM", "NIKITATITARENKO81@GMAIL.COM", null, null, false, "22758ef3-c2d6-4cc4-a8f0-dab4790fd254", false, "nikitatitarenko81@gmail.com", "151515" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "2d13340c-d9ab-4439-bd7c-897c12f8ceba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2d13340c-d9ab-4439-bd7c-897c12f8ceba" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d13340c-d9ab-4439-bd7c-897c12f8ceba");
        }
    }
}
