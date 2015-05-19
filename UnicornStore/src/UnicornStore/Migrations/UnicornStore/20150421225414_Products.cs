using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class Products : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
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
                name: "Product",
                columns: table => new
                {
                    CategoryId = table.Column(type: "int", nullable: false),
                    CurrentPrice = table.Column(type: "decimal(18, 2)", nullable: false),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column(type: "nvarchar(max)", nullable: true),
                    MSRP = table.Column(type: "decimal(18, 2)", nullable: false),
                    ProductId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity")
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
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Category");
            migration.DropTable("Product");
        }
    }
}
