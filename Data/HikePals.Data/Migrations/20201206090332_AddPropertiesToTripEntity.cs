using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HikePals.Data.Migrations
{
    public partial class AddPropertiesToTripEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproximateDuration",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "Distance",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Trips",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Trips");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ApproximateDuration",
                table: "Trips",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
