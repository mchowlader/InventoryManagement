using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Api.Migrations
{
    public partial class UpdateRoleSedding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { new Guid("3dc0fca1-8c39-4cee-ab74-759e356ac673"), "ae872eb9-0e05-4f45-9a43-729354646a0e", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("a9862c83-1b69-475c-aa8e-26bb04b0a2c5"), "833aadb2-be63-48fa-b294-ac52ea0d043b", "CompanyAdmin", "COMPANYADMIN" },
                    { new Guid("b1a5de59-5f0a-4883-b808-accc14f168dc"), "ace462bc-2e19-4b61-8966-82dec15e36c5", "User", "USER" },
                    { new Guid("e8861941-02d3-4605-be43-7bb295104048"), "dc625ea7-b129-4581-b228-5748ac2c4651", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3dc0fca1-8c39-4cee-ab74-759e356ac673"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9862c83-1b69-475c-aa8e-26bb04b0a2c5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b1a5de59-5f0a-4883-b808-accc14f168dc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e8861941-02d3-4605-be43-7bb295104048"));

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
    }
}
