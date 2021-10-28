using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class someedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Medias_MediaID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Package_PackageID",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_QandAVideos_Medias_MediaID",
                table: "QandAVideos");

            migrationBuilder.DropIndex(
                name: "IX_Medias_PackageID",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "PackageID",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "QandAVideos",
                newName: "VideoURL");

            migrationBuilder.RenameColumn(
                name: "MediaID",
                table: "QandAVideos",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "QandAVideos",
                newName: "VideoName");

            migrationBuilder.RenameIndex(
                name: "IX_QandAVideos_MediaID",
                table: "QandAVideos",
                newName: "IX_QandAVideos_VideoID");

            migrationBuilder.RenameColumn(
                name: "PackagePrise",
                table: "Package",
                newName: "PackageCost");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Medias",
                newName: "MediaID");

            migrationBuilder.RenameColumn(
                name: "MediaTitle",
                table: "Medias",
                newName: "VideoURL");

            migrationBuilder.RenameColumn(
                name: "MediaLink",
                table: "Medias",
                newName: "VideoName");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "CourseID");

            migrationBuilder.RenameColumn(
                name: "CourseRequire",
                table: "Courses",
                newName: "Requirements");

            migrationBuilder.RenameColumn(
                name: "CourseDescription",
                table: "Courses",
                newName: "RelasedDate");

            migrationBuilder.RenameColumn(
                name: "MediaID",
                table: "Comments",
                newName: "VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MediaID",
                table: "Comments",
                newName: "IX_Comments_VideoID");

            migrationBuilder.AddColumn<string>(
                name: "MainCategoryLogo",
                table: "MainCategorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Online",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Options",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FreeVideoID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FreeVideo",
                columns: table => new
                {
                    FreeVideoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeVideoName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeVideoURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeVideo", x => x.FreeVideoID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FreeVideo_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID",
                principalTable: "FreeVideo",
                principalColumn: "FreeVideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Medias_VideoID",
                table: "Comments",
                column: "VideoID",
                principalTable: "Medias",
                principalColumn: "MediaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QandAVideos_Medias_VideoID",
                table: "QandAVideos",
                column: "VideoID",
                principalTable: "Medias",
                principalColumn: "MediaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FreeVideo_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Medias_VideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_QandAVideos_Medias_VideoID",
                table: "QandAVideos");

            migrationBuilder.DropTable(
                name: "FreeVideo");

            migrationBuilder.DropIndex(
                name: "IX_Comments_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MainCategoryLogo",
                table: "MainCategorys");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Online",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FreeVideoID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "VideoURL",
                table: "QandAVideos",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "VideoName",
                table: "QandAVideos",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "QandAVideos",
                newName: "MediaID");

            migrationBuilder.RenameIndex(
                name: "IX_QandAVideos_VideoID",
                table: "QandAVideos",
                newName: "IX_QandAVideos_MediaID");

            migrationBuilder.RenameColumn(
                name: "PackageCost",
                table: "Package",
                newName: "PackagePrise");

            migrationBuilder.RenameColumn(
                name: "MediaID",
                table: "Medias",
                newName: "MediaId");

            migrationBuilder.RenameColumn(
                name: "VideoURL",
                table: "Medias",
                newName: "MediaTitle");

            migrationBuilder.RenameColumn(
                name: "VideoName",
                table: "Medias",
                newName: "MediaLink");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "Requirements",
                table: "Courses",
                newName: "CourseRequire");

            migrationBuilder.RenameColumn(
                name: "RelasedDate",
                table: "Courses",
                newName: "CourseDescription");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Comments",
                newName: "MediaID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VideoID",
                table: "Comments",
                newName: "IX_Comments_MediaID");

            migrationBuilder.AddColumn<int>(
                name: "PackageID",
                table: "Medias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_PackageID",
                table: "Medias",
                column: "PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Medias_MediaID",
                table: "Comments",
                column: "MediaID",
                principalTable: "Medias",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Package_PackageID",
                table: "Medias",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QandAVideos_Medias_MediaID",
                table: "QandAVideos",
                column: "MediaID",
                principalTable: "Medias",
                principalColumn: "MediaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
