﻿// <auto-generated />
using Bonbonniere.Core.Enums;
using Bonbonniere.Infrastructure.EFData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Bonbonniere.Data.Migrations.Bonbonniere
{
    [DbContext(typeof(BonbonniereContext))]
    partial class BonbonniereContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bonbonniere.Core.Models.Bonbonniere.BookStore.BookInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(100);

                    b.Property<int>("CategoryId");

                    b.Property<string>("CoverImageUrl")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("ISBN10")
                        .HasMaxLength(100);

                    b.Property<string>("ISBN13")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive");

                    b.Property<int>("Language");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Publisher")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("B_BookInfo");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.Bonbonniere.BookStore.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("ModifiedTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("B_Category");
                });

            modelBuilder.Entity("Bonbonniere.Core.Models.Bonbonniere.BookStore.BookInfo", b =>
                {
                    b.HasOne("Bonbonniere.Core.Models.Bonbonniere.BookStore.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}