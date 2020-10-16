using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class Review_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Criteria = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Justification = table.Column<string>(nullable: true),
                    weight = table.Column<double>(nullable: false),
                    score = table.Column<double>(nullable: false),
                    ReviewCycleId = table.Column<Guid>(nullable: false),
                    InitiativeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriteria_Initiative_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewCycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    OverallComment = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InitiativeId = table.Column<Guid>(nullable: false),
                    EvaluatorId = table.Column<string>(nullable: false),
                    MemberId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewCycle_Initiative_InitiativeId",
                        column: x => x.InitiativeId,
                        principalTable: "Initiative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewCycle_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_InitiativeId",
                table: "EvaluationCriteria",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCycle_InitiativeId",
                table: "ReviewCycle",
                column: "InitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCycle_MemberId",
                table: "ReviewCycle",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriteria");

            migrationBuilder.DropTable(
                name: "ReviewCycle");
        }
    }
}
