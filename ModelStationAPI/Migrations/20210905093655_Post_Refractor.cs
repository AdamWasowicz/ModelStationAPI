using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelStationAPI.Migrations
{
    public partial class Post_Refractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostHash",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostHash",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
