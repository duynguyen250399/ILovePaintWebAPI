using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddAppImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "AppImageID",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppImage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImage", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AppImageID",
                table: "Products",
                column: "AppImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AppImage_AppImageID",
                table: "Products",
                column: "AppImageID",
                principalTable: "AppImage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AppImage_AppImageID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "AppImage");

            migrationBuilder.DropIndex(
                name: "IX_Products_AppImageID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AppImageID",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
