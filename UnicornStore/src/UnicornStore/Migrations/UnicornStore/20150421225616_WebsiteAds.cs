using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class WebsiteAds : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "WebsiteAd",
                columns: table => new
                {
                    Details = table.Column(type: "nvarchar(max)", nullable: true),
                    End = table.Column(type: "datetime2", nullable: true),
                    ImageUrl = table.Column(type: "nvarchar(max)", nullable: true),
                    Start = table.Column(type: "datetime2", nullable: true),
                    TagLine = table.Column(type: "nvarchar(max)", nullable: true),
                    Url = table.Column(type: "nvarchar(max)", nullable: true),
                    WebsiteAdId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteAd", x => x.WebsiteAdId);
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("WebsiteAd");
        }
    }
}
