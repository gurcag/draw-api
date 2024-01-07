using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Draw.API.Migrations
{
    /// <inheritdoc />
    public partial class DrawDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupSize",
                table: "DrawOptions",
                newName: "NumberOfGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfGroups",
                table: "DrawOptions",
                newName: "GroupSize");
        }
    }
}
