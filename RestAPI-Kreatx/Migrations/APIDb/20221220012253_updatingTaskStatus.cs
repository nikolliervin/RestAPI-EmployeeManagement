using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAPI_Kreatx.Migrations.APIDb
{
    public partial class updatingTaskStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
