﻿// <auto-generated />
using System;
using Childhood.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Childhood.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191220062545_Tutor3")]
    partial class Tutor3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Childhood.Models.AddActions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AddActions");
                });

            modelBuilder.Entity("Childhood.Models.Child", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DaddyId")
                        .HasColumnType("int");

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MomId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DaddyId");

                    b.HasIndex("GroupId");

                    b.HasIndex("MomId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("Childhood.Models.ChildCheck", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckType")
                        .HasColumnType("int");

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ChildId");

                    b.ToTable("ChildChecks");
                });

            modelBuilder.Entity("Childhood.Models.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TutorId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TutorId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Childhood.Models.Information", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Information");
                });

            modelBuilder.Entity("Childhood.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FIO = "Director",
                            Login = "Director",
                            PasswordHash = "7C5BA892645AF8D7DBA520E3978C726F",
                            Phone = "000",
                            UserType = 0
                        },
                        new
                        {
                            ID = 2,
                            FIO = "Tutor",
                            Login = "Tutor",
                            PasswordHash = "2EC7805E0CCE36C07750D04F60857CD9",
                            Phone = "000",
                            UserType = 1
                        });
                });

            modelBuilder.Entity("Childhood.Models.Child", b =>
                {
                    b.HasOne("Childhood.Models.User", "Daddy")
                        .WithMany()
                        .HasForeignKey("DaddyId");

                    b.HasOne("Childhood.Models.Group", "Group")
                        .WithMany("Children")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Childhood.Models.User", "Mom")
                        .WithMany()
                        .HasForeignKey("MomId");
                });

            modelBuilder.Entity("Childhood.Models.ChildCheck", b =>
                {
                    b.HasOne("Childhood.Models.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Childhood.Models.Group", b =>
                {
                    b.HasOne("Childhood.Models.User", "Tutor")
                        .WithMany()
                        .HasForeignKey("TutorId");
                });
#pragma warning restore 612, 618
        }
    }
}
