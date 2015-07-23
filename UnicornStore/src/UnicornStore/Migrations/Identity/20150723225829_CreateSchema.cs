using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace UnicornStore.Migrations.Identity
{
    public partial class CreateSchema : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column(type: "nvarchar(max)", nullable: true),
                    Name = table.Column(type: "nvarchar(256)", nullable: true),
                    NormalizedName = table.Column(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migration.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column(type: "nvarchar(max)", nullable: true),
                    Email = table.Column(type: "nvarchar(256)", nullable: true),
                    EmailConfirmed = table.Column(type: "bit", nullable: false),
                    LockoutEnabled = table.Column(type: "bit", nullable: false),
                    LockoutEnd = table.Column(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column(type: "nvarchar(256)", nullable: true),
                    NormalizedUserName = table.Column(type: "nvarchar(256)", nullable: true),
                    PasswordHash = table.Column(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column(type: "bit", nullable: false),
                    SecurityStamp = table.Column(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column(type: "bit", nullable: false),
                    UserName = table.Column(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });
            migration.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    ClaimType = table.Column(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        columns: x => x.RoleId,
                        referencedTable: "AspNetRoles",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    ClaimType = table.Column(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        columns: x => x.RoleId,
                        referencedTable: "AspNetRoles",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "AspNetUserAddresses",
                columns: table => new
                {
                    UserAddressId = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Addressee = table.Column(type: "nvarchar(max)", nullable: false),
                    CityOrTown = table.Column(type: "nvarchar(max)", nullable: false),
                    Country = table.Column(type: "nvarchar(max)", nullable: false),
                    LineOne = table.Column(type: "nvarchar(max)", nullable: false),
                    LineTwo = table.Column(type: "nvarchar(max)", nullable: true),
                    StateOrProvince = table.Column(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column(type: "nvarchar(450)", nullable: false),
                    ZipOrPostalCode = table.Column(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.UserAddressId);
                    table.ForeignKey(
                        name: "FK_UserAddress_ApplicationUser_UserId",
                        columns: x => x.UserId,
                        referencedTable: "AspNetUsers",
                        referencedColumn: "Id");
                });
            migration.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migration.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migration.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("AspNetRoleClaims");
            migration.DropTable("AspNetUserClaims");
            migration.DropTable("AspNetUserLogins");
            migration.DropTable("AspNetUserRoles");
            migration.DropTable("AspNetUserAddresses");
            migration.DropTable("AspNetRoles");
            migration.DropTable("AspNetUsers");
        }
    }
}
