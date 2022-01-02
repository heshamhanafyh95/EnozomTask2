﻿// <auto-generated />
using EnozomTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnozomTask.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EnozomTask.Models.Country", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCca2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("EnozomTask.Models.Holiday", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("HolidayEndDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HolidayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HolidayStartDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("countryid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("countryid");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("EnozomTask.Models.Holiday", b =>
                {
                    b.HasOne("EnozomTask.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("countryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });
#pragma warning restore 612, 618
        }
    }
}
