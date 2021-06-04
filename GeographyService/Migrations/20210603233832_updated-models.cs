using Microsoft.EntityFrameworkCore.Migrations;

namespace GeographyService.Migrations
{
    public partial class updatedmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rivers",
                newName: "RiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RiverId",
                table: "Rivers",
                newName: "Id");
        }
    }
}
