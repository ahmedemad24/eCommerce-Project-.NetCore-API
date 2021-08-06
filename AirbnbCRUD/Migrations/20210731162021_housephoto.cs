using Microsoft.EntityFrameworkCore.Migrations;

namespace AirbnbCRUD.Migrations
{
    public partial class housephoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HousePhotoName",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HousePhotoName",
                table: "Houses");
        }
    }
}
