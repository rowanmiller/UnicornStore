using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class Categories : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Category",
                x => new
                {
                    CategoryId = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } }),
                    DisplayName = x.Column("nvarchar(max)", nullable: true),
                    ParentCategoryId = x.Column("int", nullable: true)
                })
                .PrimaryKey(x => x.CategoryId, name: "PK_Category")
                .ForeignKey(x => x.ParentCategoryId, "Category", principalColumn: "CategoryId", name: "FK_Category_Category_ParentCategoryId");

            migrationBuilder.Sql("SET IDENTITY_INSERT [Category] ON");
            migrationBuilder.Sql("INSERT INTO [Category] (CategoryId, DisplayName) VALUES (1, 'Uncategorized')");
            migrationBuilder.Sql("SET IDENTITY_INSERT [Category] OFF");

            migrationBuilder.AddColumn("Product", "CategoryId", x => x.Column("int", defaultValue: 1));
            migrationBuilder.AddForeignKey("Product", "CategoryId", "Category", principalColumn: "CategoryId", name: "FK_Product_Category_CategoryId");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("Product", "FK_Product_Category_CategoryId");
            migrationBuilder.DropTable("Category");
            migrationBuilder.DropColumn("Product", "CategoryId");
        }
    }
}
