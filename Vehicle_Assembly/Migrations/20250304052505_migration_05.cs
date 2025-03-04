using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle_Assembly.Migrations
{
    /// <inheritdoc />
    public partial class migration_05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "logininfo",
                keyColumn: "jwt",
                keyValue: null,
                column: "jwt",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "jwt",
                table: "logininfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "attachment_path",
                table: "assembles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attachment_path",
                table: "assembles");

            migrationBuilder.AlterColumn<string>(
                name: "jwt",
                table: "logininfo",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
