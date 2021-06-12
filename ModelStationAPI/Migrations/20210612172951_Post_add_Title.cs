using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelStationAPI.Migrations
{
    public partial class Post_add_Title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastEditDAte",
                table: "Comments",
                newName: "LastEditDate");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "LastEditDate",
                table: "Comments",
                newName: "LastEditDAte");
        }
    }
}
