using Microsoft.EntityFrameworkCore.Migrations;

namespace Class62Homework.Data.Migrations
{
    public partial class AddedTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Jokes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Jokes");
        }
    }
}
