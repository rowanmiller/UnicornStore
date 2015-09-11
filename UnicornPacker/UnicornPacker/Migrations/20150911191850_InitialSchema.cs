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
                    OrderId = table.Column<int>(isNullable: false),
                    Addressee = table.Column<string>(isNullable: true),
                    CityOrTown = table.Column<string>(isNullable: true),
                    Country = table.Column<string>(isNullable: true),
                    IsShipped = table.Column<bool>(isNullable: false),
                    IsShippingSynced = table.Column<bool>(isNullable: false),
                    LineOne = table.Column<string>(isNullable: true),
                    LineTwo = table.Column<string>(isNullable: true),
                    StateOrProvince = table.Column<string>(isNullable: true),
                    ZipOrPostalCode = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column<int>(isNullable: false),
                    ProductId = table.Column<int>(isNullable: false),
                    IsPacked = table.Column<bool>(isNullable: false),
                    ProductName = table.Column<string>(isNullable: true),
                    Quantity = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("OrderLine");
            migrationBuilder.DropTable("Order");
        }
    }
}
