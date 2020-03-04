using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToolshopApp2.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(nullable: true),
                    Classyfy = table.Column<string>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Project = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CostCenter = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Attachment = table.Column<bool>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BeginigSrz = table.Column<string>(nullable: true),
                    EndingSrz = table.Column<string>(nullable: true),
                    Transpot = table.Column<string>(nullable: true),
                    Insurance = table.Column<bool>(nullable: false),
                    InsuranceCost = table.Column<string>(nullable: true),
                    Srz1 = table.Column<string>(nullable: true),
                    Srz2 = table.Column<string>(nullable: true),
                    Srz3 = table.Column<string>(nullable: true),
                    Srz4 = table.Column<string>(nullable: true),
                    Srz5 = table.Column<string>(nullable: true),
                    Swz1 = table.Column<string>(nullable: true),
                    Swz2 = table.Column<string>(nullable: true),
                    Swz3 = table.Column<string>(nullable: true),
                    Swz4 = table.Column<string>(nullable: true),
                    Swz5 = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    DescpriptionToolshop = table.Column<string>(nullable: true),
                    PackageDimmention = table.Column<string>(nullable: true),
                    PackageWeight = table.Column<string>(nullable: true),
                    PackageType = table.Column<string>(nullable: true),
                    PackageOnPalltete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
