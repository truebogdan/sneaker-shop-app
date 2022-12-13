using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBRepo.Migrations
{
    public partial class SizeSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "CartProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "CartProducts");
        }
    }
}
