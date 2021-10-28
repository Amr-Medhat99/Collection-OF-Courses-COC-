using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class renameCoursePackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Carts_CartId",
                table: "CourseCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Package_PackageID",
                table: "CourseCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCarts",
                table: "CourseCarts");

            migrationBuilder.RenameTable(
                name: "CourseCarts",
                newName: "CoursePackage");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCarts_PackageID",
                table: "CoursePackage",
                newName: "IX_CoursePackage_PackageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePackage",
                table: "CoursePackage",
                columns: new[] { "CartId", "PackageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePackage_Carts_CartId",
                table: "CoursePackage",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePackage_Package_PackageID",
                table: "CoursePackage",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePackage_Carts_CartId",
                table: "CoursePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePackage_Package_PackageID",
                table: "CoursePackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePackage",
                table: "CoursePackage");

            migrationBuilder.RenameTable(
                name: "CoursePackage",
                newName: "CourseCarts");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePackage_PackageID",
                table: "CourseCarts",
                newName: "IX_CourseCarts_PackageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCarts",
                table: "CourseCarts",
                columns: new[] { "CartId", "PackageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Carts_CartId",
                table: "CourseCarts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Package_PackageID",
                table: "CourseCarts",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
