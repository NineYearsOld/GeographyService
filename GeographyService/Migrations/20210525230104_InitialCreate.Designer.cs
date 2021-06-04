﻿// <auto-generated />
using GeographyService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GeographyService.Migrations
{
    [DbContext(typeof(GeoContext))]
    [Migration("20210525230104_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeographyService.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("GeographyService.Models.CityMapping", b =>
                {
                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCapital")
                        .HasColumnType("bit");

                    b.HasKey("CityId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("CityMappings");
                });

            modelBuilder.Entity("GeographyService.Models.Continent", b =>
                {
                    b.Property<int>("ContinentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.HasKey("ContinentId");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("GeographyService.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<int>("Surface")
                        .HasColumnType("int");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GeographyService.Models.CountryMapping", b =>
                {
                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("ContinentId")
                        .HasColumnType("int");

                    b.HasKey("CountryId", "ContinentId");

                    b.HasIndex("ContinentId");

                    b.ToTable("CountryMappings");
                });

            modelBuilder.Entity("GeographyService.Models.River", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rivers");
                });

            modelBuilder.Entity("GeographyService.Models.RiverMapping", b =>
                {
                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("RiverId")
                        .HasColumnType("int");

                    b.HasKey("CountryId", "RiverId");

                    b.HasIndex("RiverId");

                    b.ToTable("RiverMappings");
                });

            modelBuilder.Entity("GeographyService.Models.CityMapping", b =>
                {
                    b.HasOne("GeographyService.Models.City", null)
                        .WithMany("CityMappings")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeographyService.Models.Country", null)
                        .WithMany("CityMappings")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeographyService.Models.CountryMapping", b =>
                {
                    b.HasOne("GeographyService.Models.Continent", null)
                        .WithMany("CountryMappings")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeographyService.Models.Country", null)
                        .WithMany("CountryMappings")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeographyService.Models.RiverMapping", b =>
                {
                    b.HasOne("GeographyService.Models.Country", null)
                        .WithMany("RiverMappings")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeographyService.Models.River", null)
                        .WithMany("RiverMappings")
                        .HasForeignKey("RiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GeographyService.Models.City", b =>
                {
                    b.Navigation("CityMappings");
                });

            modelBuilder.Entity("GeographyService.Models.Continent", b =>
                {
                    b.Navigation("CountryMappings");
                });

            modelBuilder.Entity("GeographyService.Models.Country", b =>
                {
                    b.Navigation("CityMappings");

                    b.Navigation("CountryMappings");

                    b.Navigation("RiverMappings");
                });

            modelBuilder.Entity("GeographyService.Models.River", b =>
                {
                    b.Navigation("RiverMappings");
                });
#pragma warning restore 612, 618
        }
    }
}
