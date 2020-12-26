using Microsoft.EntityFrameworkCore.Migrations;

namespace Son_Hali.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "VideoModels");

            migrationBuilder.DropColumn(
                name: "VideoYolu",
                table: "VideoModels");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "VideoModels",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "VideoModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "VideoModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoYolu",
                table: "VideoModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
