using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IdentityServer.Migrations
{
    public partial class client_id_field_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUsers");
        }
    }
}
