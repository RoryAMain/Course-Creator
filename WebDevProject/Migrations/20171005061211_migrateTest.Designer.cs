using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebDevProject.Models;

namespace WebDevProject.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20171005061211_migrateTest")]
    partial class migrateTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebDevProject.Models.TestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("modelTestInt");

                    b.Property<string>("modelTestString");

                    b.HasKey("Id");

                    b.ToTable("testModels");
                });
        }
    }
}
