using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoeMania.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25291b9a-74c5-4356-adfd-315017c610ec", "AQAAAAIAAYagAAAAEBs1t/6AT2smuyYsMs1frIK5p9NmptyQLtPQuKkC/K6OxEu/F3bT375ROjbTFQx02g==", "6c780819-0912-43d6-b098-fb98c6ab8c36" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3", 0, "ul. Kokiche 14", "Kazanlak", "395fe36f-96c8-4c77-8e9b-25c9c7dcd404", "Bulgaria", "georgiivanov@gmail.com", false, "Georgi", true, "Ivanov", false, null, "GEORGIIVANOV@GMAIL.COM", "GOSHO", "AQAAAAIAAYagAAAAELL/H4uZu3+NiKBf+RKGEK8ER5ULkPy0CZb+NEHNupK4hiTqgIITt1V5cRzJ44cqoA==", null, false, "https://res.cloudinary.com/dwocfg6qw/image/upload/v1703607775/FootTrapProject/2150771123_oytfrj.jpg", "36643601-9ef1-4f5a-8b4f-e2340eab4302", false, "Gosho" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "2e92ejgd-95de-486a-a2qd-360ihtc3066d", "Sport" },
                    { "8kd954a9-3od1-ldp1-a984-e91kshb22e23e", "Women" },
                    { "asc3dde4-7fd9-4c33-add1-ef8l3c7kdm0c", "Mens" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Number" },
                values: new object[,]
                {
                    { 1, 36 },
                    { 2, 37 },
                    { 3, 38 },
                    { 4, 39 },
                    { 5, 40 },
                    { 6, 41 },
                    { 7, 42 },
                    { 8, 43 },
                    { 9, 44 },
                    { 10, 45 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "78374b9b-5158-4aff-8626-d088a02d79e1", "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "IsActive", "UserId" },
                values: new object[] { "a3bd28c7-f6f8-4eb8-9ca3-d3539faf427e", true, "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "78374b9b-5158-4aff-8626-d088a02d79e1", "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3" });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: "2e92ejgd-95de-486a-a2qd-360ihtc3066d");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: "8kd954a9-3od1-ldp1-a984-e91kshb22e23e");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: "asc3dde4-7fd9-4c33-add1-ef8l3c7kdm0c");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: "a3bd28c7-f6f8-4eb8-9ca3-d3539faf427e");

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2017511c-fc0d-4f2f-8ad4-47e8166af9b6", "AQAAAAIAAYagAAAAEJywCUN5BsyWtFqCrzY0EzKjGN7Vdb1GVatKTfkOYLGsYcRTlghIBMkR+61eDC7KUA==", "5bd4f05e-f997-460b-88ce-a65a5bb8560c" });
        }
    }
}
