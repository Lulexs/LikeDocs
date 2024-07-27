using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VAddEditsToUserContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    n = table.Column<int>(type: "INTEGER", nullable: false),
                    m = table.Column<int>(type: "INTEGER", nullable: false),
                    UserContextId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edit_UserContexts_UserContextId",
                        column: x => x.UserContextId,
                        principalTable: "UserContexts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiffWrapper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    operation = table.Column<int>(type: "INTEGER", nullable: false),
                    text = table.Column<string>(type: "TEXT", nullable: false),
                    EditId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiffWrapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiffWrapper_Edit_EditId",
                        column: x => x.EditId,
                        principalTable: "Edit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiffWrapper_EditId",
                table: "DiffWrapper",
                column: "EditId");

            migrationBuilder.CreateIndex(
                name: "IX_Edit_UserContextId",
                table: "Edit",
                column: "UserContextId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiffWrapper");

            migrationBuilder.DropTable(
                name: "Edit");
        }
    }
}
