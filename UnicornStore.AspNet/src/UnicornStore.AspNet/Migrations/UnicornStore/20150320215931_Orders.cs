using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class Orders : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Order",
                x => new
                {
                    CheckoutBegan = x.Column("datetime2"),
                    OrderId = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } }),
                    OrderPlaced = x.Column("datetime2", nullable: true),
                    ShippingAddressLineOne = x.Column("nvarchar(max)", nullable: true),
                    ShippingAddressLineTwo = x.Column("nvarchar(max)", nullable: true),
                    ShippingAddressee = x.Column("nvarchar(max)", nullable: true),
                    ShippingCityOrTown = x.Column("nvarchar(max)", nullable: true),
                    ShippingCountry = x.Column("nvarchar(max)", nullable: true),
                    ShippingStateOrProvince = x.Column("nvarchar(max)", nullable: true),
                    ShippingZipOrPostalCode = x.Column("nvarchar(max)", nullable: true),
                    State = x.Column("int"),
                    Total = x.Column("decimal(18, 2)"),
                    Username = x.Column("nvarchar(max)", nullable: true)
                })
                .PrimaryKey(x => x.OrderId, name: "PK_Order");

            migrationBuilder.CreateTable(
                "OrderLine",
                x => new
                {
                    OrderId = x.Column("int"),
                    PricePerUnit = x.Column("decimal(18, 2)"),
                    ProductId = x.Column("int"),
                    Quantity = x.Column("int")
                })
                .PrimaryKey(x => new { x.OrderId, x.ProductId }, name: "PK_OrderLine")
                .ForeignKey(x => x.OrderId, "Order", principalColumn: "OrderId", name: "FK_OrderLine_Order_OrderId")
                .ForeignKey(x => x.ProductId, "Product", principalColumn: "ProductId", name: "FK_OrderLine_Product_ProductId");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("OrderLine", "FK_OrderLine_Order_OrderId");
            migrationBuilder.DropForeignKey("OrderLine", "FK_OrderLine_Product_ProductId");
            migrationBuilder.DropTable("Order");
            migrationBuilder.DropTable("OrderLine");
        }
    }
}
