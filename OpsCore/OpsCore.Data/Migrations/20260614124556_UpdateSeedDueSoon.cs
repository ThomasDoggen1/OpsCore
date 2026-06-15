using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDueSoon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LastMaintenanceDate", "MaintenanceIntervalDays" },
                values: new object[] { new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LastMaintenanceDate", "MaintenanceIntervalDays" },
                values: new object[] { new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 120 });
        }
    }
}
