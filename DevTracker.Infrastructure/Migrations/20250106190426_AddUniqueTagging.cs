using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueTagging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Taggings_EntityId_EntityType_TagId",
                table: "Taggings",
                columns: new[] { "EntityId", "EntityType", "TagId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Taggings_EntityId_EntityType_TagId",
                table: "Taggings");
        }
    }
}
