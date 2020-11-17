﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NflStats.Data;

namespace NflStats.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20201117131223_TeamStatModel")]
    partial class TeamStatModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NflStats.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("Fumbles")
                        .HasColumnType("real");

                    b.Property<float?>("Games")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("PassInt")
                        .HasColumnType("real");

                    b.Property<float?>("PassTds")
                        .HasColumnType("real");

                    b.Property<float?>("PassYds")
                        .HasColumnType("real");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("RecTds")
                        .HasColumnType("real");

                    b.Property<float?>("RecYds")
                        .HasColumnType("real");

                    b.Property<int?>("RosterId")
                        .HasColumnType("int");

                    b.Property<float?>("RushTds")
                        .HasColumnType("real");

                    b.Property<float?>("RushYds")
                        .HasColumnType("real");

                    b.Property<int?>("Season")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RosterId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("NflStats.Models.Roster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Team")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rosters");
                });

            modelBuilder.Entity("NflStats.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Conference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("NflStats.Models.TeamStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("PassTds")
                        .HasColumnType("real");

                    b.Property<float>("PassYds")
                        .HasColumnType("real");

                    b.Property<float>("PenYds")
                        .HasColumnType("real");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<float>("RushTds")
                        .HasColumnType("real");

                    b.Property<float>("RushYds")
                        .HasColumnType("real");

                    b.Property<int?>("Season")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalYds")
                        .HasColumnType("real");

                    b.Property<float>("Turnovers")
                        .HasColumnType("real");

                    b.Property<float>("YdsPerAtt")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamStats");
                });

            modelBuilder.Entity("NflStats.Models.Player", b =>
                {
                    b.HasOne("NflStats.Models.Roster", "Roster")
                        .WithMany("Players")
                        .HasForeignKey("RosterId");

                    b.HasOne("NflStats.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("NflStats.Models.TeamStat", b =>
                {
                    b.HasOne("NflStats.Models.Team", "Team")
                        .WithMany("TeamStats")
                        .HasForeignKey("TeamId");
                });
#pragma warning restore 612, 618
        }
    }
}
