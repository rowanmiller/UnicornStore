using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.Identity
{
    public partial class UserAddresses : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "AspNetUserAddresses",
                columns: table => new
                {
                    Addressee = table.Column(type: "nvarchar(max)", nullable: false),
                    CityOrTown = table.Column(type: "nvarchar(max)", nullable: false),
                    Country = table.Column(type: "nvarchar(max)", nullable: false),
                    LineOne = table.Column(type: "nvarchar(max)", nullable: false),
                    LineTwo = table.Column(type: "nvarchar(max)", nullable: true),
                    StateOrProvince = table.Column(type: "nvarchar(max)", nullable: false),
                    UserAddressId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGeneration", "Identity"),
                    UserId = table.Column(type: "nvarchar(450)", nullable: false),
                    ZipOrPostalCode = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserAddresses", x => x.UserAddressId);
                    table.ForeignKey(
                        name: "FK_AspNetUserAddresses_AspNetUsers_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("AspNetUserAddresses");
        }
    }
}
