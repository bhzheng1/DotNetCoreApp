using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi_Application.Migrations
{
    /// <inheritdoc />
    public partial class seedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2552bd66-c0fa-40a1-b75c-b2f9c5ba2240", "1", "Admin", "ADMIN" },
                    { "7c3244b8-e648-4127-a0ad-8f58dd18b5ff", "2", "User", "USER" },
                    { "cd5fa632-6a5e-4878-bd32-784a74402538", "4", "Vendor", "VENDOR" },
                    { "d76713ad-bb49-4f82-a3f5-b6e2fe741003", "3", "Guest", "GUEST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2552bd66-c0fa-40a1-b75c-b2f9c5ba2240");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c3244b8-e648-4127-a0ad-8f58dd18b5ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd5fa632-6a5e-4878-bd32-784a74402538");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d76713ad-bb49-4f82-a3f5-b6e2fe741003");
        }
    }
}
