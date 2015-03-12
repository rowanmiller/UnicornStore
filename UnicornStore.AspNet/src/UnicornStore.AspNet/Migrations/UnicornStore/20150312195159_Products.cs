using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class Products : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Product",
                x => new
                {
                    CurrentPrice = x.Column("decimal(18, 2)"),
                    Description = x.Column("nvarchar(max)", nullable: true),
                    DisplayName = x.Column("nvarchar(max)", nullable: true),
                    MSRP = x.Column("decimal(18, 2)"),
                    ProductId = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } })
                })
                .PrimaryKey(x => x.ProductId, name: "PK_Product");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Product");
        }
    }
}
