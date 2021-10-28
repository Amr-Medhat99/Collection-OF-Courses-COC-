using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class RemoveGenderAndSpecific : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SubCategory_SubCategoryID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourse_Students_studentID",
                table: "CurrentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourse_Courses_CourseID",
                table: "CurrentCourseCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourse_CurrentCourse_CurrentCourseID",
                table: "CurrentCourseCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Students_studentID",
                table: "Favorite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourse_Courses_CourseID",
                table: "FavoriteCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourse_Favorite_FavoriteID",
                table: "FavoriteCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_MainCategory_MainCategoryID",
                table: "SubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterCourse_Courses_CourseID",
                table: "WatchLaterCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterCourse_watchLaters_WatchLaterID",
                table: "WatchLaterCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchLaterCourse",
                table: "WatchLaterCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainCategory",
                table: "MainCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteCourse",
                table: "FavoriteCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCourseCourse",
                table: "CurrentCourseCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCourse",
                table: "CurrentCourse");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Specific",
                table: "Instructors");

            migrationBuilder.RenameTable(
                name: "WatchLaterCourse",
                newName: "WatchLaterCourses");

            migrationBuilder.RenameTable(
                name: "SubCategory",
                newName: "SubCategorys");

            migrationBuilder.RenameTable(
                name: "MainCategory",
                newName: "MainCategorys");

            migrationBuilder.RenameTable(
                name: "FavoriteCourse",
                newName: "FavoriteCourses");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.RenameTable(
                name: "CurrentCourseCourse",
                newName: "CurrentCourseCourses");

            migrationBuilder.RenameTable(
                name: "CurrentCourse",
                newName: "CurrentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaterCourse_CourseID",
                table: "WatchLaterCourses",
                newName: "IX_WatchLaterCourses_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategory_MainCategoryID",
                table: "SubCategorys",
                newName: "IX_SubCategorys_MainCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCourse_CourseID",
                table: "FavoriteCourses",
                newName: "IX_FavoriteCourses_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_studentID",
                table: "Favorites",
                newName: "IX_Favorites_studentID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourseCourse_CourseID",
                table: "CurrentCourseCourses",
                newName: "IX_CurrentCourseCourses_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourse_studentID",
                table: "CurrentCourses",
                newName: "IX_CurrentCourses_studentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchLaterCourses",
                table: "WatchLaterCourses",
                columns: new[] { "WatchLaterID", "CourseID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategorys",
                table: "SubCategorys",
                column: "SubCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainCategorys",
                table: "MainCategorys",
                column: "MainCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteCourses",
                table: "FavoriteCourses",
                columns: new[] { "FavoriteID", "CourseID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "FavoriteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCourseCourses",
                table: "CurrentCourseCourses",
                columns: new[] { "CurrentCourseID", "CourseID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCourses",
                table: "CurrentCourses",
                column: "CurrentCourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SubCategorys_SubCategoryID",
                table: "Courses",
                column: "SubCategoryID",
                principalTable: "SubCategorys",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_Courses_CourseID",
                table: "CurrentCourseCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourses_CurrentCourses_CurrentCourseID",
                table: "CurrentCourseCourses",
                column: "CurrentCourseID",
                principalTable: "CurrentCourses",
                principalColumn: "CurrentCourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourses_Students_studentID",
                table: "CurrentCourses",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_Courses_CourseID",
                table: "FavoriteCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourses_Favorites_FavoriteID",
                table: "FavoriteCourses",
                column: "FavoriteID",
                principalTable: "Favorites",
                principalColumn: "FavoriteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Students_studentID",
                table: "Favorites",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategorys_MainCategorys_MainCategoryID",
                table: "SubCategorys",
                column: "MainCategoryID",
                principalTable: "MainCategorys",
                principalColumn: "MainCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterCourses_Courses_CourseID",
                table: "WatchLaterCourses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterCourses_watchLaters_WatchLaterID",
                table: "WatchLaterCourses",
                column: "WatchLaterID",
                principalTable: "watchLaters",
                principalColumn: "WatchLaterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SubCategorys_SubCategoryID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_Courses_CourseID",
                table: "CurrentCourseCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourseCourses_CurrentCourses_CurrentCourseID",
                table: "CurrentCourseCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCourses_Students_studentID",
                table: "CurrentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_Courses_CourseID",
                table: "FavoriteCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteCourses_Favorites_FavoriteID",
                table: "FavoriteCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Students_studentID",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategorys_MainCategorys_MainCategoryID",
                table: "SubCategorys");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterCourses_Courses_CourseID",
                table: "WatchLaterCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaterCourses_watchLaters_WatchLaterID",
                table: "WatchLaterCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchLaterCourses",
                table: "WatchLaterCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategorys",
                table: "SubCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MainCategorys",
                table: "MainCategorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteCourses",
                table: "FavoriteCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCourses",
                table: "CurrentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentCourseCourses",
                table: "CurrentCourseCourses");

            migrationBuilder.RenameTable(
                name: "WatchLaterCourses",
                newName: "WatchLaterCourse");

            migrationBuilder.RenameTable(
                name: "SubCategorys",
                newName: "SubCategory");

            migrationBuilder.RenameTable(
                name: "MainCategorys",
                newName: "MainCategory");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.RenameTable(
                name: "FavoriteCourses",
                newName: "FavoriteCourse");

            migrationBuilder.RenameTable(
                name: "CurrentCourses",
                newName: "CurrentCourse");

            migrationBuilder.RenameTable(
                name: "CurrentCourseCourses",
                newName: "CurrentCourseCourse");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaterCourses_CourseID",
                table: "WatchLaterCourse",
                newName: "IX_WatchLaterCourse_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategorys_MainCategoryID",
                table: "SubCategory",
                newName: "IX_SubCategory_MainCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_studentID",
                table: "Favorite",
                newName: "IX_Favorite_studentID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteCourses_CourseID",
                table: "FavoriteCourse",
                newName: "IX_FavoriteCourse_CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourses_studentID",
                table: "CurrentCourse",
                newName: "IX_CurrentCourse_studentID");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentCourseCourses_CourseID",
                table: "CurrentCourseCourse",
                newName: "IX_CurrentCourseCourse_CourseID");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specific",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchLaterCourse",
                table: "WatchLaterCourse",
                columns: new[] { "WatchLaterID", "CourseID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "SubCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MainCategory",
                table: "MainCategory",
                column: "MainCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "FavoriteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteCourse",
                table: "FavoriteCourse",
                columns: new[] { "FavoriteID", "CourseID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCourse",
                table: "CurrentCourse",
                column: "CurrentCourseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentCourseCourse",
                table: "CurrentCourseCourse",
                columns: new[] { "CurrentCourseID", "CourseID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SubCategory_SubCategoryID",
                table: "Courses",
                column: "SubCategoryID",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourse_Students_studentID",
                table: "CurrentCourse",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourse_Courses_CourseID",
                table: "CurrentCourseCourse",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCourseCourse_CurrentCourse_CurrentCourseID",
                table: "CurrentCourseCourse",
                column: "CurrentCourseID",
                principalTable: "CurrentCourse",
                principalColumn: "CurrentCourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Students_studentID",
                table: "Favorite",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourse_Courses_CourseID",
                table: "FavoriteCourse",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteCourse_Favorite_FavoriteID",
                table: "FavoriteCourse",
                column: "FavoriteID",
                principalTable: "Favorite",
                principalColumn: "FavoriteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_MainCategory_MainCategoryID",
                table: "SubCategory",
                column: "MainCategoryID",
                principalTable: "MainCategory",
                principalColumn: "MainCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterCourse_Courses_CourseID",
                table: "WatchLaterCourse",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaterCourse_watchLaters_WatchLaterID",
                table: "WatchLaterCourse",
                column: "WatchLaterID",
                principalTable: "watchLaters",
                principalColumn: "WatchLaterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
