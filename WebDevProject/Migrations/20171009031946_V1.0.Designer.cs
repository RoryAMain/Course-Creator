using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebDevProject.Models;

namespace WebDevProject.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20171009031946_V1.0")]
    partial class V10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebDevProject.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("lectureText");

                    b.Property<int>("moduleOrder");

                    b.Property<string>("moduleTitle");

                    b.Property<string>("videoURL");

                    b.HasKey("Id");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("WebDevProject.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ModuleId");

                    b.Property<int?>("QuestionId");

                    b.Property<int>("TopicId");

                    b.Property<string>("correctCodeAnswer");

                    b.Property<int>("correctMultipleChoice");

                    b.Property<bool>("isMultipleChoice");

                    b.Property<string>("multipleChoice1");

                    b.Property<string>("multipleChoice2");

                    b.Property<string>("multipleChoice3");

                    b.Property<string>("multipleChoice4");

                    b.Property<string>("questionString");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TopicId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("WebDevProject.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ModuleId");

                    b.Property<string>("lectureText");

                    b.Property<int>("topicOrder");

                    b.Property<string>("topicTitle");

                    b.Property<string>("videoURL");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("WebDevProject.Models.Question", b =>
                {
                    b.HasOne("WebDevProject.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId");

                    b.HasOne("WebDevProject.Models.Question")
                        .WithMany("questionList")
                        .HasForeignKey("QuestionId");

                    b.HasOne("WebDevProject.Models.Topic")
                        .WithMany("questionList")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebDevProject.Models.Topic", b =>
                {
                    b.HasOne("WebDevProject.Models.Module", "Module")
                        .WithMany("topicList")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
