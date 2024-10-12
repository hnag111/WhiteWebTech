using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiteWebTech.API.Migrations
{
    /// <inheritdoc />
    public partial class Newfileadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "filename",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filename",
                table: "Applicants");
        }
    }
}
