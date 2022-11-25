using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Dev.Data.Migrations
{
    public partial class AdjustRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "Requests");
        }
    }
}
