﻿// <auto-generated />
using ManufacturingApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManufacturingApp.Migrations
{
    [DbContext(typeof(ManufacturingDbContext))]
    partial class ManufacturingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ManufacturingApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ManufacturingApp.Models.RawMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RawMaterials");
                });

            modelBuilder.Entity("ManufacturingApp.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("ManufacturingApp.Models.RecipeProduct", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.HasKey("RecipeId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("RecipeProducts");
                });

            modelBuilder.Entity("ManufacturingApp.Models.RecipeRawMaterial", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.HasKey("RecipeId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("RecipeRawMaterials");
                });

            modelBuilder.Entity("ManufacturingApp.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ManufacturingApp.Models.SupplierRawMaterial", b =>
                {
                    b.Property<int>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<int>("RawMaterialId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("SupplierId", "RawMaterialId");

                    b.HasIndex("RawMaterialId");

                    b.ToTable("SupplierRawMaterials");
                });

            modelBuilder.Entity("ManufacturingApp.Models.RecipeProduct", b =>
                {
                    b.HasOne("ManufacturingApp.Models.Product", "Product")
                        .WithMany("RecipeProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManufacturingApp.Models.Recipe", "Recipe")
                        .WithMany("RecipeProducts")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("ManufacturingApp.Models.RecipeRawMaterial", b =>
                {
                    b.HasOne("ManufacturingApp.Models.RawMaterial", "RawMaterial")
                        .WithMany()
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManufacturingApp.Models.Recipe", "Recipe")
                        .WithMany("RecipeRawMaterials")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawMaterial");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("ManufacturingApp.Models.SupplierRawMaterial", b =>
                {
                    b.HasOne("ManufacturingApp.Models.RawMaterial", "RawMaterial")
                        .WithMany()
                        .HasForeignKey("RawMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManufacturingApp.Models.Supplier", "Supplier")
                        .WithMany("SupplierRawMaterials")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawMaterial");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ManufacturingApp.Models.Product", b =>
                {
                    b.Navigation("RecipeProducts");
                });

            modelBuilder.Entity("ManufacturingApp.Models.Recipe", b =>
                {
                    b.Navigation("RecipeProducts");

                    b.Navigation("RecipeRawMaterials");
                });

            modelBuilder.Entity("ManufacturingApp.Models.Supplier", b =>
                {
                    b.Navigation("SupplierRawMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}