using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.Identity
{
    public partial class Addresses : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetUserAddresses",
                x => new
                {
                    Addressee = x.Column("nvarchar(max)"),
                    CityOrTown = x.Column("nvarchar(max)"),
                    Country = x.Column("nvarchar(max)"),
                    LineOne = x.Column("nvarchar(max)"),
                    LineTwo = x.Column("nvarchar(max)", nullable: true),
                    StateOrProvince = x.Column("nvarchar(max)"),
                    UserAddressId = x.Column("int"),
                    UserId = x.Column("nvarchar(450)"),
                    ZipOrPostalCode = x.Column("nvarchar(max)")
                })
                .PrimaryKey(x => x.UserAddressId, name: "PK_AspNetUserAddresses")
                .ForeignKey(x => x.UserId, "AspNetUsers", principalColumn: "Id", name: "FK_AspNetUserAddresses_AspNetUsers_UserId");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetUserAddresses");
        }
    }
}
