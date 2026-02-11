using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterMyPlants.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnPlantName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Plants_Name",
                table: "Plants",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plants_Name",
                table: "Plants");
        }
    }
}
