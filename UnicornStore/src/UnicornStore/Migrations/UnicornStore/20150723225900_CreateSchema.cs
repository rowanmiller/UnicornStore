using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace UnicornStore.Migrations.UnicornStore
{
    public partial class CreateSchema : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    DisplayName = table.Column(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        columns: x => x.ParentCategoryId,
                        referencedTable: "Category",
                        referencedColumn: "CategoryId");
                });
            migration.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    CheckoutBegan = table.Column(type: "datetime2", nullable: false),
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
                name: "WebsiteAd",
                columns: table => new
                {
                    WebsiteAdId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Details = table.Column(type: "nvarchar(max)", nullable: true),
                    End = table.Column(type: "datetime2", nullable: true),
                    ImageUrl = table.Column(type: "nvarchar(max)", nullable: true),
                    Start = table.Column(type: "datetime2", nullable: true),
                    TagLine = table.Column(type: "nvarchar(max)", nullable: true),
                    Url = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteAd", x => x.WebsiteAdId);
                });
            migration.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    CategoryId = table.Column(type: "int", nullable: false),
                    CurrentPrice = table.Column(type: "decimal(18, 2)", nullable: false),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column(type: "nvarchar(max)", nullable: true),
                    MSRP = table.Column(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        columns: x => x.CategoryId,
                        referencedTable: "Category",
                        referencedColumn: "CategoryId");
                });
            migration.CreateTable(
                name: "OrderShippingDetails",
                columns: table => new
                {
                    OrderId = table.Column(type: "int", nullable: false),
                    Addressee = table.Column(type: "nvarchar(max)", nullable: false),
                    CityOrTown = table.Column(type: "nvarchar(max)", nullable: false),
                    Country = table.Column(type: "nvarchar(max)", nullable: false),
                    LineOne = table.Column(type: "nvarchar(max)", nullable: false),
                    LineTwo = table.Column(type: "nvarchar(max)", nullable: true),
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
            migration.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    LastUpdated = table.Column(type: "datetime2", nullable: false),
                    PriceCalculated = table.Column(type: "datetime2", nullable: false),
                    PricePerUnit = table.Column(type: "decimal(18, 2)", nullable: false),
                    ProductId = table.Column(type: "int", nullable: false),
                    Quantity = table.Column(type: "int", nullable: false),
                    Username = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        columns: x => x.ProductId,
                        referencedTable: "Product",
                        referencedColumn: "ProductId");
                });
            migration.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column(type: "int", nullable: false),
                    ProductId = table.Column(type: "int", nullable: false),
                    PricePerUnit = table.Column(type: "decimal(18, 2)", nullable: false),
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
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("CartItem");
            migration.DropTable("OrderLine");
            migration.DropTable("OrderShippingDetails");
            migration.DropTable("WebsiteAd");
            migration.DropTable("Product");
            migration.DropTable("Order");
            migration.DropTable("Category");
        }
    }
}
