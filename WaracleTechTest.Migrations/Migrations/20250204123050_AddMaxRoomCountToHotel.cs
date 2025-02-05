using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaracleTechTest.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxRoomCountToHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxRoomCount",
                table: "Hotels",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxRoomCount",
                table: "Hotels");
        }
    }
}
