using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsAndChangeRelationsBetweenTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUserServices_ApplicationUserServiceApplicationUserId_ApplicationUserServiceServiceId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationUserServiceApplicationUserId_ApplicationUserServiceServiceId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserServices",
                table: "ApplicationUserServices");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserServiceApplicationUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserServiceServiceId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Messages",
                newName: "ApplicationUserServiceId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromProvider",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserServiceId",
                table: "ApplicationUserServices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserServices",
                table: "ApplicationUserServices",
                column: "ApplicationUserServiceId");

            migrationBuilder.CreateTable(
                name: "UserSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsPurcharsed = table.Column<bool>(type: "bit", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscriptions_SubscriptionTypies_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "SubscriptionTypies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationUserServiceId",
                table: "Messages",
                column: "ApplicationUserServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserServices_ApplicationUserId",
                table: "ApplicationUserServices",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_ApplicationUserId",
                table: "UserSubscriptions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_SubscriptionTypeId",
                table: "UserSubscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUserServices_ApplicationUserServiceId",
                table: "Messages",
                column: "ApplicationUserServiceId",
                principalTable: "ApplicationUserServices",
                principalColumn: "ApplicationUserServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ApplicationUserServices_ApplicationUserServiceId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "UserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationUserServiceId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserServices",
                table: "ApplicationUserServices");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserServices_ApplicationUserId",
                table: "ApplicationUserServices");

            migrationBuilder.DropColumn(
                name: "IsFromProvider",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserServiceId",
                table: "ApplicationUserServices");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserServiceId",
                table: "Messages",
                newName: "ChatId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserServiceApplicationUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserServiceServiceId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserServices",
                table: "ApplicationUserServices",
                columns: new[] { "ApplicationUserId", "ServiceId" });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionTypeId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPurcharsed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_SubscriptionTypies_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "SubscriptionTypies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationUserServiceApplicationUserId_ApplicationUserServiceServiceId",
                table: "Messages",
                columns: new[] { "ApplicationUserServiceApplicationUserId", "ApplicationUserServiceServiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationUserId",
                table: "Payments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionTypeId",
                table: "Payments",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ApplicationUserServices_ApplicationUserServiceApplicationUserId_ApplicationUserServiceServiceId",
                table: "Messages",
                columns: new[] { "ApplicationUserServiceApplicationUserId", "ApplicationUserServiceServiceId" },
                principalTable: "ApplicationUserServices",
                principalColumns: new[] { "ApplicationUserId", "ServiceId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
