using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedUserRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "FinanceWebApp",
                table: "Movements",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 8));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "FinanceWebApp",
                table: "Movements",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 8),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 9));
        }
    }
}
