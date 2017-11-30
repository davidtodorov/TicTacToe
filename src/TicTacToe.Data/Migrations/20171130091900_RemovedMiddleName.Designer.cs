﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Data.Migrations
{
    [DbContext(typeof(TicTacToeDbContext))]
    [Migration("20171130091900_RemovedMiddleName")]
    partial class RemovedMiddleName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TicTacToe.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("CreatorUserId");

                    b.Property<string>("HashedPassword")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid?>("OpponentUserId");

                    b.Property<int>("State");

                    b.Property<int>("Visibility");

                    b.HasKey("GameId");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("OpponentUserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TicTacToe.Models.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("DestinationUserId");

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("SourceUserId");

                    b.Property<int>("State");

                    b.HasKey("NotificationId");

                    b.HasIndex("DestinationUserId");

                    b.HasIndex("GameId");

                    b.HasIndex("SourceUserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TicTacToe.Models.Score", b =>
                {
                    b.Property<Guid>("ScoreId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GameId");

                    b.Property<int>("ScoreStatus");

                    b.Property<Guid>("UserId");

                    b.HasKey("ScoreId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("TicTacToe.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("RegistrationDate");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TicTacToe.Models.Game", b =>
                {
                    b.HasOne("TicTacToe.Models.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicTacToe.Models.User", "OpponentUser")
                        .WithMany()
                        .HasForeignKey("OpponentUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TicTacToe.Models.Notification", b =>
                {
                    b.HasOne("TicTacToe.Models.User", "DestinationUser")
                        .WithMany()
                        .HasForeignKey("DestinationUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicTacToe.Models.Game", "Game")
                        .WithMany("Notifications")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicTacToe.Models.User", "SourceUser")
                        .WithMany()
                        .HasForeignKey("SourceUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TicTacToe.Models.Score", b =>
                {
                    b.HasOne("TicTacToe.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TicTacToe.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
