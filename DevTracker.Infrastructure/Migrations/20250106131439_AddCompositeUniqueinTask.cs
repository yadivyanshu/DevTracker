using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositeUniqueinTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskItems_FeatureId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_Title",
                table: "TaskItems");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_FeatureId_Title",
                table: "TaskItems",
                columns: new[] { "FeatureId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskItems_FeatureId_Title",
                table: "TaskItems");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_FeatureId",
                table: "TaskItems",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_Title",
                table: "TaskItems",
                column: "Title",
                unique: true);
        }
    }
}
