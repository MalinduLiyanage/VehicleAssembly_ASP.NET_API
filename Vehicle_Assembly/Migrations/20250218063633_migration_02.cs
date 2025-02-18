using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle_Assembly.Migrations
{
    /// <inheritdoc />
    public partial class migration_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assignee_id",
                table: "assembles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assembles_assignee_id",
                table: "assembles",
                column: "assignee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_assembles_admin_assignee_id",
                table: "assembles",
                column: "assignee_id",
                principalTable: "admin",
                principalColumn: "NIC",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assembles_admin_assignee_id",
                table: "assembles");

            migrationBuilder.DropIndex(
                name: "IX_assembles_assignee_id",
                table: "assembles");

            migrationBuilder.DropColumn(
                name: "assignee_id",
                table: "assembles");
        }
    }
}
