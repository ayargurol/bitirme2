using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreWebApp2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BaseUrl = table.Column<string>(nullable: false),
                    ImageAttribute = table.Column<string>(type: "varchar", nullable: false),
                    ImageChilds = table.Column<string>(type: "varchar", nullable: false),
                    LinkAttribute = table.Column<string>(type: "varchar", nullable: false),
                    LinkChilds = table.Column<string>(type: "varchar", nullable: false),
                    LinkExtra = table.Column<string>(type: "varchar", nullable: true),
                    NameAttribute = table.Column<string>(nullable: true),
                    NameChilds = table.Column<string>(type: "varchar", nullable: true),
                    PriceAttribute = table.Column<string>(type: "varchar", nullable: true),
                    PriceChilds = table.Column<string>(type: "varchar", nullable: true),
                    PriceChildsTwo = table.Column<string>(type: "varchar", nullable: true),
                    RepatedItem = table.Column<string>(nullable: false),
                    SatisfactionAttribute = table.Column<string>(type: "varchar", nullable: true),
                    SatisfactionChilds = table.Column<string>(type: "varchar", nullable: true),
                    SearcUrlPart2 = table.Column<string>(nullable: false),
                    SearchUrlPart1 = table.Column<string>(nullable: false),
                    SellerAttribute = table.Column<string>(type: "varchar", nullable: true),
                    SellerChilds = table.Column<string>(type: "varchar", nullable: true),
                    SiteName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sites");
        }
    }
}
