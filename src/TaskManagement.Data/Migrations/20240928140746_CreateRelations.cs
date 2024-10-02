using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations;

/// <inheritdoc />
public partial class CreateRelations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ProjectId",
            table: "Task",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "UserId",
            table: "Task",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "TaskId",
            table: "Comment",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Task_ProjectId",
            table: "Task",
            column: "ProjectId");

        migrationBuilder.CreateIndex(
            name: "IX_Task_UserId",
            table: "Task",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_TaskId",
            table: "Comment",
            column: "TaskId");

        migrationBuilder.AddForeignKey(
            name: "FK_Comment_Task_TaskId",
            table: "Comment",
            column: "TaskId",
            principalTable: "Task",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Task_Project_ProjectId",
            table: "Task",
            column: "ProjectId",
            principalTable: "Project",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Task_User_UserId",
            table: "Task",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Comment_Task_TaskId",
            table: "Comment");

        migrationBuilder.DropForeignKey(
            name: "FK_Task_Project_ProjectId",
            table: "Task");

        migrationBuilder.DropForeignKey(
            name: "FK_Task_User_UserId",
            table: "Task");

        migrationBuilder.DropIndex(
            name: "IX_Task_ProjectId",
            table: "Task");

        migrationBuilder.DropIndex(
            name: "IX_Task_UserId",
            table: "Task");

        migrationBuilder.DropIndex(
            name: "IX_Comment_TaskId",
            table: "Comment");

        migrationBuilder.DropColumn(
            name: "ProjectId",
            table: "Task");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Task");

        migrationBuilder.DropColumn(
            name: "TaskId",
            table: "Comment");
    }
}
