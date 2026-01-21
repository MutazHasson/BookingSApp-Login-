using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name" },
                values: new object[] { 4, "Landscaping" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Code", "Name" },
                values: new object[] { 5, "Painting" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Code", "Name" },
                values: new object[] { 6, "Carpentry" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Code", "Name" },
                values: new object[] { 9, "PestControl" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Code", "Name" },
                values: new object[] { 10, "Moving Services" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name" },
                values: new object[] { 6, "Carpentry" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Code", "Name" },
                values: new object[] { 4, "Landscaping" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Code", "Name" },
                values: new object[] { 10, "Moving Services" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Code", "Name" },
                values: new object[] { 5, "Painting" });

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Code", "Name" },
                values: new object[] { 9, "PestControl" });
        }
    }
}
