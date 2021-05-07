using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToolshopApp2.Migrations
{
    public partial class DishwasherOnStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishwashersOnStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(nullable: true),
                    Pnc = table.Column<string>(nullable: true),
                    Elc = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Assigment = table.Column<string>(nullable: true),
                    ScanDate = table.Column<DateTime>(nullable: true),
                    BegingSrz = table.Column<string>(nullable: true),
                    EndingSrz = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishwashersOnStock", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishwashersOnStock");
        }
    }
}
