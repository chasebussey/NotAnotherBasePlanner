﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotAnotherBasePlanner.Data;

#nullable disable

namespace NotAnotherBasePlanner.Migrations
{
    [DbContext(typeof(PlannerContext))]
    partial class PlannerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Building", b =>
                {
                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AreaCost")
                        .HasColumnType("int");

                    b.Property<int>("Engineers")
                        .HasColumnType("int");

                    b.Property<string>("Expertise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pioneers")
                        .HasColumnType("int");

                    b.Property<int>("Scientists")
                        .HasColumnType("int");

                    b.Property<int>("Settlers")
                        .HasColumnType("int");

                    b.Property<int>("Technicians")
                        .HasColumnType("int");

                    b.HasKey("Ticker");

                    b.ToTable("Building", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Material", b =>
                {
                    b.Property<string>("Ticker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Volume")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Ticker");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.MaterialRecipe", b =>
                {
                    b.Property<string>("MaterialTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("MaterialTicker", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("MaterialRecipe", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Price", b =>
                {
                    b.Property<string>("MaterialTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExchangeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Ask")
                        .HasColumnType("float");

                    b.Property<double?>("Bid")
                        .HasColumnType("float");

                    b.Property<int?>("BidCount")
                        .HasColumnType("int");

                    b.Property<int?>("Demand")
                        .HasColumnType("int");

                    b.Property<double?>("MarketMakerBuy")
                        .HasColumnType("float");

                    b.Property<double?>("MarketMakerSell")
                        .HasColumnType("float");

                    b.Property<double?>("PriceAverage")
                        .HasColumnType("float");

                    b.Property<int?>("Supply")
                        .HasColumnType("int");

                    b.HasKey("MaterialTicker", "ExchangeCode");

                    b.ToTable("Price", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"), 1L, 1);

                    b.Property<string>("BuildingTicker")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Inputs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Outputs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeMs")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("BuildingTicker");

                    b.ToTable("Recipe", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.MaterialRecipe", b =>
                {
                    b.HasOne("NotAnotherBasePlanner.Data.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialTicker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotAnotherBasePlanner.Data.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Recipe", b =>
                {
                    b.HasOne("Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingTicker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });
#pragma warning restore 612, 618
        }
    }
}
