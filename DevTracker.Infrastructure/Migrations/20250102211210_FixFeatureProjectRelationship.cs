using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFeatureProjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Projects_ProjectId1",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProjectId1",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Features");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "Features",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProjectId1",
                table: "Features",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Projects_ProjectId1",
                table: "Features",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
