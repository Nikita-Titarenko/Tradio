using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenAppUserAndCityInsteadOfAppUserAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserServices_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Cities_CityId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CityId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ApplicationUserId",
                table: "Services",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserServices_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserServices",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_ApplicationUserId",
                table: "Services",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserServices_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserServices");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_ApplicationUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ApplicationUserId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CityId",
                table: "Services",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserServices_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserServices",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Cities_CityId",
                table: "Services",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
