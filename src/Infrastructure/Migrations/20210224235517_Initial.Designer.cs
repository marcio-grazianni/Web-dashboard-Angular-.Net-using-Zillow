﻿// <auto-generated />
using System;
using CDACommercial.PoC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CDACommercial.PoC.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210224235517_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MarketCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("MarketId")
                        .HasColumnType("bigint");

                    b.Property<string>("MarketName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("State")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Stats")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.HistoryRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Bedrooms")
                        .HasColumnType("int");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("HistoryRequests");
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.Listing", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("Bathrooms")
                        .HasColumnType("float");

                    b.Property<float>("Bedrooms")
                        .HasColumnType("float");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DaysOnZillow")
                        .HasColumnType("int");

                    b.Property<double>("HighCapRate")
                        .HasColumnType("double");

                    b.Property<double>("LastTaxPaid")
                        .HasColumnType("double");

                    b.Property<double>("LowCapRate")
                        .HasColumnType("double");

                    b.Property<double>("MiddleCapRate")
                        .HasColumnType("double");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("PriceHistory")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RentalizerData")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("SquareFootage")
                        .HasColumnType("float");

                    b.Property<string>("State")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Stats")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TaxHistory")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ZillowId")
                        .HasColumnType("bigint");

                    b.Property<string>("Zipcode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.MonthlyRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Bedrooms")
                        .HasColumnType("int");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Percentiles")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("MonthlyRecords");
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.HistoryRequest", b =>
                {
                    b.HasOne("CDACommercial.PoC.Domain.Entities.City", null)
                        .WithMany("Requests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.Listing", b =>
                {
                    b.HasOne("CDACommercial.PoC.Domain.Entities.City", "City")
                        .WithMany("Listings")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CDACommercial.PoC.Domain.Entities.MonthlyRecord", b =>
                {
                    b.HasOne("CDACommercial.PoC.Domain.Entities.City", null)
                        .WithMany("History")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
