using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoeMania.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78374b9b-5158-4aff-8626-d088a02d79e1", null, "Customer", "CUSTOMER" },
                    { "835c8458-e8b7-493f-9c13-67bfcd7316a3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78374b9b-5158-4aff-8626-d088a02d79e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835c8458-e8b7-493f-9c13-67bfcd7316a3");
        }
    }
}
