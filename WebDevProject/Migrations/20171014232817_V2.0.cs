using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDevProject.Migrations
{
    public partial class V20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Module_ModuleId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Question_QuestionId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Topic_TopicId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Topic_ModuleId",
                table: "Topic");

            migrationBuilder.DropIndex(
                name: "IX_Question_ModuleId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_TopicId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Question");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ModuleId",
                table: "Topic",
                column: "ModuleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Module_ModuleId",
                table: "Question",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Question_QuestionId",
                table: "Question",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Topic_TopicId",
                table: "Question",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topic_Module_ModuleId",
                table: "Topic",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
