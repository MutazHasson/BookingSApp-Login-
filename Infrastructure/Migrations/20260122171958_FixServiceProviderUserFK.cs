using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixServiceProviderUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Users_UserId1",
                table: "ServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_UserId1",
                table: "ServiceProviders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ServiceProviders");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ServiceProviders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_UserId",
                table: "ServiceProviders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Users_UserId",
                table: "ServiceProviders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProviders_Users_UserId",
                table: "ServiceProviders");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProviders_UserId",
                table: "ServiceProviders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ServiceProviders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "ServiceProviders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_UserId1",
                table: "ServiceProviders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProviders_Users_UserId1",
                table: "ServiceProviders",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
