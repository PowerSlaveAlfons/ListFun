﻿// <auto-generated />
using System;
using IO.Swagger.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IO.Swagger.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("IO.Swagger.Models.Entry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<int>("Mediaid")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime");

                    b.HasKey("ID")
                        .HasName("EntryID");

                    b.HasIndex("Mediaid");

                    b.ToTable("Entry");
                });

            modelBuilder.Entity("IO.Swagger.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Platform")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("MediaID");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("IO.Swagger.Models.Entry", b =>
                {
                    b.HasOne("IO.Swagger.Models.Media", null)
                        .WithMany()
                        .HasForeignKey("Mediaid")
                        .HasConstraintName("FK_MediaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
