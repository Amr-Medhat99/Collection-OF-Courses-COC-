using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class someedit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FreeVideo_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeVideo",
                table: "FreeVideo");

            migrationBuilder.RenameTable(
                name: "FreeVideo",
                newName: "FreeVideos");

            migrationBuilder.AddColumn<int>(
                name: "PackageID",
                table: "FreeVideos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeVideos",
                table: "FreeVideos",
                column: "FreeVideoID");

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageID = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_FreeVideos_PackageID",
                table: "FreeVideos",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Components_PackageID",
                table: "Components",
                column: "PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FreeVideos_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID",
                principalTable: "FreeVideos",
                principalColumn: "FreeVideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVideos_Package_PackageID",
                table: "FreeVideos",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FreeVideos_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FreeVideos_Package_PackageID",
                table: "FreeVideos");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeVideos",
                table: "FreeVideos");

            migrationBuilder.DropIndex(
                name: "IX_FreeVideos_PackageID",
                table: "FreeVideos");

            migrationBuilder.DropColumn(
                name: "PackageID",
                table: "FreeVideos");

            migrationBuilder.RenameTable(
                name: "FreeVideos",
                newName: "FreeVideo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeVideo",
                table: "FreeVideo",
                column: "FreeVideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FreeVideo_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID",
                principalTable: "FreeVideo",
                principalColumn: "FreeVideoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
