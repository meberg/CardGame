﻿// <auto-generated />
using System;
using CardGame.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardGame.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20190812112747_updatedToDecimalTimePerFood")]
    partial class updatedToDecimalTimePerFood
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CardGame.ClassLibrary.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AccountCreationDate");

                    b.Property<int>("Age");

                    b.Property<decimal>("NumberOfHoursPlayed");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CardGame.ClassLibrary.UserScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<int>("Losses");

                    b.Property<int>("Score");

                    b.Property<decimal>("TimePerFood");

                    b.Property<int>("UserId");

                    b.Property<int>("Wins");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserScores");
                });

            modelBuilder.Entity("CardGame.ClassLibrary.UserScore", b =>
                {
                    b.HasOne("CardGame.ClassLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
