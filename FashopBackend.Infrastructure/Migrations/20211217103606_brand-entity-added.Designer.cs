﻿// <auto-generated />
using System;
using FashopBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FashopBackend.Infrastructure.Migrations
{
    [DbContext(typeof(FashopContext))]
    [Migration("20211217103606_brand-entity-added")]
    partial class brandentityadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FashopBackend.Core.Aggregate.BrandAggregate.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("brands");
                });

            modelBuilder.Entity("FashopBackend.Core.Aggregate.CategoryAggregate.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("FashopBackend.Core.Aggregate.ProductAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("products_categories", b =>
                {
                    b.Property<int>("category_id")
                        .HasColumnType("integer");

                    b.Property<int>("product_id")
                        .HasColumnType("integer");

                    b.HasKey("category_id", "product_id");

                    b.HasIndex("product_id");

                    b.ToTable("products_categories");
                });

            modelBuilder.Entity("FashopBackend.Core.Aggregate.ProductAggregate.Product", b =>
                {
                    b.HasOne("FashopBackend.Core.Aggregate.BrandAggregate.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId");

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("products_categories", b =>
                {
                    b.HasOne("FashopBackend.Core.Aggregate.CategoryAggregate.Category", null)
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FashopBackend.Core.Aggregate.ProductAggregate.Product", null)
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FashopBackend.Core.Aggregate.BrandAggregate.Brand", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
