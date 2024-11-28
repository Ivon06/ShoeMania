using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeMania.Data.Migrations
{
    /// <inheritdoc />
    public partial class adminUseradded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3", 0, "ul. Stefan Stambolov 20", "Kazanlak", "1ee313c2-2e26-4ec5-bebc-615e68a1246e", "Bulgaria", "ivonmircheva2@gmail.com", false, "Ivon", true, "Mircheva", false, null, "IVONMIRCHEVA2@GMAIL.COM", "IVON", "AQAAAAIAAYagAAAAELDcnC95l01Ve2Va3GOw9tjqWolBmcwfwVaNNwRRhAfvd8QLiqAwsyzMt4lJJsA/Tg==", null, false, "https://res.cloudinary.com/dwocfg6qw/image/upload/v1703607775/FootTrapProject/2150771123_oytfrj.jpg", "c09f8686-d762-47f5-8b8d-3ea8acef8751", false, "Ivon" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "835c8458-e8b7-493f-9c13-67bfcd7316a3", "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "835c8458-e8b7-493f-9c13-67bfcd7316a3", "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3");
        }
    }
}
