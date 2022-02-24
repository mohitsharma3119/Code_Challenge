using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DealerTrack.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealerships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dealnumber = table.Column<int>(nullable: false),
                    customername = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    dealershipName = table.Column<string>(maxLength: 100, nullable: false),
                    vehicle = table.Column<string>(maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealerships", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dealerships");
        }
    }
}
