using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornStore.Migrations.UnicornStore
{
    public partial class SearchTVF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'[dbo].[SearchProducts]') IS NULL 
                                EXEC('CREATE FUNCTION [dbo].[SearchProducts] ( @term nvarchar(200) )
                                      RETURNS TABLE
                                      AS
                                      RETURN
                                      (
                                          SELECT *
                                          FROM dbo.Products
                                          WHERE Products.DisplayName LIKE ''%'' + @term + ''%''
                                          OR Products.Description LIKE ''%'' + @term + ''%''
                                      )')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
