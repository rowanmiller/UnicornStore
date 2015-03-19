using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class BannerAd : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "WebsiteAd",
                x => new
                {
                    Details = x.Column("nvarchar(max)", nullable: true),
                    End = x.Column("datetime2", nullable: true),
                    ImageUrl = x.Column("nvarchar(max)", nullable: true),
                    Start = x.Column("datetime2", nullable: true),
                    TagLine = x.Column("nvarchar(max)", nullable: true),
                    Url = x.Column("nvarchar(max)", nullable: true),
                    WebsiteAdId = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } })
                })
                .PrimaryKey(x => x.WebsiteAdId, name: "PK_WebsiteAd");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("WebsiteAd");
        }
    }
}
