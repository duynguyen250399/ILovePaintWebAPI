using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ModifyWeightToVolumeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductWeights");

            migrationBuilder.DropTable(
                name: "Weight");

            migrationBuilder.CreateTable(
                name: "Volumes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolumeValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volumes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductVolumes",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    WeightID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVolumes", x => new { x.ProductID, x.WeightID });
                    table.ForeignKey(
                        name: "FK_ProductVolumes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVolumes_Volumes_WeightID",
                        column: x => x.WeightID,
                        principalTable: "Volumes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVolumes_WeightID",
                table: "ProductVolumes",
                column: "WeightID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVolumes");

            migrationBuilder.DropTable(
                name: "Volumes");

            migrationBuilder.CreateTable(
                name: "Weight",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weight", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductWeights",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    WeightID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWeights", x => new { x.ProductID, x.WeightID });
                    table.ForeignKey(
                        name: "FK_ProductWeights_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWeights_Weight_WeightID",
                        column: x => x.WeightID,
                        principalTable: "Weight",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWeights_WeightID",
                table: "ProductWeights",
                column: "WeightID");
        }
    }
}
