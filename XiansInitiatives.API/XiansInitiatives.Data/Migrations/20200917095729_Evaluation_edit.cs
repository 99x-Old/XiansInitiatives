using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class Evaluation_edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewCycle_AspNetUsers_MemberId",
                table: "ReviewCycle");

            migrationBuilder.DropIndex(
                name: "IX_ReviewCycle_MemberId",
                table: "ReviewCycle");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "ReviewCycle");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluatorId",
                table: "ReviewCycle",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCycle_EvaluatorId",
                table: "ReviewCycle",
                column: "EvaluatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewCycle_AspNetUsers_EvaluatorId",
                table: "ReviewCycle",
                column: "EvaluatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewCycle_AspNetUsers_EvaluatorId",
                table: "ReviewCycle");

            migrationBuilder.DropIndex(
                name: "IX_ReviewCycle_EvaluatorId",
                table: "ReviewCycle");

            migrationBuilder.AlterColumn<string>(
                name: "EvaluatorId",
                table: "ReviewCycle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "ReviewCycle",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCycle_MemberId",
                table: "ReviewCycle",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewCycle_AspNetUsers_MemberId",
                table: "ReviewCycle",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
