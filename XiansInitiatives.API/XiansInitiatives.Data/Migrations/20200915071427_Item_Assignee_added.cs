using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class Item_Assignee_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    CommenterId = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentItem_AspNetUsers_CommenterId",
                        column: x => x.CommenterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentItem_ActionItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ActionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemAssignee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    AssigneeId = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAssignee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAssignee_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemAssignee_ActionItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ActionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentItem_CommenterId",
                table: "CommentItem",
                column: "CommenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentItem_ItemId",
                table: "CommentItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAssignee_AssigneeId",
                table: "ItemAssignee",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAssignee_ItemId",
                table: "ItemAssignee",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentItem");

            migrationBuilder.DropTable(
                name: "ItemAssignee");
        }
    }
}
