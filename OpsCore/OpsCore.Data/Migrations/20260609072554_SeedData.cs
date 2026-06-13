using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpsCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AssetTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Laptop" },
                    { 2, "Server" },
                    { 3, "Router" },
                    { 4, "Switch" },
                    { 5, "Printer" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "HR" },
                    { 3, "Finance" },
                    { 4, "Operations" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetTypeId", "EmployeeId", "LastMaintenanceDate", "Location", "MaintenanceIntervalDays", "Name", "Status" },
                values: new object[,]
                {
                    { 3, 2, null, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Server Room", 60, "Dell PowerEdge R740", 1 },
                    { 4, 4, null, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Server Room", 120, "Cisco Switch 48 Port", 0 },
                    { 5, 5, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office A-101", 90, "HP LaserJet Pro", 0 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Email", "FirstName", "IsOnDuty", "IsPresent", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "john@opscore.com", "John", true, true, "Smith", "", "0123456789" },
                    { 2, 2, "sarah@opscore.com", "Sarah", true, false, "Chen", "", "0123456788" },
                    { 3, 3, "mike@opscore.com", "Mike", false, false, "Johnson", "", "0123456787" }
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "Action", "AssetId", "CreatedAt", "EmployeeId" },
                values: new object[] { 3, "Status updated to Maintenance", 3, new DateTime(2026, 6, 8, 9, 25, 54, 343, DateTimeKind.Local).AddTicks(2960), 1 });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetTypeId", "EmployeeId", "LastMaintenanceDate", "Location", "MaintenanceIntervalDays", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office A-101", 90, "Dell Latitude 5520", 0 },
                    { 2, 1, 2, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office B-202", 180, "MacBook Pro 16", 0 }
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "Action", "AssetId", "CreatedAt", "EmployeeId" },
                values: new object[,]
                {
                    { 1, "Added to inventory", 1, new DateTime(2026, 6, 4, 9, 25, 54, 341, DateTimeKind.Local).AddTicks(3919), 1 },
                    { 2, "Assigned to employee", 2, new DateTime(2026, 6, 6, 9, 25, 54, 343, DateTimeKind.Local).AddTicks(2941), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AssetTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AssetTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AssetTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
