using Microsoft.EntityFrameworkCore.Migrations;

namespace IO.Swagger.Migrations
{
    public partial class ParentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "Entry",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "Entry");
        }
    }
}
