using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaylistApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Songs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Songs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Playlists",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Playlists");
        }
    }
}
