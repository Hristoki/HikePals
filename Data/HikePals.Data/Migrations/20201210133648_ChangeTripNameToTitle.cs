using Microsoft.EntityFrameworkCore.Migrations;

namespace HikePals.Data.Migrations
{
    public partial class ChangeTripNameToTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripName",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_LocationId",
                table: "Trips",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_LocationId",
                table: "Trips",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_LocationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_LocationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TripName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips",
                column: "DestinationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
