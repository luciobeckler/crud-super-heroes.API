﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using crud_super_heroes.API.Models;

#nullable disable

namespace crud_super_heroes.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241214143126_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("crud_super_heroes.API.Models.Heroi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Altura")
                        .HasColumnType("real");

                    b.Property<DateTime>("DataDeNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Peso")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Herois");
                });

            modelBuilder.Entity("crud_super_heroes.API.Models.HeroiSuperPoder", b =>
                {
                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.Property<int>("SuperPoderId")
                        .HasColumnType("int");

                    b.HasKey("HeroiId", "SuperPoderId");

                    b.HasIndex("SuperPoderId");

                    b.ToTable("HeroisSuperPoderes");
                });

            modelBuilder.Entity("crud_super_heroes.API.Models.SuperPoder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SuperPoderes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Super força",
                            Nome = "Força"
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Super velocidade",
                            Nome = "Velocidade"
                        });
                });

            modelBuilder.Entity("crud_super_heroes.API.Models.HeroiSuperPoder", b =>
                {
                    b.HasOne("crud_super_heroes.API.Models.Heroi", "Heroi")
                        .WithMany("HeroisSuperPoderes")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("crud_super_heroes.API.Models.SuperPoder", "SuperPoder")
                        .WithMany("HeroisSuperPoderes")
                        .HasForeignKey("SuperPoderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");

                    b.Navigation("SuperPoder");
                });

            modelBuilder.Entity("crud_super_heroes.API.Models.Heroi", b =>
                {
                    b.Navigation("HeroisSuperPoderes");
                });

            modelBuilder.Entity("crud_super_heroes.API.Models.SuperPoder", b =>
                {
                    b.Navigation("HeroisSuperPoderes");
                });
#pragma warning restore 612, 618
        }
    }
}