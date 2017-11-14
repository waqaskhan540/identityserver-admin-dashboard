using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IdentityServer.Migrations
{
    public partial class config_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<string>(type: "longtext", nullable: true),
                    RequireDigit = table.Column<bool>(type: "bit", nullable: false),
                    RequireLowercase = table.Column<bool>(type: "bit", nullable: false),
                    RequireNonAlphanumeric = table.Column<bool>(type: "bit", nullable: false),
                    RequireUppercase = table.Column<bool>(type: "bit", nullable: false),
                    RequiredLength = table.Column<int>(type: "int", nullable: false),
                    RequiredUniqueChars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    UserInfoEndPoint = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordConfigs");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
