using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    /// <inheritdoc />
    public partial class taskChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_members_memberId",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "memberId",
                table: "tasks",
                newName: "MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_memberId",
                table: "tasks",
                newName: "IX_tasks_MemberID");

            migrationBuilder.AlterColumn<int>(
                name: "MemberID",
                table: "tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isDone",
                table: "tasks",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_members_MemberID",
                table: "tasks",
                column: "MemberID",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_members_MemberID",
                table: "tasks");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "tasks",
                newName: "memberId");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_MemberID",
                table: "tasks",
                newName: "IX_tasks_memberId");

            migrationBuilder.AlterColumn<bool>(
                name: "isDone",
                table: "tasks",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "memberId",
                table: "tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_members_memberId",
                table: "tasks",
                column: "memberId",
                principalTable: "members",
                principalColumn: "Id");
        }
    }
}
