using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProblemDescription",
                table: "WorkOrders");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndAtUtc",
                table: "WorkOrders",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartAtUtc",
                table: "WorkOrders",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkOrderId",
                table: "RepairTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_VehicleId",
                table: "WorkOrders",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairTasks_WorkOrderId",
                table: "RepairTasks",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairTasks_WorkOrders_WorkOrderId",
                table: "RepairTasks",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Vehicles_VehicleId",
                table: "WorkOrders",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairTasks_WorkOrders_WorkOrderId",
                table: "RepairTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Vehicles_VehicleId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_VehicleId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_RepairTasks_WorkOrderId",
                table: "RepairTasks");

            migrationBuilder.DropColumn(
                name: "EndAtUtc",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "StartAtUtc",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "RepairTasks");

            migrationBuilder.AddColumn<string>(
                name: "ProblemDescription",
                table: "WorkOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
