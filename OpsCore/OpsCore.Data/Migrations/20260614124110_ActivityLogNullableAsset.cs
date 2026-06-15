using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ActivityLogNullableAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Assets_AssetId",
                table: "ActivityLogs");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "ActivityLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Assets_AssetId",
                table: "ActivityLogs",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_Assets_AssetId",
                table: "ActivityLogs");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "ActivityLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_Assets_AssetId",
                table: "ActivityLogs",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
