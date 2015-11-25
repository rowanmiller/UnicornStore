using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace UnicornStore.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckoutBegan = table.Column<DateTime>(nullable: false),
                    DisplayId = table.Column<string>(nullable: true),
                    OrderPlaced = table.Column<DateTime>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migrationBuilder.CreateTable(
                name: "WebsiteAd",
                columns: table => new
                {
                    WebsiteAdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Details = table.Column<string>(nullable: true),
                    End = table.Column<DateTime>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteAd", x => x.WebsiteAdId);
                });
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    CurrentPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    MSRP = table.Column<decimal>(nullable: false),
                    SKU = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.UniqueConstraint("AK_Product_SKU", x => x.SKU);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "OrderShippingDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Addressee = table.Column<string>(nullable: false),
                    CityOrTown = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    LineOne = table.Column<string>(nullable: false),
                    LineTwo = table.Column<string>(nullable: true),
                    StateOrProvince = table.Column<string>(nullable: false),
                    ZipOrPostalCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShippingDetails", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderShippingDetails_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PriceCalculated = table.Column<DateTime>(nullable: false),
                    PricePerUnit = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    PricePerUnit = table.Column<decimal>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Recall",
                columns: table => new
                {
                    RecallId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Details = table.Column<string>(nullable: true),
                    ProductSKU = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recall", x => x.RecallId);
                    table.ForeignKey(
                        name: "FK_Recall_Product_ProductSKU",
                        column: x => x.ProductSKU,
                        principalTable: "Product",
                        principalColumn: "SKU",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CartItem");
            migrationBuilder.DropTable("OrderLine");
            migrationBuilder.DropTable("OrderShippingDetails");
            migrationBuilder.DropTable("Recall");
            migrationBuilder.DropTable("WebsiteAd");
            migrationBuilder.DropTable("Order");
            migrationBuilder.DropTable("Product");
            migrationBuilder.DropTable("Category");
        }
    }
}
