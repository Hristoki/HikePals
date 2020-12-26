using Microsoft.EntityFrameworkCore.Migrations;

namespace HikePals.Data.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripsUsers_Events_EventId",
                table: "TripsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TripsUsers_AspNetUsers_UserId",
                table: "TripsUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripsUsers",
                table: "TripsUsers");

            migrationBuilder.RenameTable(
                name: "TripsUsers",
                newName: "EventsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_TripsUsers_IsDeleted",
                table: "EventsUsers",
                newName: "IX_EventsUsers_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_TripsUsers_EventId",
                table: "EventsUsers",
                newName: "IX_EventsUsers_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsUsers",
                table: "EventsUsers",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventsUsers_Events_EventId",
                table: "EventsUsers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsUsers_AspNetUsers_UserId",
                table: "EventsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsUsers_Events_EventId",
                table: "EventsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsUsers_AspNetUsers_UserId",
                table: "EventsUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsUsers",
                table: "EventsUsers");

            migrationBuilder.RenameTable(
                name: "EventsUsers",
                newName: "TripsUsers");

            migrationBuilder.RenameIndex(
                name: "IX_EventsUsers_IsDeleted",
                table: "TripsUsers",
                newName: "IX_TripsUsers_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_EventsUsers_EventId",
                table: "TripsUsers",
                newName: "IX_TripsUsers_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripsUsers",
                table: "TripsUsers",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TripsUsers_Events_EventId",
                table: "TripsUsers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripsUsers_AspNetUsers_UserId",
                table: "TripsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
