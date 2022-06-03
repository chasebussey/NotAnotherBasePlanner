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

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FIOApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("ApplicationUser", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("IdentityRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityRoleClaim<string>", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserClaim<string>", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserLogin<string>", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityUserRole<string>", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("IdentityUserToken<string>", (string)null);
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Building", b =>
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

                    b.ToTable("Building");
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.BuildingCost", b =>
                {
                    b.Property<string>("BuildingTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaterialTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("BuildingTicker", "MaterialTicker");

                    b.HasIndex("MaterialTicker");

                    b.ToTable("BuildingCost");
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

                    b.ToTable("Material");
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.MaterialRecipe", b =>
                {
                    b.Property<string>("MaterialTicker")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("MaterialTicker", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("MaterialRecipe");
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

                    b.ToTable("Price");
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

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.BuildingCost", b =>
                {
                    b.HasOne("NotAnotherBasePlanner.Data.Building", "Building")
                        .WithMany("BuildingCosts")
                        .HasForeignKey("BuildingTicker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NotAnotherBasePlanner.Data.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialTicker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Material");
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
                    b.HasOne("NotAnotherBasePlanner.Data.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingTicker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("NotAnotherBasePlanner.Data.Building", b =>
                {
                    b.Navigation("BuildingCosts");
                });
#pragma warning restore 612, 618
        }
    }
}
