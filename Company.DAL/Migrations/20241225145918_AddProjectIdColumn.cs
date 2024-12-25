using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaders_projects_ProjectId",
                table: "leaders");

            migrationBuilder.DropIndex(
                name: "IX_leaders_ProjectId",
                table: "leaders");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "leaders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_leaders_ProjectId",
                table: "leaders",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_leaders_projects_ProjectId",
                table: "leaders",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaders_projects_ProjectId",
                table: "leaders");

            migrationBuilder.DropIndex(
                name: "IX_leaders_ProjectId",
                table: "leaders");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "leaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_leaders_ProjectId",
                table: "leaders",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaders_projects_ProjectId",
                table: "leaders",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id");
        }
    }
}
