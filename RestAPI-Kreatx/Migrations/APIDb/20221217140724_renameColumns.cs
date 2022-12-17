using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAPI_Kreatx.Migrations.APIDb
{
    public partial class renameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_APIUser_UserIdId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_APIUser_UserIdId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectIdId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProjectIdId",
                table: "Tasks",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserIdId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectIdId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserIdId",
                table: "Projects",
                newName: "IX_Projects_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_APIUser_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "APIUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_APIUser_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "APIUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_APIUser_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_APIUser_UserId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tasks",
                newName: "UserIdId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Tasks",
                newName: "ProjectIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                newName: "IX_Tasks_UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectIdId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                newName: "IX_Projects_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_APIUser_UserIdId",
                table: "Projects",
                column: "UserIdId",
                principalTable: "APIUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_APIUser_UserIdId",
                table: "Tasks",
                column: "UserIdId",
                principalTable: "APIUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectIdId",
                table: "Tasks",
                column: "ProjectIdId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
