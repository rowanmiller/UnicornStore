using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UnicornStore.Migrations.Identity
{
    public partial class Addresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUserAddresses",
                columns: table => new
                {
                    UserAddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Addressee = table.Column<string>(nullable: false),
                    CityOrTown = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    LineOne = table.Column<string>(nullable: false),
                    LineTwo = table.Column<string>(nullable: true),
                    StateOrProvince = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ZipOrPostalCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserAddresses", x => x.UserAddressId);
                    table.ForeignKey(
                        name: "FK_AspNetUserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserAddresses_UserId",
                table: "AspNetUserAddresses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserAddresses");
        }
    }
}
