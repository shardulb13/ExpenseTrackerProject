using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceTrackingWebAPI.Migrations
{
    public partial class FriendRequestModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SentBy",
                table: "FriendRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentBy",
                table: "FriendRequests");
        }
    }
}
