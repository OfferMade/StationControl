using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationControl.Migrations
{
    /// <inheritdoc />
    public partial class new_deputy_prop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "Deputies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Deputies");
        }
    }
}
