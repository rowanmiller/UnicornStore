using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace UnicornPackerMigrations
{
    public partial class InitialSchema : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column(type: "INTEGER", nullable: false),
                    Addressee = table.Column(type: "TEXT", nullable: true),
                    CityOrTown = table.Column(type: "TEXT", nullable: true),
                    Country = table.Column(type: "TEXT", nullable: true),
                    IsShipped = table.Column(type: "INTEGER", nullable: false),
                    IsShippingSynced = table.Column(type: "INTEGER", nullable: false),
                    LineOne = table.Column(type: "TEXT", nullable: true),
                    LineTwo = table.Column(type: "TEXT", nullable: true),
                    StateOrProvince = table.Column(type: "TEXT", nullable: true),
                    ZipOrPostalCode = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migration.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column(type: "INTEGER", nullable: false),
                    ProductId = table.Column(type: "INTEGER", nullable: false),
                    IsPacked = table.Column(type: "INTEGER", nullable: false),
                    ProductName = table.Column(type: "TEXT", nullable: true),
                    Quantity = table.Column(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        columns: x => x.OrderId,
                        referencedTable: "Order",
                        referencedColumn: "OrderId");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("OrderLine");
            migration.DropTable("Order");
        }
    }
}
