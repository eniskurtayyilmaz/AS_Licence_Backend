using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AS_Licence.WebUI.CoreAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerComputerInfos",
                columns: table => new
                {
                    CustomerComputerInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubscriptionId = table.Column<int>(nullable: false),
                    CustomerComputerInfoHddSerialCode = table.Column<string>(nullable: true),
                    CustomerComputerInfoMacSerialCode = table.Column<string>(nullable: true),
                    CustomerComputerInfoProcessSerialCode = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComputerInfos", x => x.CustomerComputerInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerEMail = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    CustomerIsActive = table.Column<bool>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SoftwareName = table.Column<string>(nullable: true),
                    SoftwareLastVersion = table.Column<string>(nullable: true),
                    SoftwareIsActive = table.Column<bool>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.SoftwareId);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SoftwareId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    SubscriptionIsActive = table.Column<bool>(nullable: false),
                    SubScriptionStartDate = table.Column<DateTime>(nullable: false),
                    SubScriptionEndDate = table.Column<DateTime>(nullable: false),
                    SubScriptionLicenceCount = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerComputerInfos");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
