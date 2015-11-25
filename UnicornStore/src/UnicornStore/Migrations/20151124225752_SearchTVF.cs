using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace UnicornStore.Migrations
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
                                          FROM dbo.Product
                                          WHERE Product.DisplayName LIKE ''%'' + @term + ''%''
                                          OR Product.Description LIKE ''%'' + @term + ''%''
                                      )')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
