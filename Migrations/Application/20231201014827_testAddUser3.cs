using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKIT_enterprise_final.Migrations.Application
{
    /// <inheritdoc />
    public partial class testAddUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
