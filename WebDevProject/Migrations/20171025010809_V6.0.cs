using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebDevProject.Migrations
{
    public partial class V60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MP4Link",
                table: "Topic",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MP4Link",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "youtubeURL",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MP4Link",
                table: "Module",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Index",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MP4Link = table.Column<string>(nullable: true),
                    indexTitle = table.Column<string>(nullable: true),
                    lectureText = table.Column<string>(nullable: true),
                    youtubeURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Index", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Index");

            migrationBuilder.DropColumn(
                name: "MP4Link",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "MP4Link",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "youtubeURL",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "MP4Link",
                table: "Module");
        }
    }
}
