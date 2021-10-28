using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class someedit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeVideos_Package_PackageID",
                table: "FreeVideos");

            migrationBuilder.DropIndex(
                name: "IX_FreeVideos_PackageID",
                table: "FreeVideos");

            migrationBuilder.DropColumn(
                name: "PackageID",
                table: "FreeVideos");

            migrationBuilder.AddColumn<int>(
                name: "ComponentID",
                table: "Medias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ComponentID",
                table: "Medias",
                column: "ComponentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Components_ComponentID",
                table: "Medias",
                column: "ComponentID",
                principalTable: "Components",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Components_ComponentID",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_ComponentID",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "ComponentID",
                table: "Medias");

            migrationBuilder.AddColumn<int>(
                name: "PackageID",
                table: "FreeVideos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FreeVideos_PackageID",
                table: "FreeVideos",
                column: "PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVideos_Package_PackageID",
                table: "FreeVideos",
                column: "PackageID",
                principalTable: "Package",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
