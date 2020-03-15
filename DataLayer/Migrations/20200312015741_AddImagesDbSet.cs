using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddImagesDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AppImage_AppImageID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppImage",
                table: "AppImage");

            migrationBuilder.RenameTable(
                name: "AppImage",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_AppImageID",
                table: "Products",
                column: "AppImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_AppImageID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "AppImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppImage",
                table: "AppImage",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AppImage_AppImageID",
                table: "Products",
                column: "AppImageID",
                principalTable: "AppImage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
