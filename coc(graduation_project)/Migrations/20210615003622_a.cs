using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_CurrentCourses_CurrentCourseID",
                table: "CurrentCourseCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_Package_PackageID",
                table: "CurrentCourseCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_Favorites_FavoriteID",
                table: "FavoriteCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_SubCategorys_SubCategoryID",
                table: "FavoriteCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteCourses",
                table: "FavoriteCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCourseCourses",
                table: "CurrentCourseCourses");

            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "Package");

            migrationBuilder.RenameTable(
                name: "FavoriteCourses",
                newName: "FavoriteSubCategory");

            migrationBuilder.RenameTable(
                name: "CurrentCourseCourses",
                newName: "CurrentCoursePackage");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCourses_SubCategoryID",
                table: "FavoriteSubCategory",
                newName: "IX_FavoriteSubCategory_SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourseCourses_PackageID",
                table: "CurrentCoursePackage",
                newName: "IX_CurrentCoursePackage_PackageID");

            migrationBuilder.AddColumn<int>(
                name: "PackageNumber",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteSubCategory",
                table: "FavoriteSubCategory",
                columns: new[] { "FavoriteID", "SubCategoryID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCoursePackage",
                table: "CurrentCoursePackage",
                columns: new[] { "CurrentCourseID", "PackageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCoursePackage_CurrentCourses_CurrentCourseID",
                table: "CurrentCoursePackage",
                column: "CurrentCourseID",
                principalTable: "CurrentCourses",
                principalColumn: "CurrentCourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCoursePackage_Package_PackageID",
                table: "CurrentCoursePackage",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSubCategory_Favorites_FavoriteID",
                table: "FavoriteSubCategory",
                column: "FavoriteID",
                principalTable: "Favorites",
                principalColumn: "FavoriteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSubCategory_SubCategorys_SubCategoryID",
                table: "FavoriteSubCategory",
                column: "SubCategoryID",
                principalTable: "SubCategorys",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCoursePackage_CurrentCourses_CurrentCourseID",
                table: "CurrentCoursePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCoursePackage_Package_PackageID",
                table: "CurrentCoursePackage");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSubCategory_Favorites_FavoriteID",
                table: "FavoriteSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSubCategory_SubCategorys_SubCategoryID",
                table: "FavoriteSubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteSubCategory",
                table: "FavoriteSubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCoursePackage",
                table: "CurrentCoursePackage");

            migrationBuilder.DropColumn(
                name: "PackageNumber",
                table: "Package");

            migrationBuilder.RenameTable(
                name: "FavoriteSubCategory",
                newName: "FavoriteCourses");

            migrationBuilder.RenameTable(
                name: "CurrentCoursePackage",
                newName: "CurrentCourseCourses");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteSubCategory_SubCategoryID",
                table: "FavoriteCourses",
                newName: "IX_FavoriteCourses_SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCoursePackage_PackageID",
                table: "CurrentCourseCourses",
                newName: "IX_CurrentCourseCourses_PackageID");

            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteCourses",
                table: "FavoriteCourses",
                columns: new[] { "FavoriteID", "SubCategoryID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCourseCourses",
                table: "CurrentCourseCourses",
                columns: new[] { "CurrentCourseID", "PackageID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_CurrentCourses_CurrentCourseID",
                table: "CurrentCourseCourses",
                column: "CurrentCourseID",
                principalTable: "CurrentCourses",
                principalColumn: "CurrentCourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_Package_PackageID",
                table: "CurrentCourseCourses",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_Favorites_FavoriteID",
                table: "FavoriteCourses",
                column: "FavoriteID",
                principalTable: "Favorites",
                principalColumn: "FavoriteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_SubCategorys_SubCategoryID",
                table: "FavoriteCourses",
                column: "SubCategoryID",
                principalTable: "SubCategorys",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
