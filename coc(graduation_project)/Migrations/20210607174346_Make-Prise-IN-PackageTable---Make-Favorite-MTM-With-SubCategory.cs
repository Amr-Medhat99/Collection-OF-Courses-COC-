using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class MakePriseINPackageTableMakeFavoriteMTMWithSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_Courses_CourseID",
                table: "FavoriteCourses");

            migrationBuilder.DropColumn(
                name: "CourseCost",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "FavoriteCourses",
                newName: "SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCourses_CourseID",
                table: "FavoriteCourses",
                newName: "IX_FavoriteCourses_SubCategoryID");

            migrationBuilder.AddColumn<double>(
                name: "PackagePrise",
                table: "Package",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_SubCategorys_SubCategoryID",
                table: "FavoriteCourses",
                column: "SubCategoryID",
                principalTable: "SubCategorys",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_SubCategorys_SubCategoryID",
                table: "FavoriteCourses");

            migrationBuilder.DropColumn(
                name: "PackagePrise",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "FavoriteCourses",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCourses_SubCategoryID",
                table: "FavoriteCourses",
                newName: "IX_FavoriteCourses_CourseID");

            migrationBuilder.AddColumn<double>(
                name: "CourseCost",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_Courses_CourseID",
                table: "FavoriteCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
