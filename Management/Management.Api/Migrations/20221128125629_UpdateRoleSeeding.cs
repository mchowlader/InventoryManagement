using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Api.Migrations
{
    public partial class UpdateRoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5896697b-ecb7-43c8-848b-8542b0e7756f"), "7dc34b04-500a-4e5e-91a2-64e4b916d1a5", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ab907b6c-bc8a-4411-82a4-1f07b23302ef"), "b0f6ab45-c547-4804-90a8-7d73fd6a63a7", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d1af5c6f-33a0-4a8e-b6cd-9052dad89ff6"), "4f7180d7-e489-4055-8d78-41d34ade0e4e", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5896697b-ecb7-43c8-848b-8542b0e7756f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ab907b6c-bc8a-4411-82a4-1f07b23302ef"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d1af5c6f-33a0-4a8e-b6cd-9052dad89ff6"));

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
    }
}
