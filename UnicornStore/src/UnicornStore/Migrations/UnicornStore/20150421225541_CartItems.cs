using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class CartItems : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
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
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("CartItem");
        }
    }
}
