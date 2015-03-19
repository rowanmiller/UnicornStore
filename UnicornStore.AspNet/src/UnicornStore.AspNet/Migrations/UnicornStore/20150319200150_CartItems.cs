using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class CartItems : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "CartItem",
                x => new
                {
                    CartItemId = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } }),
                    PriceCalculated = x.Column("datetime2"),
                    PricePerUnit = x.Column("decimal(18, 2)"),
                    ProductId = x.Column("int"),
                    Quantity = x.Column("int"),
                    Username = x.Column("nvarchar(max)", nullable: true)
                })
                .PrimaryKey(x => x.CartItemId, name: "PK_CartItem")
                .ForeignKey(x => x.ProductId, "Product", principalColumn: "ProductId", name: "FK_CartItem_Product_ProductId");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("CartItem");
        }
    }
}
