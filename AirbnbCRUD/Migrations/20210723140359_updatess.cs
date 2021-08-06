using Microsoft.EntityFrameworkCore.Migrations;

namespace AirbnbCRUD.Migrations
{
    public partial class updatess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonBirthday",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PersonGender",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonBirthday",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonGender",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
