using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class Orders : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Order",
                columns: table => new
                {
                    CheckoutBegan = table.Column(type: "datetime2", nullable: false),
                    OrderId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    OrderPlaced = table.Column(type: "datetime2", nullable: true),
                    State = table.Column(type: "int", nullable: false),
                    Total = table.Column(type: "decimal(18, 2)", nullable: false),
                    Username = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migration.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column(type: "int", nullable: false),
                    PricePerUnit = table.Column(type: "decimal(18, 2)", nullable: false),
                    ProductId = table.Column(type: "int", nullable: false),
                    Quantity = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        columns: x => x.OrderId,
                        referencedTable: "Order",
                        referencedColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ProductId",
                        columns: x => x.ProductId,
                        referencedTable: "Product",
                        referencedColumn: "ProductId");
                });
            migration.CreateTable(
                name: "OrderShippingDetails",
                columns: table => new
                {
                    Addressee = table.Column(type: "nvarchar(max)", nullable: false),
                    CityOrTown = table.Column(type: "nvarchar(max)", nullable: false),
                    Country = table.Column(type: "nvarchar(max)", nullable: false),
                    LineOne = table.Column(type: "nvarchar(max)", nullable: false),
                    LineTwo = table.Column(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column(type: "int", nullable: false),
                    StateOrProvince = table.Column(type: "nvarchar(max)", nullable: false),
                    ZipOrPostalCode = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShippingDetails", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderShippingDetails_Order_OrderId",
                        columns: x => x.OrderId,
                        referencedTable: "Order",
                        referencedColumn: "OrderId");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Order");
            migration.DropTable("OrderLine");
            migration.DropTable("OrderShippingDetails");
        }
    }
}
