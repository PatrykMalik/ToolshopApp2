using Microsoft.EntityFrameworkCore.Migrations;

namespace ToolshopApp2.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatuses_Requests_RequestId",
                table: "RequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatuses_RequestId",
                table: "RequestStatuses");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "RequestStatuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "RequestStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_RequestId",
                table: "RequestStatuses",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatuses_Requests_RequestId",
                table: "RequestStatuses",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
