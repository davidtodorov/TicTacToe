﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Data.Migrations
{
    public partial class Rename_Score_Status_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreStatus",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Scores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "ScoreStatus",
                table: "Scores",
                nullable: false,
                defaultValue: 0);
        }
    }
}