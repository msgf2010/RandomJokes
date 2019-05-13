using Microsoft.EntityFrameworkCore.Migrations;

namespace Class62Homework.Data.Migrations
{
    public partial class AddedLikesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Jokes");

            migrationBuilder.RenameColumn(
                name: "SetUp",
                table: "Jokes",
                newName: "Setup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Setup",
                table: "Jokes",
                newName: "SetUp");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Jokes",
                nullable: true);
        }
    }
}
