using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeTarefas.Migrations
{
    public partial class SimpleUpdateTasksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_IdUser",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Task_IdUser",
                table: "Tasks",
                newName: "IX_Tasks_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_IdUser",
                table: "Tasks",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_IdUser",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_IdUser",
                table: "Task",
                newName: "IX_Task_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_IdUser",
                table: "Task",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
