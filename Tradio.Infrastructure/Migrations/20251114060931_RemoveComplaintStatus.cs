using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComplaintStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintStatuses_ComplaintStatusId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ComplaintStatusId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintReplies_ComplaintId",
                table: "ComplaintReplies");

            migrationBuilder.DropColumn(
                name: "ComplaintStatusId",
                table: "Complaints");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ComplaintReplies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReplies_ComplaintId",
                table: "ComplaintReplies",
                column: "ComplaintId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ComplaintReplies_ComplaintId",
                table: "ComplaintReplies");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ComplaintReplies");

            migrationBuilder.AddColumn<int>(
                name: "ComplaintStatusId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComplaintStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintStatusId",
                table: "Complaints",
                column: "ComplaintStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintReplies_ComplaintId",
                table: "ComplaintReplies",
                column: "ComplaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintStatuses_ComplaintStatusId",
                table: "Complaints",
                column: "ComplaintStatusId",
                principalTable: "ComplaintStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
