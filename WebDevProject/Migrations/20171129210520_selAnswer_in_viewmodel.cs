using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDevProject.Migrations
{
    public partial class selAnswer_in_viewmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "selectedAnswer",
                table: "Question");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "selectedAnswer",
                table: "Question",
                nullable: false,
                defaultValue: 0);
        }
    }
}
