using Microsoft.EntityFrameworkCore.Migrations;

namespace AS_Licence.WebUI.CoreAPI.Migrations
{
    public partial class SoftwareDownloadUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoftwareDownloadUrl",
                table: "Softwares",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftwareDownloadUrl",
                table: "Softwares");
        }
    }
}
