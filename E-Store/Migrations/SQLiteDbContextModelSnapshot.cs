﻿// <auto-generated />
using System;
using E_Store.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EStore.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    partial class SQLiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("E_Store.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAdmin = true,
                            Name = "Elshad",
                            Password = "Hasanoff17",
                            Surname = "Hasanov",
                            UserName = "hasanoff2005"
                        },
                        new
                        {
                            Id = 2,
                            IsAdmin = false,
                            Name = "StepIt",
                            Password = "12345",
                            Surname = "Academy",
                            UserName = "Step"
                        });
                });

            modelBuilder.Entity("E_Store.Models.MessageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2023, 1, 19, 20, 41, 45, 102, DateTimeKind.Local).AddTicks(3889),
                            Email = "hesenovelsad468@gmail.com",
                            Fullname = "Elshad Hasanov",
                            Message = "Fruit is Great!!!!"
                        });
                });

            modelBuilder.Entity("E_Store.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = "Step",
                            Price = 3.3999999999999999,
                            ProductName = "Apple",
                            Weight = 1.0
                        });
                });

            modelBuilder.Entity("E_Store.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/74132_-Apple_Imperfect-Fuji-1.5Kg-Square-_1200x1200px_medium.jpg?v=1649650428",
                            Price = 5.6799999999999997,
                            ProductName = "Red Apple"
                        },
                        new
                        {
                            Id = 2,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/24141-done_medium.jpg?v=1640211252",
                            Price = 0.5,
                            ProductName = "Apricot"
                        },
                        new
                        {
                            Id = 3,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/4152-done_medium.jpg?v=1611027746",
                            Price = 2.5,
                            ProductName = "Avacado"
                        },
                        new
                        {
                            Id = 4,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/4211_-Bananas-Each-Square-_1200x1200px_medium.jpg?v=1650600692",
                            Price = 0.69999999999999996,
                            ProductName = "Banana"
                        },
                        new
                        {
                            Id = 5,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/4242-done_medium.jpg?v=1625704287",
                            Price = 3.4900000000000002,
                            ProductName = "Black Berry"
                        },
                        new
                        {
                            Id = 6,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/blueberries-resized_medium.jpg?v=1594262022",
                            Price = 3.9900000000000002,
                            ProductName = "Blue Berry"
                        },
                        new
                        {
                            Id = 7,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/coconuts_4382_resized_d9dfdbc5-5037-41cb-88d2-43088d2c449c_medium.jpeg?v=1594264018",
                            Price = 5.4900000000000002,
                            ProductName = "Coconut"
                        },
                        new
                        {
                            Id = 9,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/Black_Grapes_2cdd8a19-e047-43ed-b3ea-d809b0422cfb_medium.jpeg?v=1607485081",
                            Price = 20.289999999999999,
                            ProductName = "Black Grape"
                        },
                        new
                        {
                            Id = 10,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/grapes_red_seedless_resized_0d6c120f-f276-41e8-8efb-c4d3764be8db_medium.jpeg?v=1594265102",
                            Price = 14.289999999999999,
                            ProductName = "Red Grape"
                        },
                        new
                        {
                            Id = 11,
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0206/9470/products/44911-thomson-white-grapes-done_medium.jpg?v=1614895413",
                            Price = 13.289999999999999,
                            ProductName = "White Grape"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
