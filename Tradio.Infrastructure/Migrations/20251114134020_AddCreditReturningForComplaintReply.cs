using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditReturningForComplaintReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditReturning",
                table: "ComplaintReplies",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditReturning",
                table: "ComplaintReplies");
        }
    }
}
