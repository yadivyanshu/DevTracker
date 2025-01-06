using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Features",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Features",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Features",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Features",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Features",
                newName: "Name");
        }
    }
}
