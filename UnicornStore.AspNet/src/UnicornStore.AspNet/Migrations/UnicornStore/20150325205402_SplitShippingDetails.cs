using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;

namespace UnicornStore.AspNet.Migrations.UnicornStore
{
    public partial class SplitShippingDetails : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "OrderShippingDetails",
                x => new
                {
                    Addressee = x.Column("nvarchar(max)"),
                    CityOrTown = x.Column("nvarchar(max)"),
                    Country = x.Column("nvarchar(max)"),
                    LineOne = x.Column("nvarchar(max)"),
                    LineTwo = x.Column("nvarchar(max)", nullable: true),
                    OrderId = x.Column("int"),
                    StateOrProvince = x.Column("nvarchar(max)"),
                    ZipOrPostalCode = x.Column("nvarchar(max)")
                })
                .PrimaryKey(x => x.OrderId, name: "PK_OrderShippingDetails")
                .ForeignKey(x => x.OrderId, "Order", principalColumn: "OrderId", name: "FK_OrderShippingDetails_Order_OrderId");

            migrationBuilder.Sql(@"INSERT INTO [OrderShippingDetails] (OrderId, Addressee, LineOne, LineTwo, CityOrTown, StateOrProvince, ZipOrPostalCode, Country)
                                   SELECT OrderId, ShippingAddressee, ShippingAddressLineOne, ShippingAddressLineTwo, ShippingCityOrTown, ShippingStateOrProvince, ShippingZipOrPostalCode, ShippingCountry
                                   FROM [Order]
                                   WHERE State = 1");

            migrationBuilder.DropColumn("Order", "ShippingAddressLineOne");
            migrationBuilder.DropColumn("Order", "ShippingAddressLineTwo");
            migrationBuilder.DropColumn("Order", "ShippingAddressee");
            migrationBuilder.DropColumn("Order", "ShippingCityOrTown");
            migrationBuilder.DropColumn("Order", "ShippingCountry");
            migrationBuilder.DropColumn("Order", "ShippingStateOrProvince");
            migrationBuilder.DropColumn("Order", "ShippingZipOrPostalCode");
        }

        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("OrderShippingDetails");
            migrationBuilder.AddColumn("Order", "ShippingAddressLineOne", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingAddressLineTwo", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingAddressee", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingCityOrTown", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingCountry", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingStateOrProvince", x => x.Column("nvarchar(max)", nullable: true));
            migrationBuilder.AddColumn("Order", "ShippingZipOrPostalCode", x => x.Column("nvarchar(max)", nullable: true));
        }
    }
}
