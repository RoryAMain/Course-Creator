﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebDevProject.Models;

namespace WebDevProject.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20171021062217_V4.1")]
    partial class V41
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

                    b.Property<int>("TopicId");

                    b.Property<string>("correctCodeAnswer");

                    b.Property<int?>("correctMultipleChoice");

                    b.Property<bool>("isMultipleChoice");

                    b.Property<string>("lectureText");

                    b.Property<string>("multipleChoice1");

                    b.Property<string>("multipleChoice2");

                    b.Property<string>("multipleChoice3");

                    b.Property<string>("multipleChoice4");

                    b.Property<string>("questionString");

                    b.Property<string>("suppliedCode");

                    b.HasKey("Id");

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

                    b.ToTable("Topic");
                });
        }
    }
}