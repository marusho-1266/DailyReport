using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyReportApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChatworkApiKey = table.Column<string>(type: "TEXT", nullable: false),
                    ChatworkRoomId = table.Column<string>(type: "TEXT", nullable: false),
                    ReportTemplate = table.Column<string>(type: "TEXT", nullable: false),
                    AutoSendToChatwork = table.Column<bool>(type: "INTEGER", nullable: false),
                    DailyReminderTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Tasks = table.Column<string>(type: "TEXT", nullable: false),
                    Results = table.Column<string>(type: "TEXT", nullable: false),
                    TomorrowPlan = table.Column<string>(type: "TEXT", nullable: false),
                    MonthlyReview = table.Column<string>(type: "TEXT", nullable: false),
                    IsSentToChatwork = table.Column<bool>(type: "INTEGER", nullable: false),
                    SentToChatworkAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    TasksContent = table.Column<string>(type: "TEXT", nullable: false),
                    ResultsContent = table.Column<string>(type: "TEXT", nullable: false),
                    TomorrowPlanContent = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsageCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "DailyReports");

            migrationBuilder.DropTable(
                name: "ReportTemplates");
        }
    }
}
