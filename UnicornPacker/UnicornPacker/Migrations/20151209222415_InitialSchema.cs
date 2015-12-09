using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace UnicornPacker.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Addressee = table.Column<string>(nullable: true),
                    CityOrTown = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    IsShipped = table.Column<bool>(nullable: false),
                    IsShippingSynced = table.Column<bool>(nullable: false),
                    LineOne = table.Column<string>(nullable: true),
                    LineTwo = table.Column<string>(nullable: true),
                    StateOrProvince = table.Column<string>(nullable: true),
                    ZipOrPostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    IsPacked = table.Column<bool>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("OrderLine");
            migrationBuilder.DropTable("Order");
        }
    }
}
