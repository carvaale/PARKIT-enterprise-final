using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKIT_enterprise_final.Migrations
{
    /// <inheritdoc />
    public partial class addWalletIDinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Users");
        }
    }
}
