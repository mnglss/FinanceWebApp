using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesTable : Migration
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
                defaultValue: new DateOnly(2025, 5, 21),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 9));

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "FinanceWebApp",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ColorRGBA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "FinanceWebApp");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                schema: "FinanceWebApp",
                table: "Movements",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 9),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 21));
        }
    }
}
