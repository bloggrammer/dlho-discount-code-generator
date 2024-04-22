﻿// <auto-generated />
using DLHO.DiscountCodeGenerator.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DLHO.DiscountCodeGenerator.DataAccess.Migrations
{
    [DbContext(typeof(DiscountCodeDbContext))]
    partial class DiscountCodeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DLHO.DiscountCodeGenerator.Common.Models.DiscountCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("DiscountCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
