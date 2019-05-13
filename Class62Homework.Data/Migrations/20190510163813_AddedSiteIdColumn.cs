using Microsoft.EntityFrameworkCore.Migrations;

namespace Class62Homework.Data.Migrations
{
    public partial class AddedSiteIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebsiteId",
                table: "Jokes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebsiteId",
                table: "Jokes");
        }
    }
}
