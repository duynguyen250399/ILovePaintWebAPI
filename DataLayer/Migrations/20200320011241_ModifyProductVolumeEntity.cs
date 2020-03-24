using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ModifyProductVolumeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVolumes_Volumes_WeightID",
                table: "ProductVolumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes");

            migrationBuilder.DropIndex(
                name: "IX_ProductVolumes_WeightID",
                table: "ProductVolumes");

            migrationBuilder.DropColumn(
                name: "WeightID",
                table: "ProductVolumes");

            migrationBuilder.AddColumn<int>(
                name: "VolumeID",
                table: "ProductVolumes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes",
                columns: new[] { "ProductID", "VolumeID" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVolumes_VolumeID",
                table: "ProductVolumes",
                column: "VolumeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVolumes_Volumes_VolumeID",
                table: "ProductVolumes",
                column: "VolumeID",
                principalTable: "Volumes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVolumes_Volumes_VolumeID",
                table: "ProductVolumes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes");

            migrationBuilder.DropIndex(
                name: "IX_ProductVolumes_VolumeID",
                table: "ProductVolumes");

            migrationBuilder.DropColumn(
                name: "VolumeID",
                table: "ProductVolumes");

            migrationBuilder.AddColumn<int>(
                name: "WeightID",
                table: "ProductVolumes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVolumes",
                table: "ProductVolumes",
                columns: new[] { "ProductID", "WeightID" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVolumes_WeightID",
                table: "ProductVolumes",
                column: "WeightID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVolumes_Volumes_WeightID",
                table: "ProductVolumes",
                column: "WeightID",
                principalTable: "Volumes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
