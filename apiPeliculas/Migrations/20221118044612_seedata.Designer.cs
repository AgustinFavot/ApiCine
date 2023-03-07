﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using apiPeliculas;

#nullable disable

namespace apiPeliculas.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221118044612_seedata")]
    partial class seedata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("apiPeliculas.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Actores");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            FechaNacimiento = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Jim Carrey"
                        },
                        new
                        {
                            Id = 6,
                            FechaNacimiento = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 7,
                            FechaNacimiento = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Chris Evans"
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Nombre = "Aventura"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Animación"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Suspenso"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Romance"
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            EnCines = true,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 3,
                            EnCines = false,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 4,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 5,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Emma"
                        },
                        new
                        {
                            Id = 6,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Wonder Woman 1984"
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasActores", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("PeliculasActores");

                    b.HasData(
                        new
                        {
                            PeliculaId = 2,
                            ActorId = 6,
                            Orden = 1
                        },
                        new
                        {
                            PeliculaId = 2,
                            ActorId = 7,
                            Orden = 2
                        },
                        new
                        {
                            PeliculaId = 3,
                            ActorId = 6,
                            Orden = 1
                        },
                        new
                        {
                            PeliculaId = 3,
                            ActorId = 7,
                            Orden = 2
                        },
                        new
                        {
                            PeliculaId = 4,
                            ActorId = 5,
                            Orden = 1
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasGeneros", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "GeneroId");

                    b.HasIndex("GeneroId");

                    b.ToTable("PeliculasGeneros");

                    b.HasData(
                        new
                        {
                            PeliculaId = 2,
                            GeneroId = 6
                        },
                        new
                        {
                            PeliculaId = 2,
                            GeneroId = 4
                        },
                        new
                        {
                            PeliculaId = 3,
                            GeneroId = 6
                        },
                        new
                        {
                            PeliculaId = 3,
                            GeneroId = 4
                        },
                        new
                        {
                            PeliculaId = 4,
                            GeneroId = 4
                        },
                        new
                        {
                            PeliculaId = 5,
                            GeneroId = 6
                        },
                        new
                        {
                            PeliculaId = 5,
                            GeneroId = 7
                        },
                        new
                        {
                            PeliculaId = 6,
                            GeneroId = 6
                        },
                        new
                        {
                            PeliculaId = 6,
                            GeneroId = 4
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasSalasCine", b =>
                {
                    b.Property<int>("SalaCineId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.HasKey("SalaCineId", "PeliculaId");

                    b.HasIndex("PeliculaId");

                    b.ToTable("PeliculasSalasCine");
                });

            modelBuilder.Entity("apiPeliculas.Models.SalaCine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Ubicacion")
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("SalaCines");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Nombre = "Sambil",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.9118804 18.4826214)")
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Megacentro",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.856427 18.506934)")
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Village East Cinema",
                            Ubicacion = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)")
                        });
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasActores", b =>
                {
                    b.HasOne("apiPeliculas.Models.Actor", "Actor")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiPeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasGeneros", b =>
                {
                    b.HasOne("apiPeliculas.Models.Genero", "Genero")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiPeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");

                    b.Navigation("Pelicula");
                });

            modelBuilder.Entity("apiPeliculas.Models.PeliculasSalasCine", b =>
                {
                    b.HasOne("apiPeliculas.Models.Pelicula", "Pelicula")
                        .WithMany("PeliculasSalasCines")
                        .HasForeignKey("PeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apiPeliculas.Models.SalaCine", "SalaCine")
                        .WithMany("PeliculasSalasCines")
                        .HasForeignKey("SalaCineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pelicula");

                    b.Navigation("SalaCine");
                });

            modelBuilder.Entity("apiPeliculas.Models.Actor", b =>
                {
                    b.Navigation("PeliculasActores");
                });

            modelBuilder.Entity("apiPeliculas.Models.Genero", b =>
                {
                    b.Navigation("PeliculasGeneros");
                });

            modelBuilder.Entity("apiPeliculas.Models.Pelicula", b =>
                {
                    b.Navigation("PeliculasActores");

                    b.Navigation("PeliculasGeneros");

                    b.Navigation("PeliculasSalasCines");
                });

            modelBuilder.Entity("apiPeliculas.Models.SalaCine", b =>
                {
                    b.Navigation("PeliculasSalasCines");
                });
#pragma warning restore 612, 618
        }
    }
}