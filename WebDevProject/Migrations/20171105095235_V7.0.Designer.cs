using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebDevProject.Models;

namespace WebDevProject.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20171105095235_V7.0")]
    partial class V70
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebDevProject.Models.Index", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MP4Link");

                    b.Property<string>("indexTitle");

                    b.Property<string>("lectureText");

                    b.Property<string>("youtubeURL");

                    b.HasKey("Id");

                    b.ToTable("Index");
                });

            modelBuilder.Entity("WebDevProject.Models.IndexReferenceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IndexId");

                    b.Property<string>("Link");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("IndexReferenceList");
                });

            modelBuilder.Entity("WebDevProject.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MP4Link");

                    b.Property<string>("lectureText");

                    b.Property<int>("moduleOrder");

                    b.Property<string>("moduleTitle");

                    b.Property<string>("videoURL");

                    b.HasKey("Id");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("WebDevProject.Models.ModuleReferenceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<int>("ModuleId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("ModuleReferenceList");
                });

            modelBuilder.Entity("WebDevProject.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MP4Link");

                    b.Property<int>("TopicId");

                    b.Property<string>("correctCodeAnswer");

                    b.Property<int?>("correctMultipleChoice");

                    b.Property<bool>("isMultipleChoice");

                    b.Property<string>("lectureText");

                    b.Property<string>("multipleChoice1");

                    b.Property<string>("multipleChoice2");

                    b.Property<string>("multipleChoice3");

                    b.Property<string>("multipleChoice4");

                    b.Property<int>("questionOrder");

                    b.Property<string>("questionString");

                    b.Property<string>("suppliedCode");

                    b.Property<string>("youtubeURL");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("WebDevProject.Models.QuestionReferenceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("QuestionReferenceList");
                });

            modelBuilder.Entity("WebDevProject.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MP4Link");

                    b.Property<int>("ModuleId");

                    b.Property<string>("lectureText");

                    b.Property<int>("topicOrder");

                    b.Property<string>("topicTitle");

                    b.Property<string>("videoURL");

                    b.HasKey("Id");

                    b.ToTable("Topic");
                });

            modelBuilder.Entity("WebDevProject.Models.TopicReferenceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<string>("Text");

                    b.Property<int>("TopicId");

                    b.HasKey("Id");

                    b.ToTable("TopicReferenceList");
                });
        }
    }
}
