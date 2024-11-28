using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeMania.Data.Migrations
{
    /// <inheritdoc />
    public partial class adminUseradded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2017511c-fc0d-4f2f-8ad4-47e8166af9b6", "IVON06", "AQAAAAIAAYagAAAAEJywCUN5BsyWtFqCrzY0EzKjGN7Vdb1GVatKTfkOYLGsYcRTlghIBMkR+61eDC7KUA==", "5bd4f05e-f997-460b-88ce-a65a5bb8560c", "Ivon06" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "1ee313c2-2e26-4ec5-bebc-615e68a1246e", "IVON", "AQAAAAIAAYagAAAAELDcnC95l01Ve2Va3GOw9tjqWolBmcwfwVaNNwRRhAfvd8QLiqAwsyzMt4lJJsA/Tg==", "c09f8686-d762-47f5-8b8d-3ea8acef8751", "Ivon" });
        }
    }
}
