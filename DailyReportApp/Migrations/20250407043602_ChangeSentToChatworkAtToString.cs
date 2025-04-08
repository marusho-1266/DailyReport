using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyReportApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSentToChatworkAtToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SentToChatworkAt",
                table: "DailyReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentToChatworkAt",
                table: "DailyReports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
