using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEstimatedDurationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RepairTasks_RepairTaskId",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTasks",
                table: "RepairTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RepairTasks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTasks",
                table: "RepairTasks",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RepairTasks_RepairTaskId",
                table: "Parts",
                column: "RepairTaskId",
                principalTable: "RepairTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RepairTasks_RepairTaskId",
                table: "Parts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTasks",
                table: "RepairTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RepairTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTasks",
                table: "RepairTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RepairTasks_RepairTaskId",
                table: "Parts",
                column: "RepairTaskId",
                principalTable: "RepairTasks",
                principalColumn: "Id");
        }
    }
}
