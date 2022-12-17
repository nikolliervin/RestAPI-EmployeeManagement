using Microsoft.EntityFrameworkCore.Migrations;

namespace RestAPI_Kreatx.Migrations.APIDb
{
    public partial class hasOpenTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOpenTasks",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOpenTasks",
                table: "Projects");
        }
    }
}
