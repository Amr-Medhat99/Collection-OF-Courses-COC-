using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class makeSomeEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentBody",
                table: "Comments",
                newName: "missing_explain");

            migrationBuilder.AddColumn<string>(
                name: "QA_Following",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QA_FollowingWN",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Stars",
                table: "Courses",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "missing_answers",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "understand_rate",
                table: "Comments",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QA_Following",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "QA_FollowingWN",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "missing_answers",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "understand_rate",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "missing_explain",
                table: "Comments",
                newName: "CommentBody");
        }
    }
}
