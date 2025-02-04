using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBModels.Migrations
{
    /// <inheritdoc />
    public partial class modelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Currencies",
                newName: "Symbol");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Currencies",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Currencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe", "password123" },
                    { 2, "jane.smith@example.com", "Jane Smith", "password456" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CurrencyId", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, 1, "mike.johnson@example.com", "Mike Johnson", "password789" },
                    { 2, 2, "emily.davis@example.com", "Emily Davis", "password101" }
                });
        }
    }
}
