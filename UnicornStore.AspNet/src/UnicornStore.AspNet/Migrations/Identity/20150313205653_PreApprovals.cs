using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.Identity
{
    public partial class PreApprovals : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetPreApprovals",
                x => new
                {
                    ApprovedBy = x.Column("nvarchar(max)", nullable: true),
                    ApprovedOn = x.Column("datetime2"),
                    Role = x.Column("nvarchar(450)", nullable: true),
                    UserEmail = x.Column("nvarchar(450)", nullable: true)
                })
                .PrimaryKey(x => new { x.UserEmail, x.Role }, name: "PK_AspNetPreApprovals");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetPreApprovals");
        }
    }
}
