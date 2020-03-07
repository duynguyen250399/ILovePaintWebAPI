using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Phone = table.Column<string>(maxLength: 11, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Price = table.Column<float>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ManufactureDate = table.Column<DateTime>(nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ProviderID = table.Column<int>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Provider_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Provider",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Color_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductID",
                table: "Color",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProviderID",
                table: "Products",
                column: "ProviderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Provider");
        }
    }
}
