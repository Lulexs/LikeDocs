using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiffWrapper_Edit_EditId",
                table: "DiffWrapper");

            migrationBuilder.DropForeignKey(
                name: "FK_Edit_UserContexts_UserContextId",
                table: "Edit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Edit",
                table: "Edit");

            migrationBuilder.DropIndex(
                name: "IX_Edit_UserContextId",
                table: "Edit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiffWrapper",
                table: "DiffWrapper");

            migrationBuilder.DropColumn(
                name: "UserContextId",
                table: "Edit");

            migrationBuilder.RenameTable(
                name: "Edit",
                newName: "Edits");

            migrationBuilder.RenameTable(
                name: "DiffWrapper",
                newName: "DiffWrapers");

            migrationBuilder.RenameIndex(
                name: "IX_DiffWrapper_EditId",
                table: "DiffWrapers",
                newName: "IX_DiffWrapers_EditId");

            migrationBuilder.AddColumn<int>(
                name: "EditId",
                table: "UserContexts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edits",
                table: "Edits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiffWrapers",
                table: "DiffWrapers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserContexts_EditId",
                table: "UserContexts",
                column: "EditId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiffWrapers_Edits_EditId",
                table: "DiffWrapers",
                column: "EditId",
                principalTable: "Edits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContexts_Edits_EditId",
                table: "UserContexts",
                column: "EditId",
                principalTable: "Edits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiffWrapers_Edits_EditId",
                table: "DiffWrapers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContexts_Edits_EditId",
                table: "UserContexts");

            migrationBuilder.DropIndex(
                name: "IX_UserContexts_EditId",
                table: "UserContexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Edits",
                table: "Edits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiffWrapers",
                table: "DiffWrapers");

            migrationBuilder.DropColumn(
                name: "EditId",
                table: "UserContexts");

            migrationBuilder.RenameTable(
                name: "Edits",
                newName: "Edit");

            migrationBuilder.RenameTable(
                name: "DiffWrapers",
                newName: "DiffWrapper");

            migrationBuilder.RenameIndex(
                name: "IX_DiffWrapers_EditId",
                table: "DiffWrapper",
                newName: "IX_DiffWrapper_EditId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserContextId",
                table: "Edit",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Edit",
                table: "Edit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiffWrapper",
                table: "DiffWrapper",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Edit_UserContextId",
                table: "Edit",
                column: "UserContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiffWrapper_Edit_EditId",
                table: "DiffWrapper",
                column: "EditId",
                principalTable: "Edit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Edit_UserContexts_UserContextId",
                table: "Edit",
                column: "UserContextId",
                principalTable: "UserContexts",
                principalColumn: "Id");
        }
    }
}
