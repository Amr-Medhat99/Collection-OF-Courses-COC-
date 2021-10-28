using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class makerelationbetweennewsandcourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_SubCategorys_SubCategoryID",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "SubCategoryID",
                table: "News",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_News_SubCategoryID",
                table: "News",
                newName: "IX_News_CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Courses_CourseID",
                table: "News",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Courses_CourseID",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "News",
                newName: "SubCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_News_CourseID",
                table: "News",
                newName: "IX_News_SubCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_News_SubCategorys_SubCategoryID",
                table: "News",
                column: "SubCategoryID",
                principalTable: "SubCategorys",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
