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
    [Migration("20231219174247_AddedTrialSubscription")]
    partial class AddedTrialSubscription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("currency_converter.Data.Entities.Conversion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("FromCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("FromCurrencyIndex")
                        .HasColumnType("REAL");

                    b.Property<float>("Result")
                        .HasColumnType("REAL");

                    b.Property<int>("ToCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ToCurrencyIndex")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FromCurrencyId");

                    b.HasIndex("ToCurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Conversions");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ConversionIndex")
                        .HasColumnType("REAL");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

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

                    b.Property<string>("SubscriptionPicture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("UsdPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConverterLimit = 10,
                            Name = "Free",
                            SubscriptionPicture = "http://2.bp.blogspot.com/-Kjm_y-4VY6Q/T-CZuH4eDMI/AAAAAAAABUg/H5mIxef6Tjc/s1600/Free1.jpg",
                            UsdPrice = 0f
                        },
                        new
                        {
                            Id = 2,
                            ConverterLimit = -1,
                            Name = "Premium",
                            SubscriptionPicture = "",
                            UsdPrice = 9.99f
                        },
                        new
                        {
                            Id = 3,
                            ConverterLimit = 100,
                            Name = "Trial",
                            SubscriptionPicture = "",
                            UsdPrice = 2.49f
                        });
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConverterUses")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("FavoriteCurrencyId")
                        .HasColumnType("INTEGER");

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

                    b.HasIndex("FavoriteCurrencyId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConverterUses = 0,
                            Email = "admin@mail.com",
                            PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            Role = 1,
                            SubscriptionId = 2,
                            Username = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            ConverterUses = 0,
                            Email = "user@mail.com",
                            PasswordHash = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8",
                            Role = 0,
                            SubscriptionId = 1,
                            Username = "user1"
                        });
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Conversion", b =>
                {
                    b.HasOne("currency_converter.Data.Entities.Currency", "FromCurrency")
                        .WithMany()
                        .HasForeignKey("FromCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("currency_converter.Data.Entities.Currency", "ToCurrency")
                        .WithMany()
                        .HasForeignKey("ToCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("currency_converter.Data.Entities.User", "User")
                        .WithMany("ConversionHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromCurrency");

                    b.Navigation("ToCurrency");

                    b.Navigation("User");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.HasOne("currency_converter.Data.Entities.Currency", "FavoriteCurrency")
                        .WithMany()
                        .HasForeignKey("FavoriteCurrencyId");

                    b.HasOne("currency_converter.Data.Entities.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FavoriteCurrency");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("currency_converter.Data.Entities.User", b =>
                {
                    b.Navigation("ConversionHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
