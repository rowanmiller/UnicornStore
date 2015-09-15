using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace UnicornStore.Migrations.UnicornStore
{
    public partial class Recalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recall",
                columns: table => new
                {
                    RecallId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Details = table.Column<string>(isNullable: true),
                    ProductSKU = table.Column<string>(isNullable: false, type: "nvarchar(200)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recall", x => x.RecallId);
                    table.ForeignKey(
                        name: "FK_Recall_Product_ProductSKU",
                        column: x => x.ProductSKU,
                        principalTable: "Product",
                        principalColumn: "SKU");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Recall");
        }
    }
}
