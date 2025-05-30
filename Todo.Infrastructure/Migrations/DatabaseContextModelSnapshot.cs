﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.Infrastructure;

#nullable disable

namespace Todo.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.36");

            modelBuilder.Entity("Todo.Domain.Models.TodoList", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfTasks")
                        .HasColumnType("INTEGER")
                        .HasColumnName("number_of_tasks");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("todo_lists", "dbo");
                });

            modelBuilder.Entity("Todo.Domain.Models.TodoTask", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<int?>("HolderId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("is_completed");

                    b.Property<int?>("TodoId")
                        .IsRequired()
                        .HasColumnType("INTEGER")
                        .HasColumnName("todo_id");

                    b.HasKey("Id");

                    b.HasIndex("HolderId");

                    b.ToTable("todo_tasks", "dbo");
                });

            modelBuilder.Entity("Todo.Domain.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("is_admin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("users", "dbo");
                });

            modelBuilder.Entity("Todo.Domain.Models.TodoList", b =>
                {
                    b.HasOne("Todo.Domain.Models.User", "Owner")
                        .WithMany("Todos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Todo.Domain.Models.TodoTask", b =>
                {
                    b.HasOne("Todo.Domain.Models.TodoList", "Holder")
                        .WithMany("Tasks")
                        .HasForeignKey("HolderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Holder");
                });

            modelBuilder.Entity("Todo.Domain.Models.TodoList", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Todo.Domain.Models.User", b =>
                {
                    b.Navigation("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
