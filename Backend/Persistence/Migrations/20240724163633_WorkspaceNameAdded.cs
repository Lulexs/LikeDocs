using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class WorkspaceNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_AppUserId",
                table: "Workspaces");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Workspaces",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Workspaces_AppUserId",
                table: "Workspaces",
                newName: "IX_Workspaces_OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workspaces",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_OwnerId",
                table: "Workspaces",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_OwnerId",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workspaces");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Workspaces",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workspaces_OwnerId",
                table: "Workspaces",
                newName: "IX_Workspaces_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_AppUserId",
                table: "Workspaces",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
