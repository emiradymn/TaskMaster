using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMaster.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEngineer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EngineerId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Engineers",
                columns: table => new
                {
                    EngineerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EngineerName = table.Column<string>(type: "TEXT", nullable: true),
                    EngineerUsername = table.Column<string>(type: "TEXT", nullable: true),
                    EngineerEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EngineerPhone = table.Column<string>(type: "TEXT", nullable: true),
                    JobDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineers", x => x.EngineerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSaves_StajyerId",
                table: "TaskSaves",
                column: "StajyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSaves_TaskId",
                table: "TaskSaves",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EngineerId",
                table: "Tasks",
                column: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Engineers_EngineerId",
                table: "Tasks",
                column: "EngineerId",
                principalTable: "Engineers",
                principalColumn: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSaves_Stajyers_StajyerId",
                table: "TaskSaves",
                column: "StajyerId",
                principalTable: "Stajyers",
                principalColumn: "StajyerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSaves_Tasks_TaskId",
                table: "TaskSaves",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Engineers_EngineerId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSaves_Stajyers_StajyerId",
                table: "TaskSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSaves_Tasks_TaskId",
                table: "TaskSaves");

            migrationBuilder.DropTable(
                name: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_TaskSaves_StajyerId",
                table: "TaskSaves");

            migrationBuilder.DropIndex(
                name: "IX_TaskSaves_TaskId",
                table: "TaskSaves");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EngineerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EngineerId",
                table: "Tasks");
        }
    }
}
