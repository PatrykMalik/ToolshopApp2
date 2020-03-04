﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToolshopApp2.Data;

namespace ToolshopApp2.Migrations
{
    [DbContext(typeof(DatabaseConnectionContext))]
    [Migration("20200302145520_RequestStatusChange")]
    partial class RequestStatusChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToolshopApp2.Model.KindOfUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeOfUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KindOfUsers");
                });

            modelBuilder.Entity("ToolshopApp2.Model.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Attachment")
                        .HasColumnType("bit");

                    b.Property<string>("BeginigSrz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Classyfy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostCenter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescpriptionToolshop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndingSrz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Insurance")
                        .HasColumnType("bit");

                    b.Property<string>("InsuranceCost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Order")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageDimmention")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PackageOnPalltete")
                        .HasColumnType("bit");

                    b.Property<string>("PackageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Project")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestStatusesId")
                        .HasColumnType("int");

                    b.Property<string>("Srz1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Srz2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Srz3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Srz4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Srz5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Swz1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Swz2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Swz3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Swz4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Swz5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Transpot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("ToolshopApp2.Model.RequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestStatuses");
                });

            modelBuilder.Entity("ToolshopApp2.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Emial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KindOfUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KindOfUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToolshopApp2.Model.User", b =>
                {
                    b.HasOne("ToolshopApp2.Model.KindOfUser", "KindOfUser")
                        .WithMany()
                        .HasForeignKey("KindOfUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
