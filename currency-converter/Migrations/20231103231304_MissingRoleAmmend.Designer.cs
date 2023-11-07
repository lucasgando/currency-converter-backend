﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using currency_converter.Data;

#nullable disable

namespace currency_converter.Migrations
{
    [DbContext(typeof(ConverterContext))]
    [Migration("20231103231304_MissingRoleAmmend")]
    partial class MissingRoleAmmend
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("currency_converter.Data.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ConversionIndex")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConverterLimit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsdPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConverterLimit = 10,
                            Name = "free",
                            UsdPrice = 0
                        },
                        new
                        {
                            Id = 2,
                            ConverterLimit = -1,
                            Name = "premium",
                            UsdPrice = 10
                        });
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail.com",
                            PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            Role = 1,
                            SubscriptionId = 2,
                            Username = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user@mail.com",
                            PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            Role = 0,
                            SubscriptionId = 1,
                            Username = "user1"
                        });
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Currency", b =>
                {
                    b.HasOne("currency_converter.Data.Entities.User", null)
                        .WithMany("FavoriteCurrencies")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.HasOne("currency_converter.Data.Entities.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.Navigation("FavoriteCurrencies");
                });
#pragma warning restore 612, 618
        }
    }
}
