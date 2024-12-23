﻿// <auto-generated />
using System;
using Company.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.DAL.Migrations
{
    [DbContext(typeof(CompanyDBContext))]
    partial class CompanyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Company.DAL.Models.Leader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("leaders");
                });

            modelBuilder.Entity("Company.DAL.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("members");
                });

            modelBuilder.Entity("Company.DAL.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("Company.DAL.Models.TaskMod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isDone")
                        .HasColumnType("bit");

                    b.Property<int?>("memberId")
                        .HasColumnType("int");

                    b.Property<int?>("projectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("memberId");

                    b.HasIndex("projectId");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("MemberProject", b =>
                {
                    b.Property<int>("membersId")
                        .HasColumnType("int");

                    b.Property<int>("projectsId")
                        .HasColumnType("int");

                    b.HasKey("membersId", "projectsId");

                    b.HasIndex("projectsId");

                    b.ToTable("MemberProject");
                });

            modelBuilder.Entity("Company.DAL.Models.Leader", b =>
                {
                    b.HasOne("Company.DAL.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Company.DAL.Models.TaskMod", b =>
                {
                    b.HasOne("Company.DAL.Models.Member", "member")
                        .WithMany("tasks")
                        .HasForeignKey("memberId");

                    b.HasOne("Company.DAL.Models.Project", "project")
                        .WithMany("tasks")
                        .HasForeignKey("projectId");

                    b.Navigation("member");

                    b.Navigation("project");
                });

            modelBuilder.Entity("MemberProject", b =>
                {
                    b.HasOne("Company.DAL.Models.Member", null)
                        .WithMany()
                        .HasForeignKey("membersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company.DAL.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("projectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.DAL.Models.Member", b =>
                {
                    b.Navigation("tasks");
                });

            modelBuilder.Entity("Company.DAL.Models.Project", b =>
                {
                    b.Navigation("tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
