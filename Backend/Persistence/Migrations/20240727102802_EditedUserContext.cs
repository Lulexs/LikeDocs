using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditedUserContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContexts_AspNetUsers_UserContext",
                table: "UserContexts");

            migrationBuilder.DropIndex(
                name: "IX_UserContexts_UserContext",
                table: "UserContexts");

            migrationBuilder.DropColumn(
                name: "UserContext",
                table: "UserContexts");

            migrationBuilder.AddColumn<string>(
                name: "connectionId",
                table: "UserContexts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "connectionId",
                table: "UserContexts");

            migrationBuilder.AddColumn<string>(
                name: "UserContext",
                table: "UserContexts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContexts_UserContext",
                table: "UserContexts",
                column: "UserContext",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContexts_AspNetUsers_UserContext",
                table: "UserContexts",
                column: "UserContext",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
