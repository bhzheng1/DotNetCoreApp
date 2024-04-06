using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi_Application.Migrations
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "657ae99d-1bed-4424-bbbf-0a5dcebc9480", "2", "User", "USER" },
                    { "73a39466-94ec-46d6-91e2-6e0308790438", "3", "Guest", "GUEST" },
                    { "c850a52e-df1c-41b3-b330-59398863329b", "4", "Vendor", "VENDOR" },
                    { "ebb178be-c29e-46cc-b88c-63ad51ef6535", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Address 1", "Company 1" },
                    { 2, "Address 2", "Company 2" },
                    { 3, "Address 3", "Company 3" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Product 1", 100m },
                    { 2, "Product 2", 200m },
                    { 3, "Product 3", 300m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "657ae99d-1bed-4424-bbbf-0a5dcebc9480");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a39466-94ec-46d6-91e2-6e0308790438");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c850a52e-df1c-41b3-b330-59398863329b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebb178be-c29e-46cc-b88c-63ad51ef6535");

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
    }
}
