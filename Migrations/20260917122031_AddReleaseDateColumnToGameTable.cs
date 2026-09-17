using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddReleaseDateColumnToGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Games",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Games");
        }
    }
}
