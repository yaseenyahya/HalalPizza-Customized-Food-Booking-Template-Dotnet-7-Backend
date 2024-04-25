﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using halalpizzabackend.Data;

#nullable disable

namespace halalpizzabackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240109170318_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("halalpizzabackend.Models.Addons", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ProductImagePath")
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ProductSalePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ID");

                    b.ToTable("Addons");
                });

            modelBuilder.Entity("halalpizzabackend.Models.Categories", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("halalpizzabackend.Models.Products", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("OtherSelectionSerialized")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductDetails")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductDetailsImagePath")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ProductImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ProductSalePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.Property<string>("variationSelectionItemsSerialized")
                        .HasColumnType("longtext");

                    b.Property<string>("variationSelectionTitle")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("halalpizzabackend.Models.SliderSettings", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Enabled")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("ID");

                    b.ToTable("SliderSettings");
                });

            modelBuilder.Entity("halalpizzabackend.Models.UserDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext");

                    b.Property<string>("BlockComments")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SettingsJSON")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserDetails");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "John Doe",
                            Password = "2a97516c354b68848cdbd8f54a226a0a55b21ed138e207ad6c5cbb9c00aa5aea",
                            Role = 2,
                            Status = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("halalpizzabackend.Models.Products", b =>
                {
                    b.HasOne("halalpizzabackend.Models.Categories", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
