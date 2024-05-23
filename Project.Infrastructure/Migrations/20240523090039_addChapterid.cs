using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    public partial class addChapterid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_chapters_ChapterId",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "ChapterId",
                table: "questions",
                newName: "chapterId");

            migrationBuilder.RenameIndex(
                name: "IX_questions_ChapterId",
                table: "questions",
                newName: "IX_questions_chapterId");

            migrationBuilder.AlterColumn<Guid>(
                name: "chapterId",
                table: "questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
    name: "FK_questions_chapters_chapterId",
    table: "questions",
    column: "chapterId",
    principalTable: "chapters",
    principalColumn: "Id",
    onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_chapters_chapterId",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "chapterId",
                table: "questions",
                newName: "ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_questions_chapterId",
                table: "questions",
                newName: "IX_questions_ChapterId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChapterId",
                table: "questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_chapters_ChapterId",
                table: "questions",
                column: "ChapterId",
                principalTable: "chapters",
                principalColumn: "Id");
        }
    }
}
