using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class LastUpdated : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "LastUpdated",
                table: "CartItem",
                type: "datetime2",
                nullable: false);
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "LastUpdated", table: "CartItem");
        }
    }
}
