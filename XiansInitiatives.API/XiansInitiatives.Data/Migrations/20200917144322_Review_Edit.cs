using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class Review_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "EvaluationCriteria",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "EvaluationCriteria",
                newName: "Score");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "EvaluationCriteria",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "EvaluationCriteria",
                newName: "score");
        }
    }
}
