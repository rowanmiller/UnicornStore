using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace UnicornClicker.UWP.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameScore",
                columns: table => new
                {
                    GameScoreId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clicks = table.Column<int>(nullable: false),
                    ClicksPerSecond = table.Column<double>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Played = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScore", x => x.GameScoreId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("GameScore");
        }
    }
}
