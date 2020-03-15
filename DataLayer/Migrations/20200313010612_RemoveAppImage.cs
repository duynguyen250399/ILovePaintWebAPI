using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemoveAppImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_AppImageID",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Products_AppImageID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AppImageID",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "AppImageID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AppImageID",
                table: "Products",
                column: "AppImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_AppImageID",
                table: "Products",
                column: "AppImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
