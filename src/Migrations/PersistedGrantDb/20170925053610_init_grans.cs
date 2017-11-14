using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IdentityServer.Migrations.PersistedGrantDb
{
    public partial class init_grans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Data = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SubjectId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersistedGrants");
        }
    }
}
