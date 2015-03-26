using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class InitialSchema : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence("DefaultSequence", storeType: "bigint");
            migrationBuilder.CreateTable(
                "Category",
                x => new
                {
                    CategoryId = x.Column("int"),
                    DisplayName = x.Column("nvarchar(max)", nullable: true),
                    ParentCategoryId = x.Column("int", nullable: true)
                })
                .PrimaryKey(x => x.CategoryId, name: "PK_Category")
                .ForeignKey(x => x.ParentCategoryId, "Category", principalColumn: "CategoryId", name: "FK_Category_Category_ParentCategoryId");
            migrationBuilder.CreateTable(
                "Order",
                x => new
                {
                    CheckoutBegan = x.Column("datetime2"),
                    OrderId = x.Column("int"),
                    OrderPlaced = x.Column("datetime2", nullable: true),
                    State = x.Column("int"),
                    Total = x.Column("decimal(18, 2)"),
                    Username = x.Column("nvarchar(max)", nullable: true)
                })
                .PrimaryKey(x => x.OrderId, name: "PK_Order");
            migrationBuilder.CreateTable(
                "WebsiteAd",
                x => new
                {
                    Details = x.Column("nvarchar(max)", nullable: true),
                    End = x.Column("datetime2", nullable: true),
                    ImageUrl = x.Column("nvarchar(max)", nullable: true),
                    Start = x.Column("datetime2", nullable: true),
                    TagLine = x.Column("nvarchar(max)", nullable: true),
                    Url = x.Column("nvarchar(max)", nullable: true),
                    WebsiteAdId = x.Column("int")
                })
                .PrimaryKey(x => x.WebsiteAdId, name: "PK_WebsiteAd");
            migrationBuilder.CreateTable(
                "Product",
                x => new
                {
                    CategoryId = x.Column("int"),
                    CurrentPrice = x.Column("decimal(18, 2)"),
                    Description = x.Column("nvarchar(max)", nullable: true),
                    DisplayName = x.Column("nvarchar(max)", nullable: true),
                    ImageUrl = x.Column("nvarchar(max)", nullable: true),
                    MSRP = x.Column("decimal(18, 2)"),
                    ProductId = x.Column("int")
                })
                .PrimaryKey(x => x.ProductId, name: "PK_Product")
                .ForeignKey(x => x.CategoryId, "Category", principalColumn: "CategoryId", name: "FK_Product_Category_CategoryId");
            migrationBuilder.CreateTable(
                "OrderShippingDetails",
                x => new
                {
                    Addressee = x.Column("nvarchar(max)"),
                    CityOrTown = x.Column("nvarchar(max)"),
                    Country = x.Column("nvarchar(max)"),
                    LineOne = x.Column("nvarchar(max)"),
                    LineTwo = x.Column("nvarchar(max)", nullable: true),
                    OrderId = x.Column("int"),
                    StateOrProvince = x.Column("nvarchar(max)"),
                    ZipOrPostalCode = x.Column("nvarchar(max)")
                })
                .PrimaryKey(x => x.OrderId, name: "PK_OrderShippingDetails")
                .ForeignKey(x => x.OrderId, "Order", principalColumn: "OrderId", name: "FK_OrderShippingDetails_Order_OrderId");
            migrationBuilder.CreateTable(
                "CartItem",
                x => new
                {
                    CartItemId = x.Column("int"),
                    PriceCalculated = x.Column("datetime2"),
                    PricePerUnit = x.Column("decimal(18, 2)"),
                    ProductId = x.Column("int"),
                    Quantity = x.Column("int"),
                    Username = x.Column("nvarchar(max)", nullable: true)
                })
                .PrimaryKey(x => x.CartItemId, name: "PK_CartItem")
                .ForeignKey(x => x.ProductId, "Product", principalColumn: "ProductId", name: "FK_CartItem_Product_ProductId");
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
            migrationBuilder.DropTable("CartItem");
            migrationBuilder.DropTable("Category");
            migrationBuilder.DropTable("Order");
            migrationBuilder.DropTable("OrderLine");
            migrationBuilder.DropTable("OrderShippingDetails");
            migrationBuilder.DropTable("Product");
            migrationBuilder.DropTable("WebsiteAd");
            migrationBuilder.DropSequence("DefaultSequence");
        }
    }
}
