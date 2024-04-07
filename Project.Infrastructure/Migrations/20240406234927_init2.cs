using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsTopic");

            migrationBuilder.CreateIndex(
                name: "IX_questions_TopicId",
                table: "questions",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_tipics_TopicId",
                table: "questions",
                column: "TopicId",
                principalTable: "tipics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_tipics_TopicId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_TopicId",
                table: "questions");

            migrationBuilder.CreateTable(
                name: "QuestionsTopic",
                columns: table => new
                {
                    QuestionsListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsTopic", x => new { x.QuestionsListId, x.TopicListId });
                    table.ForeignKey(
                        name: "FK_QuestionsTopic_questions_QuestionsListId",
                        column: x => x.QuestionsListId,
                        principalTable: "questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsTopic_tipics_TopicListId",
                        column: x => x.TopicListId,
                        principalTable: "tipics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTopic_TopicListId",
                table: "QuestionsTopic",
                column: "TopicListId");
        }
    }
}
