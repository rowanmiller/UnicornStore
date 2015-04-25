using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.Identity
{
    public partial class PreApprovals : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "AspNetPreApprovals",
                columns: table => new
                {
                    ApprovedBy = table.Column(type: "nvarchar(max)", nullable: true),
                    ApprovedOn = table.Column(type: "datetime2", nullable: false),
                    Role = table.Column(type: "nvarchar(450)", nullable: true),
                    UserEmail = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPreApprovals", x => new { x.UserEmail, x.Role });
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("AspNetPreApprovals");
        }
    }
}
