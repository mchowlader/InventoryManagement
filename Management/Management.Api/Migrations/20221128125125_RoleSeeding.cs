using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Api.Migrations
{
    public partial class RoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("0df1a2ab-0e35-476b-aba1-c7d8bf502613"), "11/28/2022 6:51:25 PM", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("75265502-730d-4b85-b1bf-448c5cda51dd"), "11/28/2022 6:51:25 PM", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8b1496c7-bc44-4ebe-abd0-6e92e68d344f"), "11/28/2022 6:51:25 PM", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0df1a2ab-0e35-476b-aba1-c7d8bf502613"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("75265502-730d-4b85-b1bf-448c5cda51dd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8b1496c7-bc44-4ebe-abd0-6e92e68d344f"));
        }
    }
}
