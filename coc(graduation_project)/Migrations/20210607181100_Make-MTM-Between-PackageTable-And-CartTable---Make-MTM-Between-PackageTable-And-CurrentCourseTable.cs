using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class MakeMTMBetweenPackageTableAndCartTableMakeMTMBetweenPackageTableAndCurrentCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Courses_CourseId",
                table: "CourseCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_Courses_CourseID",
                table: "CurrentCourseCourses");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "CurrentCourseCourses",
                newName: "PackageID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourseCourses_CourseID",
                table: "CurrentCourseCourses",
                newName: "IX_CurrentCourseCourses_PackageID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseCarts",
                newName: "PackageID");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCarts_CourseId",
                table: "CourseCarts",
                newName: "IX_CourseCarts_PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Package_PackageID",
                table: "CourseCarts",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_Package_PackageID",
                table: "CurrentCourseCourses",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCarts_Package_PackageID",
                table: "CourseCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_Package_PackageID",
                table: "CurrentCourseCourses");

            migrationBuilder.RenameColumn(
                name: "PackageID",
                table: "CurrentCourseCourses",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourseCourses_PackageID",
                table: "CurrentCourseCourses",
                newName: "IX_CurrentCourseCourses_CourseID");

            migrationBuilder.RenameColumn(
                name: "PackageID",
                table: "CourseCarts",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCarts_PackageID",
                table: "CourseCarts",
                newName: "IX_CourseCarts_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCarts_Courses_CourseId",
                table: "CourseCarts",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_Courses_CourseID",
                table: "CurrentCourseCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
