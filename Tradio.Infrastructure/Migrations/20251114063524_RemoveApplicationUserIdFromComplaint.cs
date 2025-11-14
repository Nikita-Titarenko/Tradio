using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveApplicationUserIdFromComplaint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ApplicationUserId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Complaints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Complaints",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ApplicationUserId",
                table: "Complaints",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_AspNetUsers_ApplicationUserId",
                table: "Complaints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
