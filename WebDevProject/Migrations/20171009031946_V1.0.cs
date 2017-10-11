using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebDevProject.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "testModels");

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    lectureText = table.Column<string>(nullable: true),
                    moduleOrder = table.Column<int>(nullable: false),
                    moduleTitle = table.Column<string>(nullable: true),
                    videoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: false),
                    lectureText = table.Column<string>(nullable: true),
                    topicOrder = table.Column<int>(nullable: false),
                    topicTitle = table.Column<string>(nullable: true),
                    videoURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModuleId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    correctCodeAnswer = table.Column<string>(nullable: true),
                    correctMultipleChoice = table.Column<int>(nullable: false),
                    isMultipleChoice = table.Column<bool>(nullable: false),
                    multipleChoice1 = table.Column<string>(nullable: true),
                    multipleChoice2 = table.Column<string>(nullable: true),
                    multipleChoice3 = table.Column<string>(nullable: true),
                    multipleChoice4 = table.Column<string>(nullable: true),
                    questionString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_ModuleId",
                table: "Question",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionId",
                table: "Question",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TopicId",
                table: "Question",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ModuleId",
                table: "Topic",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.CreateTable(
                name: "testModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    modelTestInt = table.Column<int>(nullable: false),
                    modelTestString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testModels", x => x.Id);
                });
        }
    }
}
