﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhotoApi.Models;

namespace PhotoApi.Migrations
{
    [DbContext(typeof(PhotoDbContext))]
    [Migration("20200917192919_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PhotoApi.Models.Face", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PhotoHash")
                        .HasColumnType("int");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PhotoHash");

                    b.ToTable("Faces");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            PersonId = -1,
                            PhotoHash = 148592049
                        },
                        new
                        {
                            Id = -2,
                            PersonId = -2,
                            PhotoHash = 148593649
                        });
                });

            modelBuilder.Entity("PhotoApi.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            FirstName = "Anna",
                            LastName = "Gurova",
                            Patronymic = "Aleksandrovna"
                        },
                        new
                        {
                            Id = -2,
                            FirstName = "Petr",
                            LastName = "Petrov",
                            Patronymic = "Petrovich"
                        });
                });

            modelBuilder.Entity("PhotoApi.Models.Face", b =>
                {
                    b.HasOne("PhotoApi.Models.Person", "Person")
                        .WithMany("Faces")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}