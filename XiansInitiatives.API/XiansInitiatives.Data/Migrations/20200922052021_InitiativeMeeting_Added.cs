using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class InitiativeMeeting_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitiativeMeeting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Purpose = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ScheduledAt = table.Column<DateTime>(nullable: false),
                    MeetingNotes = table.Column<string>(nullable: true),
                    OrganizerId = table.Column<string>(nullable: false),
                    InitiativeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitiativeMeeting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitiativeMeeting_Initiative_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InitiativeMeeting_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeMeeting_InitiativeId",
                table: "InitiativeMeeting",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeMeeting_OrganizerId",
                table: "InitiativeMeeting",
                column: "OrganizerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitiativeMeeting");
        }
    }
}
