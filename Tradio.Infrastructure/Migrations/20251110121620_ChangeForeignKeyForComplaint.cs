using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeForeignKeyForComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Services_ServiceId",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Complaints",
                newName: "ApplicationUserServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_ServiceId",
                table: "Complaints",
                newName: "IX_Complaints_ApplicationUserServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Complaints",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ApplicationUserServices_ApplicationUserServiceId",
                table: "Complaints",
                column: "ApplicationUserServiceId",
                principalTable: "ApplicationUserServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ApplicationUserServices_ApplicationUserServiceId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserServiceId",
                table: "Complaints",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_ApplicationUserServiceId",
                table: "Complaints",
                newName: "IX_Complaints_ServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Complaints",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Services_ServiceId",
                table: "Complaints",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
