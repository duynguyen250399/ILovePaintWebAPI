using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class MofifyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVolumes_Products_ProductID",
                table: "ProductVolumes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVolumes_Volumes_VolumeID",
                table: "ProductVolumes");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes");

            migrationBuilder.DropIndex(
                name: "IX_ProductVolumes_VolumeID",
                table: "ProductVolumes");

            migrationBuilder.DropColumn(
                name: "VolumeID",
                table: "ProductVolumes");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductVolumes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProductVolumes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<float>(
                name: "VolumeValue",
                table: "ProductVolumes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVolumes_ProductID",
                table: "ProductVolumes",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVolumes_Products_ProductID",
                table: "ProductVolumes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVolumes_Products_ProductID",
                table: "ProductVolumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes");

            migrationBuilder.DropIndex(
                name: "IX_ProductVolumes_ProductID",
                table: "ProductVolumes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProductVolumes");

            migrationBuilder.DropColumn(
                name: "VolumeValue",
                table: "ProductVolumes");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductVolumes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeID",
                table: "ProductVolumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes",
                columns: new[] { "ProductID", "VolumeID" });

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolumeValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVolumes_VolumeID",
                table: "ProductVolumes",
                column: "VolumeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVolumes_Products_ProductID",
                table: "ProductVolumes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVolumes_Volumes_VolumeID",
                table: "ProductVolumes",
                column: "VolumeID",
                principalTable: "Volumes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
