using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetRoles",
                x => new
                {
                    ConcurrencyStamp = x.Column("nvarchar(max)", nullable: true),
                    Id = x.Column("nvarchar(450)", nullable: true),
                    Name = x.Column("nvarchar(max)", nullable: true),
                    NormalizedName = x.Column("nvarchar(max)", nullable: true)})
                .PrimaryKey(x => x.Id, name: "PK_AspNetRoles");
            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                x => new
                {
                    RoleId = x.Column("nvarchar(450)", nullable: true),
                    UserId = x.Column("nvarchar(450)", nullable: true)})
                .PrimaryKey(x => new { x.UserId, x.RoleId }, name: "PK_AspNetUserRoles");
            migrationBuilder.CreateTable(
                "AspNetUsers",
                x => new
                {
                    AccessFailedCount = x.Column("int"),
                    ConcurrencyStamp = x.Column("nvarchar(max)", nullable: true),
                    Email = x.Column("nvarchar(max)", nullable: true),
                    EmailConfirmed = x.Column("bit"),
                    Id = x.Column("nvarchar(450)", nullable: true),
                    LockoutEnabled = x.Column("bit"),
                    LockoutEnd = x.Column("datetimeoffset", nullable: true),
                    NormalizedEmail = x.Column("nvarchar(max)", nullable: true),
                    NormalizedUserName = x.Column("nvarchar(max)", nullable: true),
                    PasswordHash = x.Column("nvarchar(max)", nullable: true),
                    PhoneNumber = x.Column("nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = x.Column("bit"),
                    SecurityStamp = x.Column("nvarchar(max)", nullable: true),
                    TwoFactorEnabled = x.Column("bit"),
                    UserName = x.Column("nvarchar(max)", nullable: true)})
                .PrimaryKey(x => x.Id, name: "PK_AspNetUsers");
            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                x => new
                {
                    ClaimType = x.Column("nvarchar(max)", nullable: true),
                    ClaimValue = x.Column("nvarchar(max)", nullable: true),
                    Id = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } }),
                    RoleId = x.Column("nvarchar(450)", nullable: true)})
                .PrimaryKey(x => x.Id, name: "PK_AspNetRoleClaims")
                .ForeignKey(x => x.RoleId, "AspNetRoles", principalColumns: new string[] { "Id" }, name: "FK_AspNetRoleClaims_AspNetRoles_RoleId");
            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                x => new
                {
                    ClaimType = x.Column("nvarchar(max)", nullable: true),
                    ClaimValue = x.Column("nvarchar(max)", nullable: true),
                    Id = x.Column("int", annotations: new Dictionary<string, string> { { "SqlServer:ValueGeneration", "Identity" } }),
                    UserId = x.Column("nvarchar(450)", nullable: true)})
                .PrimaryKey(x => x.Id, name: "PK_AspNetUserClaims")
                .ForeignKey(x => x.UserId, "AspNetUsers", principalColumns: new string[] { "Id" }, name: "FK_AspNetUserClaims_AspNetUsers_UserId");
            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                x => new
                {
                    LoginProvider = x.Column("nvarchar(450)", nullable: true),
                    ProviderDisplayName = x.Column("nvarchar(max)", nullable: true),
                    ProviderKey = x.Column("nvarchar(450)", nullable: true),
                    UserId = x.Column("nvarchar(450)", nullable: true)})
                .PrimaryKey(x => new { x.LoginProvider, x.ProviderKey }, name: "PK_AspNetUserLogins")
                .ForeignKey(x => x.UserId, "AspNetUsers", principalColumns: new string[] { "Id" }, name: "FK_AspNetUserLogins_AspNetUsers_UserId");
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("AspNetUsers");
        }
    }
}
