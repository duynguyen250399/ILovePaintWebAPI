using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddProductWeightEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeight_Products_ProductID",
                table: "ProductWeight");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeight_Weight_WeightID",
                table: "ProductWeight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWeight",
                table: "ProductWeight");

            migrationBuilder.RenameTable(
                name: "ProductWeight",
                newName: "ProductWeights");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWeight_WeightID",
                table: "ProductWeights",
                newName: "IX_ProductWeights_WeightID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWeights",
                table: "ProductWeights",
                columns: new[] { "ProductID", "WeightID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeights_Products_ProductID",
                table: "ProductWeights",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeights_Weight_WeightID",
                table: "ProductWeights",
                column: "WeightID",
                principalTable: "Weight",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeights_Products_ProductID",
                table: "ProductWeights");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWeights_Weight_WeightID",
                table: "ProductWeights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWeights",
                table: "ProductWeights");

            migrationBuilder.RenameTable(
                name: "ProductWeights",
                newName: "ProductWeight");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWeights_WeightID",
                table: "ProductWeight",
                newName: "IX_ProductWeight_WeightID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWeight",
                table: "ProductWeight",
                columns: new[] { "ProductID", "WeightID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeight_Products_ProductID",
                table: "ProductWeight",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWeight_Weight_WeightID",
                table: "ProductWeight",
                column: "WeightID",
                principalTable: "Weight",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
