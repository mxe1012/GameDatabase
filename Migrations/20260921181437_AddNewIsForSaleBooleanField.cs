using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddNewIsForSaleBooleanField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForSale",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForSale",
                table: "Games");
        }
    }
}
