using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetopiaApi.Migrations
{
    /// <inheritdoc />
    public partial class Advertisement3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Reactions",
                table: "Advertisements",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reactions",
                table: "Advertisements");
        }
    }
}
