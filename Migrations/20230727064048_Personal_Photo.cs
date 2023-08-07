using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace com.portfolio.website.Migrations
{
    public partial class Personal_Photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "PersonalInformation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "PersonalInformation");
        }
    }
}
