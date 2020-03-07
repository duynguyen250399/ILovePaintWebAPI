using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Provider_ProviderID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provider",
                table: "Provider");

            migrationBuilder.RenameTable(
                name: "Provider",
                newName: "Providers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Providers",
                table: "Providers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Providers_ProviderID",
                table: "Products",
                column: "ProviderID",
                principalTable: "Providers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Providers_ProviderID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Providers",
                table: "Providers");

            migrationBuilder.RenameTable(
                name: "Providers",
                newName: "Provider");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provider",
                table: "Provider",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Provider_ProviderID",
                table: "Products",
                column: "ProviderID",
                principalTable: "Provider",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
