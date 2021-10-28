using Microsoft.EntityFrameworkCore.Migrations;

namespace coc_graduation_project_.Migrations
{
    public partial class someedit4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FreeVideos_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_FreeVideoID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FreeVideoID",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ComponentID",
                table: "FreeVideos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FreeVideos_ComponentID",
                table: "FreeVideos",
                column: "ComponentID");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVideos_Components_ComponentID",
                table: "FreeVideos",
                column: "ComponentID",
                principalTable: "Components",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeVideos_Components_ComponentID",
                table: "FreeVideos");

            migrationBuilder.DropIndex(
                name: "IX_FreeVideos_ComponentID",
                table: "FreeVideos");

            migrationBuilder.DropColumn(
                name: "ComponentID",
                table: "FreeVideos");

            migrationBuilder.AddColumn<int>(
                name: "FreeVideoID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FreeVideos_FreeVideoID",
                table: "Comments",
                column: "FreeVideoID",
                principalTable: "FreeVideos",
                principalColumn: "FreeVideoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
