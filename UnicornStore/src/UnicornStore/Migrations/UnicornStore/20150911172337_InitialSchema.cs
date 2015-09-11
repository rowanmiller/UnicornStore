using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace UnicornStore.Migrations.UnicornStore
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(isNullable: true),
                    ParentCategoryId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    CheckoutBegan = table.Column<DateTime>(isNullable: false),
                    OrderPlaced = table.Column<DateTime>(isNullable: true),
                    State = table.Column<int>(isNullable: false),
                    Total = table.Column<decimal>(isNullable: false),
                    Username = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });
            migrationBuilder.CreateTable(
                name: "WebsiteAd",
                columns: table => new
                {
                    WebsiteAdId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Details = table.Column<string>(isNullable: true),
                    End = table.Column<DateTime>(isNullable: true),
                    ImageUrl = table.Column<string>(isNullable: true),
                    Start = table.Column<DateTime>(isNullable: true),
                    TagLine = table.Column<string>(isNullable: true),
                    Url = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteAd", x => x.WebsiteAdId);
                });
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(isNullable: false),
                    CurrentPrice = table.Column<decimal>(isNullable: false),
                    Description = table.Column<string>(isNullable: true),
                    DisplayName = table.Column<string>(isNullable: true),
                    ImageUrl = table.Column<string>(isNullable: true),
                    MSRP = table.Column<decimal>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });
            migrationBuilder.CreateTable(
                name: "OrderShippingDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(isNullable: false),
                    Addressee = table.Column<string>(isNullable: false),
                    CityOrTown = table.Column<string>(isNullable: false),
                    Country = table.Column<string>(isNullable: false),
                    LineOne = table.Column<string>(isNullable: false),
                    LineTwo = table.Column<string>(isNullable: true),
                    StateOrProvince = table.Column<string>(isNullable: false),
                    ZipOrPostalCode = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShippingDetails", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderShippingDetails_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                });
            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    LastUpdated = table.Column<DateTime>(isNullable: false),
                    PriceCalculated = table.Column<DateTime>(isNullable: false),
                    PricePerUnit = table.Column<decimal>(isNullable: false),
                    ProductId = table.Column<int>(isNullable: false),
                    Quantity = table.Column<int>(isNullable: false),
                    Username = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });
            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    OrderId = table.Column<int>(isNullable: false),
                    ProductId = table.Column<int>(isNullable: false),
                    PricePerUnit = table.Column<decimal>(isNullable: false),
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
                    table.ForeignKey(
                        name: "FK_OrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.Sql(@"IF OBJECT_ID(N'[dbo].[SearchProducts]') IS NULL 
                                EXEC('CREATE FUNCTION [dbo].[SearchProducts] ( @term nvarchar(200) )
                                      RETURNS TABLE
                                      AS
                                      RETURN
                                      (
                                          SELECT *
                                          FROM dbo.Product
                                          WHERE Product.DisplayName LIKE ''%'' + @term + ''%''
                                          OR Product.Description LIKE ''%'' + @term + ''%''
                                      )')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CartItem");
            migrationBuilder.DropTable("OrderLine");
            migrationBuilder.DropTable("OrderShippingDetails");
            migrationBuilder.DropTable("WebsiteAd");
            migrationBuilder.DropTable("Product");
            migrationBuilder.DropTable("Order");
            migrationBuilder.DropTable("Category");
        }
    }
}
