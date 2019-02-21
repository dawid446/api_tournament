using Microsoft.EntityFrameworkCore.Migrations;

namespace tournamentapi.Migrations
{
    public partial class edit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeamName1",
                table: "Match",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TeamName1",
                table: "Match",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
