using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class RemoveComponentTableAndMakeMTMRelationBetweenPackageAndMediaoneTOmany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Courses_CourseId",
                table: "Medias");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Medias",
                newName: "PackageID");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_CourseId",
                table: "Medias",
                newName: "IX_Medias_PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Package_PackageID",
                table: "Medias",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Package_PackageID",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "PackageID",
                table: "Medias",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Medias_PackageID",
                table: "Medias",
                newName: "IX_Medias_CourseId");

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfVideos = table.Column<int>(type: "int", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    Prise = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentID);
                    table.ForeignKey(
                        name: "FK_Components_Package_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Package",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_PackageID",
                table: "Components",
                column: "PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Courses_CourseId",
                table: "Medias",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
