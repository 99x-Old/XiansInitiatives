using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XiansInitiatives.Data.Migrations
{
    public partial class InitMember_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeMember_AspNetUsers_MemberId",
                table: "InitiativeMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitiativeMember",
                table: "InitiativeMember");

            migrationBuilder.DropIndex(
                name: "IX_InitiativeMember_InitiativeId",
                table: "InitiativeMember");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InitiativeMember");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "InitiativeMember",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitiativeMember",
                table: "InitiativeMember",
                columns: new[] { "InitiativeId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeMember_AspNetUsers_MemberId",
                table: "InitiativeMember",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitiativeMember_AspNetUsers_MemberId",
                table: "InitiativeMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitiativeMember",
                table: "InitiativeMember");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "InitiativeMember",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "InitiativeMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitiativeMember",
                table: "InitiativeMember",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InitiativeMember_InitiativeId",
                table: "InitiativeMember",
                column: "InitiativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InitiativeMember_AspNetUsers_MemberId",
                table: "InitiativeMember",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
