using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    /// <inheritdoc />
    public partial class leadermembermanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaderMember",
                columns: table => new
                {
                    leadersId = table.Column<int>(type: "int", nullable: false),
                    membersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderMember", x => new { x.leadersId, x.membersId });
                    table.ForeignKey(
                        name: "FK_LeaderMember_leaders_leadersId",
                        column: x => x.leadersId,
                        principalTable: "leaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaderMember_members_membersId",
                        column: x => x.membersId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderMember_membersId",
                table: "LeaderMember",
                column: "membersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderMember");
        }
    }
}
