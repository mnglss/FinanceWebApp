using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                schema: "FinanceWebApp",
                table: "Users");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "FinanceWebApp",
                table: "Movements",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 8),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 7));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                schema: "FinanceWebApp",
                table: "Users",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "FinanceWebApp",
                table: "Movements",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 7),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 8));
        }
    }
}
