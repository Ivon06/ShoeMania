using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeMania.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfficeClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "DeliveryOffices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4935f16-342a-4e84-bd99-1a13401476e3", "AQAAAAIAAYagAAAAEIftyruIZXopFr7cyiBOE/W3s6s7TBHSt0EN+SccpYXjr4WIcCNU6V+o6jiV3tcMpQ==", "7728d25f-13e4-4f20-a310-dd8329c47dba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e44d012-499b-4e24-a7c6-4a527659243e", "AQAAAAIAAYagAAAAEHYkAXXmt8lulmmGeIWHf/4ZZ0AJEPvmCAJTPUbEUyprlHwknc/Y2kMmfFtl+Op88g==", "417ad52a-ee5e-4291-b465-bc8a58a9df23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "DeliveryOffices",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03ef4003-e7c0-3a14-9f7a-3c7e7as25gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25291b9a-74c5-4356-adfd-315017c610ec", "AQAAAAIAAYagAAAAEBs1t/6AT2smuyYsMs1frIK5p9NmptyQLtPQuKkC/K6OxEu/F3bT375ROjbTFQx02g==", "6c780819-0912-43d6-b098-fb98c6ab8c36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hdef4003-e7cp-3e14-wk7a-3ci37aso5gd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "395fe36f-96c8-4c77-8e9b-25c9c7dcd404", "AQAAAAIAAYagAAAAELL/H4uZu3+NiKBf+RKGEK8ER5ULkPy0CZb+NEHNupK4hiTqgIITt1V5cRzJ44cqoA==", "36643601-9ef1-4f5a-8b4f-e2340eab4302" });
        }
    }
}
