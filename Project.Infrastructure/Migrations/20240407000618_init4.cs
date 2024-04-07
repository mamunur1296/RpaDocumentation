using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChapterId",
                table: "questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_questions_ChapterId",
                table: "questions",
                column: "ChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_chapters_ChapterId",
                table: "questions",
                column: "ChapterId",
                principalTable: "chapters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_chapters_ChapterId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_ChapterId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                table: "questions");
        }
    }
}
