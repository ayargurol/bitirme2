using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreWebApp2.Migrations
{
    public partial class SearchedItemTableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchedItems",
                columns: table => new
                {
                    RowID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SearchedWord = table.Column<string>(nullable: false),
                    count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchedItems", x => x.RowID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchedItems");
        }
    }
}
