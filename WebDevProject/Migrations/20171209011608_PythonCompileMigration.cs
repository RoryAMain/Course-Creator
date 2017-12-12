using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDevProject.Migrations
{
    public partial class PythonCompileMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<string>(
                name: "compiledError",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "compiledResult",
                table: "Question",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "compiledError",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "compiledResult",
                table: "Question");

        }
    }
}
